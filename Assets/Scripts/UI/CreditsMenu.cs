using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class CreditsMenu : MonoBehaviour
{
    private UIDocument document;

    #nullable enable
    private GameObject? m_returnTo;
    public void SetReturnToObject(GameObject returnto) => m_returnTo = returnto;

    void OnEnable() {
        document = GetComponent<UIDocument>();
        Button back = document.rootVisualElement.Q<Button>("Return");
        back.clicked += OnReturnClicked;
    }

    void OnReturnClicked() {
        // no `?.` because unity :skull:
        if (m_returnTo)
            m_returnTo!.SetActive(true);
        Destroy(this.gameObject);
    }
}
