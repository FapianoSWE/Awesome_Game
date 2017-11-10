using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animations : MonoBehaviour {

    public Animator anim;

	void Start ()
    {
        anim = GetComponent<Animator>();
	}
	

	void Update ()
    {
        foreach (GameObject g in GameObject.FindGameObjectsWithTag("Player"))
        {
                g.GetComponentInChildren<Animator>().SetBool("IsMoving", g.GetComponent<PlayerController>().isMoving);
        }
	}
}
