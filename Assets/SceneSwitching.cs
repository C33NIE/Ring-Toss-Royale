using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneSwitching : MonoBehaviour
{
   
    // Start is called before the first frame update
    void Start()
    {

    }

    private void Update()
    {
        
    }


    public void MainMenu()
    {
        SceneManager.LoadScene(1);
    }
    public void Retry()
    {
        Time.timeScale = 1.0f; 
        SceneManager.LoadScene(2);
    }

}