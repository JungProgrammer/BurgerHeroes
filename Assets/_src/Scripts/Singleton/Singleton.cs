using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace BurgerHeroes
{
    public class Singleton<T> : SerializedMonoBehaviour where T:Component
    {
        private static T _instance;

        public static T Instance
        {
            get
            {
                if (_instance == null)
                {
                    var objects = FindObjectsOfType(typeof(T)) as T[];
                    if (objects.Length > 0)
                    {
                        _instance = objects[0];
                    }

                    if (objects.Length > 1)
                    {
                        Debug.LogError("There is more than one " + typeof(T).Name + " in the scene");
                    }

                    if (_instance == null)
                    {
                        GameObject gameObject = new GameObject();
                        gameObject.hideFlags = HideFlags.HideAndDontSave;
                        _instance = gameObject.AddComponent<T>();
                    }
                }

                return _instance;
            }
        }

        public virtual void OnDestroy()
        {
            if (_instance == this)
            {
                _instance = null;
            }
        }
    }   
}
