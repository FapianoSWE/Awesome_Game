using UnityEngine;

public class CameraFollow : MonoBehaviour {

    public Transform target;
    public Vector3 offset;
    public GameController gameController;

    void Start()
    {
        gameController = GameObject.Find("Game_Controller").GetComponent<GameController>();
        
    }

    void Update()
    {
        target = gameController.currentCube.transform;
        transform.position = new Vector3(target.position.x, target.position.y, offset.z);
    }


}
