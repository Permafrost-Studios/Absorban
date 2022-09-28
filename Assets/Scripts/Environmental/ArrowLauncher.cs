using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowLauncher : MonoBehaviour
{
    public enum Direction {Up, Down, Left, Right};

    public Direction arrowDirection;
    public float arrowForce;

    void OnTriggerEnter2D(Collider2D collider) 
    {
        switch (arrowDirection)
        {
            case Direction.Up:
                collider.gameObject.GetComponent<Rigidbody2D>().AddForce(arrowForce * Vector2.up);
                break;
            case Direction.Down:
                collider.gameObject.GetComponent<Rigidbody2D>().AddForce(arrowForce * Vector2.down);
                break;
            case Direction.Left:
                collider.gameObject.GetComponent<Rigidbody2D>().AddForce(arrowForce * Vector2.left);
                break;
            case Direction.Right:
                collider.gameObject.GetComponent<Rigidbody2D>().AddForce(arrowForce * Vector2.right);
                break;
            default:
                collider.gameObject.GetComponent<Rigidbody2D>().AddForce(arrowForce * Vector2.up);
                break;
        }   
    }
}
