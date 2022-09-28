using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using System;

public class OptionsMenu : MonoBehaviour
{
    private List<SettingEntry> settingslist = new List<SettingEntry>();



    [SerializeField] private VisualTreeAsset m_OptionEntryTemplate;
    [SerializeField] private UIDocument m_UIDocument;

    #nullable enable
    private GameObject? m_returnTo;
    public void SetReturnToObject(GameObject returnto) => m_returnTo = returnto;

    // Get UI Document and register callbacks, MUST be OnEnable
    void OnEnable() {
        VisualElement rootVis =  m_UIDocument.rootVisualElement;
        Button quit = rootVis.Q<Button>("Return");
        quit.clicked += OnReturnClicked;

        // settingslist = new List<SettingEntry>(){
        //     new SettingEntry ("Volume", new MinMaxSlider())
        // };
        PopulateSettingsList();
        PopulateElems(rootVis);
    }
    
    void OnReturnClicked() {
        // no ?. because unity :skull:
        if (m_returnTo)
            m_returnTo!.SetActive(true);
        Destroy(this.gameObject);
    }

    #nullable restore

    void PopulateElems(VisualElement rootVis) {
        VisualElement optcontainer = rootVis.Q<VisualElement>("OptionsContainer");

        foreach (var item in settingslist) {
            var elem = m_OptionEntryTemplate.Instantiate();
            
            elem.Q<Label>("EntryName").text = item.name;
            elem.Q<VisualElement>("EntryObject").Add(item.element);

            optcontainer.Add(elem);
        }
    }

    void PopulateSettingsList() {
        settingslist.Add(new SettingEntry("Master Volume", new Slider(0f,100f)));
        settingslist.Add(new SettingEntry("Music Volume", new Slider(0f,100f,0f,100f)));
        settingslist.Add(new SettingEntry("SFX Volume", new Slider(0f,100f,0f,100f)));

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
