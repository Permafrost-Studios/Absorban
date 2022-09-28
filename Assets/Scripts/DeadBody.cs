using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadBody : MonoBehaviour
{
    private int drop;
    private string m_name;
    private int m_resources;

    void Init(int dropPrefab, string name) 
    {
        drop = dropPrefab;
        m_name = name;
        m_resources = 0;
    }

    void Init(string name) 
    {
        m_name = name;
        m_resources = 1;
    }

    void Init(int dropPrefab) 
    {
        drop = dropPrefab;
        m_resources = 2;
    }

    void OnTriggerEnter2D(Collider2D other) 
    {
        //Make the main slime character do an absorption animation

        switch (m_resources)
        {
            case 0:
                other.gameObject.GetComponent<MemoryBridge>().MemoryAdd(m_name);
                // other.gameObject.GetComponent<WeaponManager>().EnableWeapon(drop);
                break;
            case 1:
                other.gameObject.GetComponent<MemoryBridge>().MemoryAdd(m_name);
                break;
            case 2:
                // other.gameObject.GetComponent<WeaponManager>().EnableWeapon(drop);
                break;
            default:
                break;
        }

        Despawn();
    }

    void Despawn() 
    {
        //Play despawn animation (L)
        Destroy(gameObject);
    }
}
