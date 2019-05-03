using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorChanger : MonoBehaviour
{
    public Material myMaterial;

    void OnEnable()
    {
        OnsetManager.OnOnset += ChangeColor;
    }

    void OnDisable()
    {
        OnsetManager.OnOnset -= ChangeColor;
    }

    void Start()
    {
        myMaterial = GetComponent<Renderer>().material;
    }

    private void ChangeColor()
    {
        myMaterial.SetColor("Color_9565E91F", Random.ColorHSV());
    }
   
}


