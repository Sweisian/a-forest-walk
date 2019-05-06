using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivationController : MonoBehaviour
{
    public enum Group { Zero, One, Two, Three, Four, Five, Six, Seven, Eight, Nine };
    [SerializeField] public Group myGroup;


    private void OnValidate()
    {
        Renderer rend = gameObject.GetComponent<Renderer>();

        if (rend)
        {
            ColorSwitcher(rend, myGroup);
        }
        else
        {
            foreach (Renderer childRend in gameObject.GetComponentsInChildren<Renderer>())
            {
                ColorSwitcher(childRend, myGroup);
            }
        }
    }

    private void ColorSwitcher(Renderer rend, Group thisGroup)
    {
        switch (myGroup)
        {
            case ActivationController.Group.Zero:
                rend.material.color = Color.magenta;
                break;
            case ActivationController.Group.One:
                rend.material.color = Color.blue;
                break;
            case ActivationController.Group.Two:
                rend.material.color = Color.cyan;
                break;
            case ActivationController.Group.Three:
                rend.material.color = Color.green;
                break;
            case ActivationController.Group.Four:
                rend.material.color = Color.yellow;
                break;
            case ActivationController.Group.Five:
                rend.material.color = Color.grey;
                break;
            case ActivationController.Group.Six:
                rend.material.color = Color.red;
                break;
            case ActivationController.Group.Seven:
                rend.material.color = Color.black;
                break;
            case ActivationController.Group.Eight:
                rend.material.color = Color.white;
                break;
            case ActivationController.Group.Nine:
                rend.material.color = new Color32(139, 69, 19, 255);
                break;
        }
    }
}
