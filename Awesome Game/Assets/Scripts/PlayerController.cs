using UnityEngine;
using System.Collections;

public enum Gamestates
{
    alive,
    dead
}


public class PlayerController : MonoBehaviour {


    public float speed = 10f;
    public float jumpHeight = 20f;
    public bool grounded = false;
    public bool isMoving = false;
    public bool turnedRight = true;
    public Vector2 currentVelocity;


    public Rigidbody2D rb;
    public Transform currentCheckpoint;

    Gamestates gameStates = new Gamestates();
    

	void Start ()
    {
        rb = GetComponent<Rigidbody2D>();
	}
	
	void Update ()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            Respawn();
        }
        if(rb.velocity.y > 0 || rb.velocity.y < 0)
        {
            grounded = false;
        }
        if(rb.velocity.x < 0)
        {
            turnedRight = false;
            isMoving = true;
        }
        if(rb.velocity.x > 0)
        {
            turnedRight = true;
            isMoving = true;
        }
        if(turnedRight)
        {
            GetComponentInChildren<Transform>().localScale = new Vector3
                (1, GetComponentInChildren<Transform>().localScale.y, GetComponentInChildren<Transform>().localScale.z);
        }
        else if(!turnedRight)
        {
            GetComponentInChildren<Transform>().localScale = new Vector3
                (-1, GetComponentInChildren<Transform>().localScale.y, GetComponentInChildren<Transform>().localScale.z);
        }
        if(rb.velocity.x == 0)
        {
            isMoving = false;
        }
        print(currentCheckpoint);
    }

    void FixedUpdate()
    {
        if(gameStates == Gamestates.alive)
        {
            Movement();
        }
    }

    void Movement()
    {
        rb.velocity = new Vector2(Input.GetAxis("Horizontal") * speed, rb.velocity.y);

        if (Input.GetKeyDown(KeyCode.UpArrow) && grounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpHeight);
        }
    }

    void Respawn()
    {
        if(currentCheckpoint != null)
        {
            transform.position = currentCheckpoint.position;
        }
        else if(currentCheckpoint == null)
        {
            transform.position = GameObject.Find("Spawn").GetComponent<Transform>().position;
        }
        rb.velocity = Vector2.zero;
        gameStates = Gamestates.alive;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.tag == "HostileTerrain")
        {
            gameStates = Gamestates.dead;
        }
        if(collision.collider.tag == "Terrain" && rb.velocity.y == 0)
        {
            grounded = true;
        }
        if(collision.collider.tag == "Player")
        {
            Physics2D.IgnoreCollision(collision.collider, collision.otherCollider, true);
        }
    }

    void OnTriggerEnter(Collider c)
    {
        print("Triggered");
        if(c.transform.parent.tag == "Checkpoint")
        {
            currentCheckpoint = c.transform;
            print("Triggered by checkpoint");
        }
    }

}
