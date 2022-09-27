using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using System;

public class OptionsMenu : MonoBehaviour
{
    private List<VisualElement> items;
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
        PopulateListView(rootVis);
    }
    
    void OnReturnClicked() {
        // no ?. because unity :skull:
        if (m_returnTo)
            m_returnTo!.SetActive(true);
        Destroy(this.gameObject);
    }

    #nullable restore


    void PopulateListView(VisualElement rootVis) {
        Debug.Log("Attempt PopulateListView");
        ListView optlist = rootVis.Q<ListView>("OptionsList");
        
        items = new List<VisualElement>();

        optlist.bindItem = OnBindItem;
        optlist.makeItem = OnMakeItem;
        optlist.itemsSource = items;
        optlist.fixedItemHeight = 80;

        optlist.onItemsChosen += objects => Debug.Log(objects);


        var temp = new MinMaxSlider(0f,100f,0f,100f);
        var temp2 = new MinMaxSlider(1f,100f,0f,100f);
        items.Add(temp);
        items.Add(temp2);


    }

    void OnBindItem(VisualElement elem, int idx) {
        ElemData elm = (elem as ElemData);
        elm._lbl.text = "hello";
        // elem.user 
        // (elem as Label).text = "hello";
    }

    VisualElement OnMakeItem() {
        var newListEntry = m_OptionEntryTemplate.Instantiate();
        // newListEntry.
        return newListEntry;
    }

    class ElemData : VisualElement {
        public Label _lbl;
        public VisualElement _elem;
    }

}
