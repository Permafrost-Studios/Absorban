using System.Collections;
using System.Collections.Specialized;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using System;

public class OptionsMenu : MonoBehaviour
{
    [SerializeField] private VisualTreeAsset m_OptionEntryTemplate;
    [SerializeField] private UIDocument m_UIDocument;
    private OptionsSaver m_saver;

    #nullable enable
    private GameObject? m_returnTo;
    public void SetReturnToObject(GameObject returnto) => m_returnTo = returnto;
    
    
    
    private OrderedDictionary m_settingsNameDict = new OrderedDictionary{
        {"main_vol","Master Volume"},
        {"music_vol","Music Volume"},
        {"sfx_vol","SFX Volume"}
    };

    private Dictionary<string, VisualElement> m_settingsDisplayDict = new Dictionary<string, VisualElement>();
    void PopulateElemsDict() {
        m_settingsDisplayDict.Clear();

        m_settingsDisplayDict.Add("main_vol",new Slider(0f,100f));
        m_settingsDisplayDict.Add("music_vol",new Slider(0f,100f));
        m_settingsDisplayDict.Add("sfx_vol",new Slider(0f,100f));
    }


    // Get UI Document and register callbacks, MUST be OnEnable
    void OnEnable() {
        m_saver = OptionsSaver.CreateInstance<OptionsSaver>();
        VisualElement rootVis =  m_UIDocument.rootVisualElement;
        Button quit = rootVis.Q<Button>("Return");
        quit.clicked += OnReturnClicked;

        PopulateElemsDict();
        PopulateElems(rootVis);
    }
    
    void OnReturnClicked() {
        // no `?.` because unity :skull:
        if (m_returnTo)
            m_returnTo!.SetActive(true);
        Destroy(this.gameObject);
    }

    #nullable restore

    void PopulateElems(VisualElement rootVis) {
        VisualElement optcontainer = rootVis.Q<VisualElement>("OptionsContainer");

        // Retrieve saved data in prep
        Hashtable data = m_saver.ReadOpts();

        foreach (DictionaryEntry item in m_settingsNameDict) {
            var elem = m_OptionEntryTemplate.Instantiate();

            elem.Q<Label>("EntryName").text = item.Value as string;
            VisualElement element = m_settingsDisplayDict[item.Key as string];

            elem.Q<VisualElement>("EntryObject").Add(element);
            optcontainer.Add(elem);

            switch (element) {
                case Slider:
                    (element as Slider).value = (float)((double)data[item.Key]);
                    break;
                default:
                    break;
            }

            RegisterElemCallbacks(element, (string) item.Key);
        }
    }

    void RegisterElemCallbacks(VisualElement element, string fieldname) {
        (element as Slider).RegisterValueChangedCallback(x => m_saver.UpdateOptions(fieldname, x.newValue));
        // settingsdict.Add("Master Volume", new Slider(0f,100f));
        // settingsdict.Add("Music Volume", new Slider(0f,100f,0f,100f));
        // settingsdict.Add("SFX Volume", new Slider(0f,100f,0f,100f));

    }


    struct SettingEntry {
        public SettingEntry(string _nam, VisualElement _elm) {
            this.name = _nam;
            this.element = _elm;   
        }

        public string name;
        public VisualElement element;
    }    
}
