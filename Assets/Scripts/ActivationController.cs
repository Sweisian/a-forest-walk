using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivationController : MonoBehaviour
{
    public enum Group { Zero, One, Two, Three, Four, Five, Six, Seven, Eight, Nine };
    [SerializeField] public Group myGroup;


    private void OnValidate()
    {
        //Renderer rend = gameObject.GetComponent<Renderer>();

        //if (rend)
        //{
        //    ColorSwitcher(rend, myGroup);
        //}
        //else
        //{
        //    foreach (Renderer childRend in gameObject.GetComponentsInChildren<Renderer>())
        //    {
        //        ColorSwitcher(childRend, myGroup);
        //    }
        //}
    }

    private void ColorSwitcher(Renderer rend, Group thisGroup)
    {
        switch (myGroup)
        {
            case ActivationController.Group.Zero:
                rend.sharedMaterial.color = Color.magenta;
                break;
            case ActivationController.Group.One:
                rend.sharedMaterial.color = Color.blue;
                break;
            case ActivationController.Group.Two:
                rend.sharedMaterial.color = Color.cyan;
                break;
            case ActivationController.Group.Three:
                rend.sharedMaterial.color = Color.green;
                break;
            case ActivationController.Group.Four:
                rend.sharedMaterial.color = Color.yellow;
                break;
        }
    }
}
