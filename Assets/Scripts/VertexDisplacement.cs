using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VertexDisplacement : MonoBehaviour
{
    public Material myMaterial;
    public float displacementDelta = .5f;
    private float displacementAmount;
    public float lerpMultiplier = 2f;


    void OnEnable()
    {
        OnsetManager.OnOnset += ChangeVertex;
        myMaterial.SetFloat("_Amount", 0);
    }

    void OnDisable()
    {
        OnsetManager.OnOnset -= ChangeVertex;
    }

    void ChangeVertex()
    {
        displacementAmount += displacementDelta;
    }

    // Update is called once per frame
    void Update()
    {
        displacementAmount = Mathf.Lerp(displacementAmount, 0, Time.deltaTime * lerpMultiplier);
        myMaterial.SetFloat("_Amount", displacementAmount);  
    }
}
