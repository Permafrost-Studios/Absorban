using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseTrap : MonoBehaviour
{
    public float damage;
    
    void OnTriggerEnter(Collider other) 
    {
        other.gameObject.GetComponent<PlayerHealth>().TakeDamage(damage);
    }
}
