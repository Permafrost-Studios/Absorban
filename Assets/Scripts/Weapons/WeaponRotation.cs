using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class WeaponRotation : MonoBehaviour {

    [SerializeField] Camera m_cam;
    private PlayerMoving m_moving; 

    // Start is called before the first frame update
    void Start() {
        if (!m_cam) {
            m_cam = FindObjectOfType<Camera>();
        }
        m_moving = this.gameObject.transform.parent.gameObject.GetComponent<PlayerMoving>();
    }


    // Update is called once per frame
    void Update() {
        Vector2 mousescreenpos = Mouse.current.position.ReadValue();
        

        Vector2 deltapos = (Vector2)this.transform.position-(Vector2)m_cam.ScreenToWorldPoint(mousescreenpos);

        Vector3 eulerangles = new Vector3(0f,0f,
            (Mathf.Atan2(deltapos.y,deltapos.x)
            *Mathf.Rad2Deg) - (m_moving.facingRight ? 0f : 180f)
            );

        this.transform.rotation = Quaternion.Euler(eulerangles);
    }

    
}
