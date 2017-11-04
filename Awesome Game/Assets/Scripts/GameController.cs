using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

    public GameObject[] playerCubes;
    public GameObject currentCube;

	void Start ()
    {
        playerCubes = GameObject.FindGameObjectsWithTag("Player");
        currentCube = playerCubes[playerCubes.Length - 1];
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
        for (int i = 0; i < playerCubes.Length; i++)
        {
            if (playerCubes[i] == currentCube && Input.GetKeyDown(KeyCode.G))
            {
                if(i < playerCubes.Length - 1)
                {
                    currentCube = playerCubes[i + 1];
                    print(playerCubes[i]);
                    print(i);
                }
                else if(i == playerCubes.Length - 1)
                {
                    currentCube = playerCubes[0];
                    print(playerCubes[i]);
                    print(i);

                }
            }
        }
	}
}
