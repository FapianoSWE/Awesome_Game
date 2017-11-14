using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

    public GameObject[] playerCubes;
    public GameObject currentCube;

    public bool switched = false;

	void Start ()
    {
        playerCubes = new GameObject[4];

        playerCubes[0] = GameObject.Find("Happy_Cube");
        playerCubes[1] = GameObject.Find("Sad_Cube");
        playerCubes[2] = GameObject.Find("Angry_Cube");
        playerCubes[3] = GameObject.Find("Sleepy_Cube");

        currentCube = GameObject.Find("Happy_Cube");
	}
	


	void Update ()
    {
        foreach (GameObject cube in playerCubes)
        {
            if(cube != currentCube)
            {
                cube.GetComponent<PlayerController>().enabled = false;
                cube.GetComponent<AiController>().enabled = true;
            }
            if(cube == currentCube)
            {
                cube.GetComponent<PlayerController>().enabled = true;
                cube.GetComponent<AiController>().enabled = false;
            }
        }

        if(Input.GetKeyUp(KeyCode.G))
        {
            switched = false;
        }

        for (int i = 0; i < playerCubes.Length; i++)
        {
            if(playerCubes[i] == currentCube && Input.GetKeyDown(KeyCode.G) && currentCube == playerCubes[playerCubes.Length - 1] && !switched)
            {
                currentCube = playerCubes[0];
                switched = true;
            }
            else if (playerCubes[i] == currentCube && Input.GetKeyDown(KeyCode.G) && !switched)
            {
                currentCube = playerCubes[i + 1];
                switched = true;
            }
        }
	}
}
