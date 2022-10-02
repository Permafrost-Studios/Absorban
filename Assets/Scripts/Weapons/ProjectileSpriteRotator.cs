using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileSpriteRotator : MonoBehaviour
{
    private Rigidbody2D body;

    void Start() {
        body = this.gameObject.transform.parent.GetComponent<Rigidbody2D>();
        // Debug.Log("spawned bullet");
    }
    
    void Update() {
        Quaternion rot = new Quaternion();
        rot.eulerAngles = new Vector3(
            0f,
            0f,
            (Mathf.Atan2(body.velocity.y, body.velocity.x)*Mathf.Rad2Deg)-90f
        );

        this.transform.localRotation = rot;        
    }
}
