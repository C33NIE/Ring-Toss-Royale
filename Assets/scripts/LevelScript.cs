using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun; 

public class LevelScript : MonoBehaviour
{

    public Transform player;
    public float lerpSpeed = 1.0f, pos;
    bool levelUp = false;
    Vector3 destination;


    // Start is called before the first frame update
    void Start()
    {
        destination = player.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (levelUp)
        {
            player.position = Vector3.Lerp(player.position, destination, Time.deltaTime * lerpSpeed);
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        StartCoroutine(DelayedCheck(other));
    }

    IEnumerator DelayedCheck(Collider other)
    {
        yield return new WaitForSeconds(2.0f); // Wait for 1 second
        if (other.gameObject.tag == "PlayerCol")
        {
            yield return new WaitForSeconds(1.0f); // Wait for 1 second
            destination = new Vector3(player.position.x, player.position.y - 7.6f, player.position.z);
            levelUp = true;
            Debug.Log("Score");
            StartCoroutine(DelayedLerp());
        }
    }

    IEnumerator DelayedLerp()
    {

        yield return new WaitForSeconds(5.0f); // Wait for 5 second
        levelUp = false;
        PhotonNetwork.Destroy(this.gameObject);

    }
}
