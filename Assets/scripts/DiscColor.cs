using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiscColor : MonoBehaviour
{
    // Set the materials in the inspector
    public Material[] myMaterials = new Material[10];
    Renderer discRend;
    int discMaterial;

    // Use this for initialization
    void Start()
    {
        discMaterial = PlayerPrefs.GetInt("selectedDisc");
        discRend = GetComponent<Renderer>();
        discRend.sharedMaterial = myMaterials[discMaterial];
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
