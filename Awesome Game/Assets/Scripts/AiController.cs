using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiController : MonoBehaviour {

    GameController gameController;
    

    void Start ()
    {
        gameController = GameObject.Find("Game_Controller").GetComponent<GameController>();
	}
	

	void Update ()
    {
        
	}
}
