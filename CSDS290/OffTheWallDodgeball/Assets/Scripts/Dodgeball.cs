using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dodgeball : MonoBehaviour
{
    private Rigidbody2D rb;
    Vector2 lastVelocity;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    //Not sure if start or Awake is the best when initializing a ball through
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

    }

    public void Throw(Vector2 direction)
    {
        rb.AddForce(direction);
    }

    // Update is called once per frame
    void Update()
    {
        lastVelocity = rb.velocity;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        
        GameObject hitObject = collision.gameObject;
        if(hitObject.GetComponent<PlayerMovement>() != null)
        {
            PlayerMovement p = hitObject.GetComponent<PlayerMovement>();
            Destroy(this);
            //DestroyBall
        }
        else if (hitObject.GetComponent<Enemy>() != null)
        {
            Enemy e = hitObject.GetComponent<Enemy>();
            e.alive = false;
            Destroy(this);
            //Destroy Ball
        } 
        else
        { 
            var speed = lastVelocity.magnitude; //We can reduce this if we want
            var direction = Vector3.Reflect(lastVelocity.normalized, collision.contacts[0].normal);

            rb.velocity = direction * speed;
        }

    }
}

