
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MemoryBridge : MonoBehaviour
{
    private GameObject memoryManager; 
    // Start is called before the first frame update
    void Start()
    {
        memoryManager = GameObject.Find("MemoryManager");
    }
    public void MemoryAdd(string name) 
    {
        memoryManager.GetComponent<FracturedMemoryManager>().AddMemory(name);
    }
}
