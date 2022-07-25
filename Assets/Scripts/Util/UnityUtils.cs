using UnityEngine;

namespace Util
{
    public static class UnityUtils
    {
        public static T GetOrAddComponent<T>(this GameObject go) where T : Component // todo: move
        {
            T comp = go.GetComponent<T>();
            if (!comp)
                comp = go.AddComponent<T>();
            return comp;
        }
    }
}