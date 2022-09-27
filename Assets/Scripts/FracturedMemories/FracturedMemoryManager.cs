using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Newtonsoft.Json;
using UnityEngine.UIElements;
using System;
using UnityEditor;

//Part of this script was reused from a tutorial on ListView elements in the Unity UI Toolkit: https://docs.unity3d.com/Manual/UIE-HowTo-CreateRuntimeUI.html
public class FracturedMemoryManager : MonoBehaviour
{    
    private string m_path;
    private Memory[] Memories;
    private int m_nextID;

    [SerializeField]
    VisualTreeAsset nameTemplate;

    private List<Memory> discoveredMemories;
    
    public ListView displayNameList;
    public ScrollView displayStory;
    public Label storyContent;

    private UIDocument document;

    // Start is called before the first frame update
    void Start()
    {
        m_path = "Assets/Savedata/memories.json";

        m_nextID = 0;
        //Memories = GenerateMemories(m_path);

        discoveredMemories = new List<Memory>();
    }

    void OnEnable()
    {
        document = GetComponent<UIDocument>();
        Memories = GenerateMemories(m_path);
        
        InitializeList(document.rootVisualElement);

        Stub();
    }

    void Stub() 
    {
        AddMemory("George");
        AddMemory("Fred");
        AddMemory("Reynold");
    }

    Memory[] GenerateMemories(string path) 
    {
        TextAsset textMems = (TextAsset)AssetDatabase.LoadAssetAtPath("Assets/Savedata/memories.json", typeof(TextAsset));
        string mems = textMems.ToString();
        return JsonConvert.DeserializeObject<Memory[]>(mems);
        
    }

    void InitializeList(VisualElement root) 
    {
        displayNameList = root.Q<ListView>("NameList");
        displayStory = root.Q<ScrollView>("ReadView");
        storyContent = root.Q<Label>("Content");

         // Set up a make item function for a list entry
        displayNameList.makeItem = () =>
        {
            var newListEntry = nameTemplate.Instantiate();

            var newListEntryLogic = new ListEntryController();

            newListEntry.userData = newListEntryLogic;

            newListEntryLogic.SetVisualElement(newListEntry);

            return newListEntry;
        };

        displayNameList.bindItem = (item, index) =>
        {
            (item.userData as ListEntryController).SetMemoryData(discoveredMemories[index]);
        };

        displayNameList.fixedItemHeight = 45;

        displayNameList.itemsSource = discoveredMemories;

        displayNameList.onSelectionChange += NameSelected;
    }

    void AddMemory(string name) 
    {
        foreach (Memory m in Memories) 
        {
            if (m.name == name) 
            {
                m.obtained = true;
                m.ID = m_nextID;

                discoveredMemories.Add(m);

                m_nextID += 1;
            }
        }

        InitializeList(document.rootVisualElement);
    }

    void NameSelected(IEnumerable<object> selectedItems)
    {
        // Get the currently selected item directly from the ListView
        var selectedMemory = displayNameList.selectedItem as Memory;

        // Handle none-selection (Escape to deselect everything)
        if (selectedMemory == null)
        {
            storyContent.text = " ";
            return;
        }

        // Fill in character detail
        storyContent.text = selectedMemory.story;
    }
}

public class ListEntryController
{
    Label m_Label;

    public void SetVisualElement(VisualElement visualElement)
    {
        m_Label = visualElement.Q<Label>("Name");
    }

    public void SetMemoryData(Memory mem)
    {
        m_Label.text = mem.name;
    }
}
