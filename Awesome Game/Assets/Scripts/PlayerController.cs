﻿using UnityEngine;
using System.Collections;

public enum Gamestates
{
    dead,
    alive
}
public enum Characters
{
    happy,
    sad,
    sleepy,
    angery
}



public class PlayerController : MonoBehaviour {

    public GameObject currentCheckpoint;

    public float speed = 10f;
    public float jumpHeight = 10f;
    public float angeryJump = 5f;
    public bool grounded = false;
    public bool canSetJump;
    public Vector2 currentVelocity;
    Rigidbody2D rb;

    public Gamestates gameStates = new Gamestates();


    public Characters characters = new Characters();

    void Start ()
    {
        rb = GetComponent<Rigidbody2D>();
        characters = Characters.happy;
        gameStates = Gamestates.alive;

	}
	
	void Update ()
    {
        if(Input.GetKeyDown(KeyCode.K))
        {
            gameStates = Gamestates.dead;
        }
        if(gameStates == Gamestates.dead)
        {
            Respawn();
        }
        currentVelocity = rb.velocity;

        if (Input.GetKeyDown(KeyCode.R))
        {
            transform.position = GameObject.Find("Spawn").transform.position;
            rb.velocity = Vector2.zero;
        }
        if(rb.velocity.y > 0 || rb.velocity.y < 0)
        {
            grounded = false;
        }
        if(characters == Characters.happy)
        {
            if(canSetJump)
            {
                jumpHeight *= 2;
                canSetJump = false;
            }
        }
        else if(characters == Characters.angery)
        {
            if(Input.GetKeyDown(KeyCode.Space))
            {
                AngerySkill();
            }
        }
    }

    void FixedUpdate()
    {
        Movement();
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
        if(collision.collider.tag == "checkpoint")
        {
            currentCheckpoint = collision.gameObject;
            
        }
    }

    void AngerySkill()
    {
        rb.velocity = new Vector2(0, angeryJump);
        if(rb.velocity.y == 0 && !grounded)
        {
            rb.velocity = new Vector2(0, -angeryJump * 2);
        }
    }
    
    void Respawn()
    {
        rb.velocity = Vector2.zero;
        if(currentCheckpoint != null)
        {
            transform.position = currentCheckpoint.transform.position;
            gameStates = Gamestates.alive;
        }
        else if(currentCheckpoint == null)
        {
            transform.position = GameObject.Find("Spawn").transform.position;
        }
            
    }
}
