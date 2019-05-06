using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyBoxController : MonoBehaviour
{
    private OnsetManager onsetManager;
    private bool shouldColor;

    void OnEnable()
    {
        OnsetManager.OnOnset += ChangeColor;
    }

    void OnDisable()
    {
        OnsetManager.OnOnset -= ChangeColor;
    }

    // Start is called before the first frame update
    void Start()
    {
        onsetManager = gameObject.GetComponent<OnsetManager>();
    }

    // Update is called once per frame
    void Update()
    {
        var actTimes = onsetManager.onsetActivationTimes;
        if (actTimes[actTimes.Count - 1] < onsetManager.cur_time)
            shouldColor = true;
    }

    private void ChangeColor()
    {
        if (shouldColor)
        {
            if (RenderSettings.skybox.HasProperty("_Tint"))
                RenderSettings.skybox.SetColor("_Tint", Random.ColorHSV());
            else if (RenderSettings.skybox.HasProperty("_SkyTint"))
                RenderSettings.skybox.SetColor("_SkyTint", Random.ColorHSV());
            else
                Debug.Log("ERROR, CAN'T FIND SKYBOX TINT");
        }
    }
}
