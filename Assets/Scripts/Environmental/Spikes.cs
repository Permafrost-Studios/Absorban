using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spikes : MonoBehaviour
{
    public float trapDamage;

    void OnTriggerEnter2D(Collider2D other) 
    {
        other.gameObject.GetComponent<PlayerHealth>().TakeDamage(trapDamage);
    }
}
