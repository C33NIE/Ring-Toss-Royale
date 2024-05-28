using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class SpawnBall : MonoBehaviour {

	public static SpawnBall Instance;
	[SerializeField]
	GameObject ball;
    public Transform Player;
    private void Start()
    {

    }

    public void Spawn(Transform T)
	{
		PhotonNetwork.Instantiate (ball.name, T.position, Quaternion.identity);
	}
}
