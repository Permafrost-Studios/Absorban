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
            var tfm = this.transform.TransformPoint(this.transform.localPosition + new Vector3(weaponlength,0f,0f));

            GameObject current = Instantiate(projectile, tfm, Quaternion.identity);

            float angle = this.gameObject.transform.parent.transform.localRotation.normalized.eulerAngles.z;

            int multx = m_moving.facingRight ? 1 : -1;

            float sinangle = Mathf.Sin(angle*Mathf.Deg2Rad);
            float cosangle = Mathf.Cos(angle*Mathf.Deg2Rad) * multx;

            Vector2 temp = new Vector2(cosangle,sinangle);
            temp = temp.normalized;

            current.GetComponent<WeaponProjectile>().Shoot(weaponDamage, shootForce, temp);

            m_remainingCooldown = shootCooldown;
        }   
    }

    // void OnDrawGizmos() {
    //     float sinangle = Mathf.Sin(this.gameObject.transform.parent.transform.localRotation.normalized.eulerAngles.z*Mathf.Deg2Rad);
    //     int multx = m_moving.facingRight ? 1 : -1;
    //     float cosangle = Mathf.Cos(this.gameObject.transform.parent.transform.localRotation.normalized.eulerAngles.z*Mathf.Deg2Rad) * multx;

    //     Vector2 temp = new Vector2(cosangle,sinangle);
    //     temp = temp.normalized;

    //     Gizmos.DrawLine(temp, temp*shootForce);
    // }
}
