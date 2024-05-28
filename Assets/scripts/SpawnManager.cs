using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SpawnManager : MonoBehaviour
{

    public Transform[] SpawnPoints;

    public static SpawnManager Instance;

    private void Awake()
    {
        Instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        foreach (Transform spawn in SpawnPoints) 
        {
            spawn.gameObject.SetActive(false);
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Transform GetSpawnPoint(int i)
    {
        
        return SpawnPoints[i];
        
    }
}
