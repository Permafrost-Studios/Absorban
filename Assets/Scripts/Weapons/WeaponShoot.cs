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

    private AudioSource m_source;
    public AudioClip m_shootySound;

    // GetButtonDown or GetButton, false = semi
    [SerializeField] private bool semiorautomatic;


    // To spawn the projectile at the end of the weapon
    [SerializeField] private float weaponlength;

    private Vector3 normalscale;
    // Start is called before the first frame update
    void Start() {
        m_source = GetComponent<AudioSource>();
        m_source.outputAudioMixerGroup = SoundManager.instance.MasterMixer.FindMatchingGroups("SFX")[0];

        m_remainingCooldown = 0f;
        m_moving = this.gameObject.transform.parent.parent.gameObject.GetComponent<PlayerMoving>();
        weaponlength = this.GetComponent<SpriteRenderer>().bounds.extents.x;      

        normalscale = this.transform.localScale;
    }

    // Update is called once per frame

    [SerializeField] private bool flipped;

    void Update() {
        m_remainingCooldown -= Time.deltaTime;
        
        float angle = this.gameObject.transform.parent.transform.localRotation.normalized.eulerAngles.z;
        int multx = m_moving.facingRight ? -1 : 1;

        float sinangle = Mathf.Sin(angle*Mathf.Deg2Rad);
        float cosangle = Mathf.Cos(angle*Mathf.Deg2Rad) * multx;

        if (cosangle*multx > 0f) {
            flipped = true;
        } else {
            flipped = false;
        }

        if (flipped) {
            this.transform.localScale = new Vector3(
                normalscale.x,
                normalscale.y,
                normalscale.z);
        } else {
            this.transform.localScale = new Vector3(
                this.transform.localScale.x,
                -normalscale.y,
                normalscale.z);
        }

        if((semiorautomatic ? Input.GetButton("Fire1") : Input.GetButtonDown("Fire1")) && (m_remainingCooldown < 0f)) {
            m_source.PlayOneShot(m_shootySound);
            
            // Spawn bullet at the tip of the weapon
            var tfm = this.transform.TransformPoint(this.transform.localPosition + new Vector3(weaponlength,0f,0f));

            GameObject current = Instantiate(projectile, tfm, Quaternion.identity);

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
