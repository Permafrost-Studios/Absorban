using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private UIDocument m_UIDocument;
    [SerializeField] private GameObject m_optionsMenu;
	
    // Get UI Document and register callbacks, MUST be OnEnable
    void OnEnable() {
        VisualElement rootVis =  m_UIDocument.rootVisualElement;

        Button strt = rootVis.Q<Button>("Start");
        Button optn = rootVis.Q<Button>("Options");
        Button crdt = rootVis.Q<Button>("Credits");
        Button quit = rootVis.Q<Button>("Quit");


        strt.clicked += OnStartClicked;
        optn.clicked += OnOptionsClicked;
        crdt.clicked += OnCreditsClicked;
        quit.clicked += OnQuitClicked;
		
    }

    void OnStartClicked() {
        Debug.Log("start button clicked");
    }

    void OnOptionsClicked() {
        Instantiate(m_optionsMenu).GetComponent<OptionsMenu>().SetReturnToObject(this.gameObject);
        this.gameObject.SetActive(false);
    }

    void OnCreditsClicked() {
        Debug.Log("credits button clicked");
    }

    void OnQuitClicked() {
        Application.Quit();
    }
}
