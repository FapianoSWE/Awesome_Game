using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour {

    public Material mat;

    void OnTriggerEnter(Collider c)
    {
        if (c.transform.parent.tag == "Player")
        {
            c.GetComponent<PlayerController>().currentCheckpoint = transform;
            mat.color = Color.blue;
        }
    }
}
