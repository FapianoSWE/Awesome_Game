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
    Rigidbody2D rb;

    Gamestates gameStates = new Gamestates();

    public GameObject[] currentTeam;

	void Start ()
    {
        rb = GetComponent<Rigidbody2D>();
	}
	
	void Update ()
    {


        if (Input.GetKeyDown(KeyCode.R))
        {
            transform.position = GameObject.Find("Spawn").transform.position;
            rb.velocity = Vector2.zero;
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
    }

}
