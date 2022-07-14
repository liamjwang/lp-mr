using System;
using System.Collections.Generic;

using UnityEngine;

namespace QRTracking
{
    public class QRCodesVisManager : Singleton<QRCodesVisManager>
    {
        public GameObject qrPrefab;

        private SortedDictionary<string, GameObject> qrCodesObjectsList = new SortedDictionary<string, GameObject>();

        
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

        private Queue<ActionData> pendingActions = new Queue<ActionData>();
        void Awake()
        {

        }

        // Use this for initialization
        void Start()
        {
            Debug.Log("QRCodesVisualizer start");

            QRCodesManager.Instance.QRCodesTrackingStateChanged += Instance_QRCodesTrackingStateChanged;
            QRCodesManager.Instance.QRCodeAdded += Instance_QRCodeAdded;
            QRCodesManager.Instance.QRCodeUpdated += Instance_QRCodeUpdated;
            QRCodesManager.Instance.QRCodeRemoved += Instance_QRCodeRemoved;
            if (qrPrefab == null)
            {
                throw new Exception("Prefab not assigned");
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
                        if (!qrCodesObjectsList.ContainsKey(action.qrCode.Data))
                        {
                            InstantiateQRCode(action);
                        }
                    }
                    else if (action.type == ActionData.Type.Removed)
                    {
                        // if (qrCodesObjectsList.ContainsKey(action.qrCode.Id))
                        // {
                        //     Destroy(qrCodesObjectsList[action.qrCode.Id]);
                        //     qrCodesObjectsList.Remove(action.qrCode.Id);
                        // }
                    }
                }
            }
            // if (clearExisting)
            // {
            //     clearExisting = false;
            //     foreach (var obj in qrCodesObjectsList)
            //     {
            //         Destroy(obj.Value);
            //     }
            //     qrCodesObjectsList.Clear();
            //
            // }
        }

        private void InstantiateQRCode(ActionData action)
        {
            GameObject qrCodeObject = GetOrCreateQrObject(action.qrCode.Data);
            qrCodeObject.GetOrAddComponent<SpatialGraphCoordinateSystem>().Id = action.qrCode.SpatialGraphNodeId;
            qrCodeObject.GetOrAddComponent<QRCodeDisplay>().qrCode = action.qrCode;
        }

        public QRCodeDisplay GetQRCode(string data)
        {
            GameObject qrCodeObject = GetOrCreateQrObject(data);
            return qrCodeObject.GetOrAddComponent<QRCodeDisplay>();
        }

        private GameObject GetOrCreateQrObject(string data)
        {
            GameObject qrCodeObject;
            if (qrCodesObjectsList.ContainsKey(data))
            {
                qrCodeObject = qrCodesObjectsList[data];
                Debug.Log("Existing QRVis: data=" + data + " length=" + data.Length);
            }
            else
            {
                qrCodeObject = Instantiate(qrPrefab);
                qrCodesObjectsList.Add(data, qrCodeObject);
                Debug.Log("New QRVis: data=" + data + " length=" + data.Length);
            }

            qrCodeObject.GetOrAddComponent<SpatialGraphCoordinateSystem>();
            qrCodeObject.GetOrAddComponent<QRCodeDisplay>();
            return qrCodeObject;
        }

        // Update is called once per frame
        void Update()
        {
            HandleEvents();
        }
    }

}