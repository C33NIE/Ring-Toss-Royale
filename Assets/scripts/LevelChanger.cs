using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelChanger : MonoBehaviour
{
    public GameObject Win, Pause,PauseButton;
    public Transform player;
    public float lerpSpeed = 1.0f, pos;
    bool levelUp = false;
    Vector3 destination;


    // Start is called before the first frame update
    void Start()
    {
        Win.SetActive(false);
        Pause.SetActive(false);
        PauseButton.SetActive(true);
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
        if (other.gameObject.tag == "Pole")
        {
            yield return new WaitForSeconds(1.0f); // Wait for 1 second
            destination = new Vector3(player.position.x, player.position.y - 7.6f, player.position.z);
            levelUp = true;
            Debug.Log("Score");
            StartCoroutine(DelayedLerp());
        }
        else if (other.gameObject.tag =="FinalPole")
        {
            Win.SetActive(true);
            PauseButton.SetActive(false);
            Time.timeScale = 0f;
        }    
    }

    IEnumerator DelayedLerp()
    {
        
        yield return new WaitForSeconds(5.0f); // Wait for 1 second
    
        levelUp = false;
        
    }
    public void PauseGame()
    {
        Pause.SetActive(true);
        Time.timeScale = 0f;
        PauseButton.SetActive(false);
    }
    public void ResumeGame()
    {
        Pause.SetActive(false);
        Time.timeScale = 1f;
        PauseButton.SetActive(true);
    }
}
