using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class OptionsMenu : MonoBehaviour
{
    [SerializeField] private UIDocument m_UIDocument;

    #nullable enable
    private GameObject? m_returnTo;

    public void SetReturnToObject(GameObject returnto) => m_returnTo = returnto;

    // Get UI Document and register callbacks
    void Start() {
        VisualElement rootVis =  m_UIDocument.rootVisualElement;
        Button quit = rootVis.Q<Button>("Return");
        quit.clicked += OnReturnClicked;
    }
    
    void OnReturnClicked() {
        // no ?. because unity :skull:
        if (m_returnTo)
            m_returnTo!.SetActive(true);
        Destroy(this.gameObject);
    }

    #nullable restore


    void Update() {

    }
}
