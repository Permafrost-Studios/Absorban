using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponShoot : MonoBehaviour
{
    public GameObject projectile;
    public float shootCooldown;
    
    public float weaponDamage;
    public float shootForce;

    private float m_remainingCooldown;

    // Start is called before the first frame update
    void Start()
    {
        m_remainingCooldown = shootCooldown;        
    }

    // Update is called once per frame
    void Update()
    {
        m_remainingCooldown -= Time.deltaTime;

        if(Input.GetButtonDown("Fire1")) 
        {
            GameObject current = Instantiate(projectile);
            float directionMultiplier = gameObject.transform.parent.parent.gameObject.GetComponent<PlayerMoving>().returnDirectionMultiplier();
            current.GetComponent<WeaponProjectile>().Shoot(weaponDamage, shootForce, directionMultiplier);

            m_remainingCooldown = shootCooldown;
        }   
    }
}
