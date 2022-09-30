using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SwitchScene : MonoBehaviour
{
    public string newScene;

    void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.gameObject.layer == 7) 
        {
            SceneManager.LoadScene(newScene);
        }
    }
}
