using UnityEngine;
using Unity.Netcode;
using System;

namespace Cuake.Actor.Singleton
{
    public class GenSingleton<T>
    where T : Component
    {
        private static T _instance;
        public static T Singleton (Func<T[]> finder)
        {
            {
                if (_instance == null)
                {
                    T[] objs = finder();
                    if (objs.Length > 0)
                        _instance = objs[0];
                    if (objs.Length > 1)
                    {
                        Logger.Singleton.LogError("There is more than one " + typeof(T).Name + " in the scene.");
                    }
                    if (_instance == null)
                    {
                        GameObject obj = new GameObject();
                        obj.name = string.Format("_{0}", typeof(T).Name);
                        _instance = obj.AddComponent<T>();
                    }
                }
                return _instance;
            }
        }
    }

    public class MonoSingleton<T> : MonoBehaviour where T : Component
    {
        public static T Singleton { get { return GenSingleton<T>.Singleton(() => FindObjectsOfType(typeof(T)) as T[]); } }
    }
    public class NetworkSingleton<T> : NetworkBehaviour where T : Component
    {
        public static T Singleton { get { return GenSingleton<T>.Singleton(() => FindObjectsOfType(typeof(T)) as T[]); } }
    }
}