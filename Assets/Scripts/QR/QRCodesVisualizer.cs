using System;
using System.Collections;

using System.Collections.Generic;

using UnityEngine;

using Microsoft.MixedReality.QR;
namespace QRTracking
{
    public abstract class SingleQRFollower : MonoBehaviour
    {
        public abstract void Follow(QRCode qrCode);
        public abstract void UpdateLastCameraPose(Vector3 cameraTransform);
    }
    public class QRCodesVisualizer : MonoBehaviour
    {
        public GameObject defaultQrPrefab;
        public GameObject silentQrPrefab;
        public List<QRPrefab> qrFollowers;
        
        [Serializable]
        public struct QRPrefab
        {
            public string data;
            public SingleQRFollower follower;
        }

        private System.Collections.Generic.SortedDictionary<System.Guid, GameObject> qrCodesObjectsList;
        private bool clearExisting = false;

        struct ActionData
        {
            public enum Type
            {
                Added,
                Updated,
                Removed
            };
            public Type type;
            public Microsoft.MixedReality.QR.QRCode qrCode;

            public ActionData(Type type, Microsoft.MixedReality.QR.QRCode qRCode) : this()
            {
                this.type = type;
                qrCode = qRCode;
            }
        }

        private System.Collections.Generic.Queue<ActionData> pendingActions = new Queue<ActionData>();
        public Transform cameraTransform;

        void Awake()
        {

        }

        // Use this for initialization
        void Start()
        {
            Debug.Log("QRCodesVisualizer start");
            qrCodesObjectsList = new SortedDictionary<System.Guid, GameObject>();

            QRCodesManager.Instance.QRCodesTrackingStateChanged += Instance_QRCodesTrackingStateChanged;
            QRCodesManager.Instance.QRCodeAdded += Instance_QRCodeAdded;
            QRCodesManager.Instance.QRCodeUpdated += Instance_QRCodeUpdated;
            QRCodesManager.Instance.QRCodeRemoved += Instance_QRCodeRemoved;
            if (defaultQrPrefab == null)
            {
                throw new System.Exception("Prefab not assigned");
            }
        }
        private void Instance_QRCodesTrackingStateChanged(object sender, bool status)
        {
            if (!status)
            {
                clearExisting = true;
            }
        }

        private void Instance_QRCodeAdded(object sender, QRCodeEventArgs<Microsoft.MixedReality.QR.QRCode> e)
        {
            Debug.Log("QRCodesVisualizer Instance_QRCodeAdded");

            lock (pendingActions)
            {
                pendingActions.Enqueue(new ActionData(ActionData.Type.Added, e.Data));
            }
        }

        private void Instance_QRCodeUpdated(object sender, QRCodeEventArgs<Microsoft.MixedReality.QR.QRCode> e)
        {
            Debug.Log("QRCodesVisualizer Instance_QRCodeUpdated");

            lock (pendingActions)
            {
                pendingActions.Enqueue(new ActionData(ActionData.Type.Updated, e.Data));
            }
        }

        private void Instance_QRCodeRemoved(object sender, QRCodeEventArgs<Microsoft.MixedReality.QR.QRCode> e)
        {
            Debug.Log("QRCodesVisualizer Instance_QRCodeRemoved");

            lock (pendingActions)
            {
                pendingActions.Enqueue(new ActionData(ActionData.Type.Removed, e.Data));
            }
        }

        private void HandleEvents()
        {
            lock (pendingActions)
            {
                while (pendingActions.Count > 0)
                {
                    var action = pendingActions.Dequeue();
                    if (action.type == ActionData.Type.Added)
                    {
                        InstantiateQRCode(action);
                    }
                    else if (action.type == ActionData.Type.Updated)
                    {
                        if (!qrCodesObjectsList.ContainsKey(action.qrCode.Id))
                        {
                            InstantiateQRCode(action);
                        }
                        else
                        {
                            UpdateQRCode(action);
                        }
                    }
                    else if (action.type == ActionData.Type.Removed)
                    {
                        if (qrCodesObjectsList.ContainsKey(action.qrCode.Id))
                        {
                            Destroy(qrCodesObjectsList[action.qrCode.Id]);
                            qrCodesObjectsList.Remove(action.qrCode.Id);
                        }
                    }
                }
            }
            if (clearExisting)
            {
                clearExisting = false;
                foreach (var obj in qrCodesObjectsList)
                {
                    Destroy(obj.Value);
                }
                qrCodesObjectsList.Clear();

            }
        }

        private void UpdateQRCode(ActionData action)
        {
            string qrCodeData = action.qrCode.Data;
            SingleQRFollower follower = null;
            foreach (QRPrefab qrFollower in qrFollowers)
            {
                if (qrFollower.data == qrCodeData)
                {
                    follower = qrFollower.follower;
                }
            }
            if (follower != null)
            {
                follower.UpdateLastCameraPose(cameraTransform.position);
            }
        }

        private void InstantiateQRCode(ActionData action)
        {
            string qrCodeData = action.qrCode.Data;
            SingleQRFollower follower = null;
            foreach (QRPrefab qrFollower in qrFollowers)
            {
                if (qrFollower.data == qrCodeData)
                {
                    follower = qrFollower.follower;
                }
            }
            GameObject qrCodeObject = Instantiate(follower == null ? defaultQrPrefab : silentQrPrefab, new Vector3(0, 0, 0), Quaternion.identity);
            qrCodeObject.GetComponent<SpatialGraphCoordinateSystem>().Id = action.qrCode.SpatialGraphNodeId;
            qrCodeObject.GetComponent<QRCode>().qrCode = action.qrCode;
            qrCodesObjectsList.Add(action.qrCode.Id, qrCodeObject);
            if (follower != null)
            {
                follower.Follow(qrCodeObject.GetComponent<QRCode>());
            }
            UpdateQRCode(action);
        }

        // Update is called once per frame
        void Update()
        {
            HandleEvents();
        }
    }

}