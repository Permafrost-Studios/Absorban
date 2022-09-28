using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxBackground : MonoBehaviour
{
    [SerializeField] private Camera cam;
    public float parallaxValue;

    [SerializeField] private Vector2 length;
    [SerializeField] private Vector2 offsetpos;

    // Start is called before the first frame update
    void Start() {
        length = GetComponentInChildren<SpriteRenderer>().bounds.size;
        offsetpos = cam.transform.localPosition;
    }

    // Update is called once per frame
    void Update() {
        Vector2 relativepos = cam.transform.localPosition*parallaxValue;
        // Out of bounds left = negative value and vice-versa
        Vector2 temp = (Vector2) cam.transform.position-(relativepos+offsetpos);

        if(temp.x>(length.x/2)) {
            offsetpos.x += length.x;
        } else if (temp.x<(-length.x/2)) {
            offsetpos.x -= length.x;
        }

        transform.localPosition = relativepos+offsetpos;  
    }
}
