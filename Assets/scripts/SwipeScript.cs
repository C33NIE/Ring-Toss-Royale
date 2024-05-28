using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SwipeScript : MonoBehaviourPunCallbacks {


	Vector2 startPos, endPos, direction; // touch start position, touch end position, swipe direction
	float touchTimeStart, touchTimeFinish, timeInterval; // to calculate swipe time to sontrol throw force in Z direction

    
    float throwForceInX = 1f; // to control throw force in X direction

    
	float throwForceInY = 0.6f; // to control throw force in Y direction

    
	float throwForceInZ = 50f; // to control throw force in Z direction

	Rigidbody rb;

    SpawnBall spawnBallScript; // Reference to SpawnBall script

    [SerializeField]
    Transform [] myPositions = new Transform[4];
    int P;

    private bool EastOrWest= false;



    void Start()
	{
        if (photonView.IsMine)
        {

            if (transform.position == myPositions[0].position)
            {
                throwForceInX = -1f;
                throwForceInZ = -260f;
                EastOrWest = false;
                Debug.Log("North");
                P = 0;
            }
            else if (transform.position == myPositions[1].position)
            {
                throwForceInX = 1f;
                throwForceInZ = 260f;
                EastOrWest = false;
                Debug.Log("South");
                P = 1;
            }
            else if (transform.position == myPositions[2].position)
            {
                throwForceInX = -260f;
                throwForceInZ = 1f;
                EastOrWest = true;
                Debug.Log("East");
                P = 2;
            }
            else if (transform.position == myPositions[3].position)
            {
                throwForceInX = 260f;
                throwForceInZ = -1f;
                EastOrWest = true;
                Debug.Log("West");
                P = 3;
            }

            rb = GetComponent<Rigidbody>();
            // Get the SpawnBall script instance
            spawnBallScript = FindObjectOfType<SpawnBall>();
        }
    }

	// Update is called once per frame
	void Update () {


        if (photonView.IsMine)
        {

            // if you touch the screen
            if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
            {

                // getting touch position and marking time when you touch the screen
                touchTimeStart = Time.time;
                startPos = Input.GetTouch(0).position;
            }

            // if you release your finger
            if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended)
            {

                // marking time when you release it
                touchTimeFinish = Time.time;

                // calculate swipe time interval 
                timeInterval = touchTimeFinish - touchTimeStart;

                // getting release finger position
                endPos = Input.GetTouch(0).position;

                // calculating swipe direction in 2D space
                direction = startPos - endPos;

                // add force to balls rigidbody in 3D space depending on swipe time, direction and throw forces
                rb.isKinematic = false;
                if (EastOrWest)
                {
                    rb.AddForce(throwForceInX / timeInterval, -direction.y * throwForceInY, -direction.x * throwForceInZ);
                }
                else
                {
                    rb.AddForce(-direction.x * throwForceInX, -direction.y * throwForceInY, throwForceInZ / timeInterval);
                }

                Invoke("CallSpawnFunction", 4.0f);

                // Destroy ball in 4 seconds
                Invoke("DestroyDisc", 4.1f);
            }
        }
	}

    public void DestroyDisc()
    {
        if (photonView.IsMine) 
        {
            PhotonNetwork.Destroy(gameObject);
        }
    }

    void CallSpawnFunction()
    {
        if (photonView.IsMine)
        {
            spawnBallScript.Spawn(myPositions[P]);
        }
    }

    

    

}
