using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShaderController : MonoBehaviour
{
    public Material myMaterial;
    public bool shouldColor = false;
    public string colorCode = "_Color";

    public bool shouldVertex = false;
    private float displacementAmount;
    public float displacementDelta = 2f;
    public float lerpMultiplier = 7f;
    public string vertexCode = "_Amount";

    public bool shouldDissolve = false;
    private float dissolveAmount;
    public float dissolveMultiplier = 7f;
    public float dissolveMin = 0f;
    public float dissolveMax = .6f;
    public string dissolveCode = "_DissolveAmount";

    private Color32 TreeBrown = new Color32(119, 54, 6, 255);
    private Color32 LeafGreenOne = new Color32(119, 116, 19, 255);
    private Color32 LeafGreenTwo = new Color32(31, 107, 0, 255);
    private Color32 LeafGreenThree = new Color32(116, 161, 82, 255);
    private Color32 RockGrey = new Color32(137, 137, 137, 255);
    private Color32 GroundGreen = new Color32(29, 53, 8, 255);


    void OnEnable()
    {
        OnsetManager.OnOnset += ChangeColor;
        OnsetManager.OnOnset += ChangeVertex;
        OnsetManager.OnOnset += ChangeDissolve;
        myMaterial.SetFloat(vertexCode, 0);
        myMaterial.SetFloat(dissolveCode, 0);
    }

    void OnDisable()
    {
        OnsetManager.OnOnset -= ChangeColor;
        OnsetManager.OnOnset -= ChangeVertex;
        OnsetManager.OnOnset -= ChangeDissolve;
    }

    private Color32 PickRandomGreen()
    {
        int randomInt = Random.Range(0, 3);
        Color32 returnColor = Color.white;

        switch (randomInt)
        {
            case 0:
                returnColor = LeafGreenOne;
                break;
            case 1:
                returnColor = LeafGreenTwo;
                break;
            case 2:
                returnColor = LeafGreenThree;
                break;
        }

        return returnColor;
    }

    void Start()
    {
        //Renderer rend = GetComponent<Renderer>();
        //myMaterial = new Material(Shader.Find("Shader Graphs/MasterShader"));
        //rend.material = myMaterial;

        if (gameObject.tag == "Bush")
            myMaterial.SetColor(colorCode, PickRandomGreen());
        else if (gameObject.tag == "Rock")
            myMaterial.SetColor(colorCode, RockGrey);
        else if (gameObject.tag == "Ground")
            myMaterial.SetColor(colorCode, GroundGreen);
    }


    // Update is called once per frame
    void Update()
    {
        if (shouldVertex)
        {
            displacementAmount = Mathf.Lerp(displacementAmount, 0, Time.deltaTime * lerpMultiplier);
            myMaterial.SetFloat(vertexCode, displacementAmount);
        }
            
        if (shouldDissolve)
        {
            dissolveAmount = Mathf.Lerp(dissolveAmount, 0, Time.deltaTime * dissolveMultiplier);
            myMaterial.SetFloat(dissolveCode, dissolveAmount);
        }
        
    }

    void ChangeVertex()
    {
        if(shouldVertex)
        displacementAmount += displacementDelta;
    }

    void ChangeDissolve()
    {
        if (shouldDissolve)
        dissolveAmount = dissolveMax;
    }

    private void ChangeColor()
    {   
        if(shouldColor)
        myMaterial.SetColor(colorCode, Random.ColorHSV());
    }
}
