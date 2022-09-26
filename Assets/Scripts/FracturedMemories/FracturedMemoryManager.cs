using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Newtonsoft.Json;


public class FracturedMemoryManager : MonoBehaviour
{
    private string m_path;
    private Memory[] Memories;

    // Start is called before the first frame update
    void Start()
    {
        //m_path = wherever tf i need to put it in the game files

        Memories = GenerateMemories(m_path);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    Memory[] GenerateMemories(string path) 
    {
        if (!File.Exists(path)) 
        {
            Debug.LogError("Error: There is no memories file. 🤓");
            Memory[] placeholder = new Memory[10];
            return placeholder;
        } 
        else
        {
            string fileContents = File.ReadAllText(path);

            return JsonConvert.DeserializeObject<Memory[]>(fileContents);
        }
        
    }

    void AddMemory(string name) 
    {

    }
}
