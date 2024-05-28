using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalLevel : MonoBehaviour
{
    public GameObject Win, Pause,PauseButton;
    public static bool Won;


    // Start is called before the first frame update
    void Start()
    {
        Win.SetActive(false);
        Pause.SetActive(false);
        PauseButton.SetActive(true);
        Won = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "PlayerCol")
        {
            Invoke("WinGame", 2f);
        }
    }

    public void PauseGame()
    {
        Pause.SetActive(true);
        PauseButton.SetActive(false);
    }
    public void ResumeGame()
    {
        Pause.SetActive(false);
        PauseButton.SetActive(true);
    }

    public void WinGame()
    {
        Win.SetActive(true);
        PauseButton.SetActive(false);
        Time.timeScale = 0f;
        Won = true;
    }
}
