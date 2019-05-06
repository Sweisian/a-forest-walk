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
    public float dissolveLerpMultiplier = 7f;
    public float dissolveMin = 0f;
    public float dissolveMax = .6f;
    public string dissolveCode = "_DissolveAmount";

    public bool shouldSize = false;
    private float goalSize = 1;
    public float sizeLerpMultiplier = 3;
    public float sizeMin = .1f;
    public float sizeMax = 3f;

    private Color32 TreeBrownOne = new Color32(119, 54, 6, 255);
    private Color32 TreeBrownTwo = new Color32(139, 69, 19, 255);
    private Color32 TreeBrownThree = new Color32(160, 82, 45, 255);
    private Color32 TreeBrownFour = new Color32(48, 34, 4, 255);
    private Color32 TreeBrownFive = new Color32(38, 21, 7, 255);

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
        OnsetManager.OnOnset += ChangeSize;
        myMaterial.SetFloat(vertexCode, 0);
        myMaterial.SetFloat(dissolveCode, 0);
        goalSize = 1;
    }

    void OnDisable()
    {
        OnsetManager.OnOnset -= ChangeColor;
        OnsetManager.OnOnset -= ChangeVertex;
        OnsetManager.OnOnset -= ChangeDissolve;
        OnsetManager.OnOnset -= ChangeSize;
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

    private Color32 PickRandomBrown()
    {
        int randomInt = Random.Range(0, 5);
        Color32 returnColor = Color.white;

        switch (randomInt)
        {
            case 0:
                returnColor = TreeBrownOne;
                break;
            case 1:
                returnColor = TreeBrownTwo;
                break;
            case 2:
                returnColor = TreeBrownThree;
                break;
            case 3:
                returnColor = TreeBrownFour;
                break;
            case 4:
                returnColor = TreeBrownFive;
                break;
        }

        return returnColor;
    }

    void Awake()
    {
        Renderer rend = GetComponent<Renderer>();
        myMaterial = new Material(Shader.Find("Shader Graphs/MasterShader"));
        rend.material = myMaterial;

        if (gameObject.tag == "Bush")
            myMaterial.SetColor(colorCode, PickRandomGreen());
        else if (gameObject.tag == "Rock")
            myMaterial.SetColor(colorCode, RockGrey);
        else if (gameObject.tag == "Ground")
            myMaterial.SetColor(colorCode, GroundGreen);
        else if (gameObject.tag == "Trunk")
            myMaterial.SetColor(colorCode, PickRandomBrown());
        else if (gameObject.tag == "Log")
            myMaterial.SetColor(colorCode, PickRandomBrown());
        else if (gameObject.tag == "Plant")
            myMaterial.SetColor(colorCode, PickRandomGreen());
        else if (gameObject.tag == "Leaf")
            myMaterial.SetColor(colorCode, PickRandomGreen());
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
            dissolveAmount = Mathf.Lerp(dissolveAmount, 0, Time.deltaTime * dissolveLerpMultiplier);
            myMaterial.SetFloat(dissolveCode, dissolveAmount);
        }

        if (shouldSize)
        {
            float currSize = Mathf.Lerp(gameObject.transform.localScale.x, goalSize, Time.deltaTime * sizeLerpMultiplier);
            transform.localScale = new Vector3(currSize, currSize, currSize);
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

    private void ChangeSize()
    {
        if (shouldSize)
            goalSize = Random.Range(sizeMin, sizeMax);
    }
}
