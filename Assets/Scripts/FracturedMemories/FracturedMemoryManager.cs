using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Newtonsoft.Json;
using UnityEngine.UIElements;
using System;


public class FracturedMemoryManager : MonoBehaviour
{
    private string m_path;
    private Memory[] Memories;
    private int m_nextID;

    private List<string> m_storyList = new List<string>();
    private List<string> m_nameList = new List<string>();
    
    public ListView displayNameList;
    public ScrollView displaystoryList;

    // Start is called before the first frame update
    void Start()
    {
        //m_path = wherever tf i need to put it in the game files

        m_nextID = 0;
        Memories = GenerateMemories(m_path);
    }

    void OnEnable()
    {
        Func<VisualElement> makeName = () => new Label();

        Action<VisualElement, int> bindName = (e, i) => (e as Label).text = m_nameList[i];

        // const int itemHeight = 16;

        displayNameList.selectionType = SelectionType.Multiple;

        displayNameList.onItemsChosen += objects => Debug.Log(objects);
        displayNameList.onSelectionChange += objects => Debug.Log(objects);
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
        foreach (Memory m in Memories) 
        {
            if (m.name == name) 
            {
                m.obtained = true;
                m.ID = m_nextID;
                m_nextID += 1;
                m_storyList.Add(m.story);
                m_nameList.Add(m.name);
            }
        }
    }
}
