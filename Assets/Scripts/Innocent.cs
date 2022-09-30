using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Innocent : MonoBehaviour
{
    public float moveCooldown;
    private float m_moveCooldown;

    // Update is called once per frame
    void Update()
    {
        m_moveCooldown -= Time.deltaTime;

        if (m_moveCooldown <= 0) 
        {
            // gameObject.transform.velocity    
        }
    }
}
