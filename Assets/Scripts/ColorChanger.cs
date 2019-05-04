using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorChanger : MonoBehaviour
{
    public Material myMaterial;
    public string colorCode;

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
        myMaterial.SetColor(colorCode, Random.ColorHSV());
    }
   
}


