using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class EndMenu : MonoBehaviour
{
    private UIDocument document;

    // Start is called before the first frame update
    void Start()
    {
        document = GetComponent<UIDocument>();
        
        Label descriptionLabel = document.rootVisualElement.Q<Label>("Description");
        
        Button exitButton = document.rootVisualElement.Q<Button>("Description");
        Button replayButton = document.rootVisualElement.Q<Button>("Description");
        
        descriptionLabel.text = "Thank you for playing our game.";

        exitButton.clicked += OnExitClicked;
        replayButton.clicked += OnReplayClicked;
    }

    void OnExitClicked() 
    {
        Application.Quit();
    }

    void OnReplayClicked() 
    {
        SceneManager.LoadScene("MainMenu");
    }
}
