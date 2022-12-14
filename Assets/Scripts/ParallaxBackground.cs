using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxBackground : MonoBehaviour
{
    [SerializeField] private Camera m_cam;
    public float parallaxValue;
    private Vector2 m_length;
    [SerializeField] private Vector2 m_camlength;
    private Vector2 m_offsetpos;
    [SerializeField] private GameObject m_bg;

    // Start is called before the first frame update
    void Start() {
        m_length = GetComponentInChildren<SpriteRenderer>().bounds.size;
        m_offsetpos = m_cam.transform.localPosition;
        m_camlength = m_cam.sensorSize;

        if (!m_bg) {
            m_bg = this.gameObject.GetComponentInChildren<SpriteRenderer>().gameObject;
        } else {
            Instantiate(m_bg, Vector3.zero, Quaternion.identity, this.transform);
        }

        Instantiate(m_bg,new Vector3(m_length.x,0f,0f), Quaternion.identity, this.transform); //Left and right
        Instantiate(m_bg,new Vector3(-m_length.x,0f,0f), Quaternion.identity, this.transform);

        Instantiate(m_bg,new Vector3(m_length.x,m_length.y,0f), Quaternion.identity, this.transform); //Corners
        Instantiate(m_bg,new Vector3(-m_length.x,-m_length.y,0f), Quaternion.identity, this.transform);
        Instantiate(m_bg,new Vector3(m_length.x,-m_length.y,0f), Quaternion.identity, this.transform);
        Instantiate(m_bg,new Vector3(-m_length.x,m_length.y,0f), Quaternion.identity, this.transform);

        Instantiate(m_bg,new Vector3(0f,m_length.y,0f), Quaternion.identity, this.transform); //Top and Bottom
        Instantiate(m_bg,new Vector3(0f,-m_length.y,0f), Quaternion.identity, this.transform);
    }

    // Update is called once per frame
    void Update() {
        Vector2 relativepos = m_cam.transform.localPosition*parallaxValue;
        // Out of bounds left = negative value and vice-versa
        Vector2 temp = (Vector2) m_cam.transform.localPosition-(relativepos+m_offsetpos);

        // Shift if too far left/right
        if(temp.x>(m_length.x/2)) {
            m_offsetpos.x += m_length.x;
        } else if (temp.x<(-m_length.x/2)) {
            m_offsetpos.x -= m_length.x;
        }

        if(temp.y>(m_length.y/2)) {
            m_offsetpos.y += m_length.y;
        } else if (temp.y<(-m_length.y/2)) {
            m_offsetpos.y -= m_length.y;
        }

        transform.localPosition = relativepos+m_offsetpos;
    }
}
