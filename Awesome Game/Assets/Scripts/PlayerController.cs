using UnityEngine;
using System.Collections;

public enum Gamestates
{
    dead,
    alive
}

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour {


    public float speed = 10f;
    public float jumpHeight = 20f;
    public bool grounded = false;
    public Vector2 currentVelocity;
    Rigidbody2D rb;

    Gamestates gameStates = new Gamestates();
    
	void Start () {
        rb = GetComponent<Rigidbody2D>();
	}
	
	void Update () {

        currentVelocity = rb.velocity;

        if (Input.GetKeyDown(KeyCode.R))
        {
            transform.position = GameObject.Find("Spawn").transform.position;
            rb.velocity = Vector2.zero;
        }
    }
    void FixedUpdate()
    {
        Movement();
    }

    void Movement()
    {
        rb.velocity = new Vector2(Input.GetAxis("Horizontal") * speed, rb.velocity.y);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpHeight);
        }
        
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.tag == "HostileTerrain")
        {
            gameStates = Gamestates.dead;
        }
    }

}
