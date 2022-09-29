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

    private PlayerMoving m_moving; 

    // GetButtonDown or GetButton, false = semi
    [SerializeField] private bool semiorautomatic;


    // To spawn the projectile at the end of the weapon
    [SerializeField] private float weaponlength;

    // Start is called before the first frame update
    void Start() {
        m_remainingCooldown = shootCooldown;
        m_moving = this.gameObject.transform.parent.parent.gameObject.GetComponent<PlayerMoving>();
        weaponlength = this.GetComponent<SpriteRenderer>().bounds.extents.x;      
    }

    // Update is called once per frame
    void Update() {
        m_remainingCooldown -= Time.deltaTime;

        // Debug.Log();

        if(semiorautomatic ? Input.GetButton("Fire1") : Input.GetButtonDown("Fire1")) {
            // Spawn bullet at the tip of the weapon
            var tfm = this.transform.position + new Vector3(weaponlength,0f,-10f);

            GameObject current = Instantiate(projectile, tfm, Quaternion.identity);
            float directionMultiplier = m_moving.returnDirectionMultiplier();
            current.GetComponent<WeaponProjectile>().Shoot(weaponDamage, shootForce, directionMultiplier);

            m_remainingCooldown = shootCooldown;
        }   
    }
}
