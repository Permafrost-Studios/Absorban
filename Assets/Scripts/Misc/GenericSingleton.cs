
using UnityEngine;
using System;

// should be abstract
public class GenericSingleton<T> : MonoBehaviour where T : MonoBehaviour {

    private static readonly Lazy<T> lazyInstance = new Lazy<T>(CreateObject);

    public static T instance => lazyInstance.Value;

    private static T CreateObject() {
        GameObject owner = new GameObject(nameof(T));
        T instance = owner.AddComponent<T>();
        DontDestroyOnLoad(owner);
        return instance;
    }
} // End GenericSingleton