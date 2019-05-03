using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System.IO;


public class OnsetManager : MonoBehaviour
{
    public AudioClip song;
    public AudioClip click;
    private AudioSource audioSource;

    public string json_filepath = "/python_materials/output/Seven_Nation_Army.json";
    private float cur_time = 0;
    private float[] onset_array;
    private int onset_idx = 0;
    private bool shouldPlay = false;
    Renderer rend;

    public delegate void OnsetAction();
    public static event OnsetAction OnOnset;

    // Start is called before the first frame update
    void Start()
    {
        //Debug.Log(onset_array);
        //rend = GetComponent<Renderer>();
        LoadGameData();
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (Input.GetKeyDown("f"))
        {
            audioSource.PlayOneShot(song);
            cur_time = 0;
            shouldPlay = true;
            Debug.Log("PLAYING SONG NOW");
        }

        if (shouldPlay)
        {
            ColorChanger();
        }
    }

    public class MusicFeatures
    {
        public float[] onsets;
    }

    public static MusicFeatures CreateFromJSON(string jsonString)
    {
        return JsonUtility.FromJson<MusicFeatures>(jsonString);
    }

    private void LoadGameData()
    {
        // Path.Combine combines strings into a file path
        // Application.StreamingAssets points to Assets/StreamingAssets in the Editor, and the StreamingAssets folder in a build
        //string filePath = Path.Combine(Application.streamingAssetsPath, json_filepath);
        string filePath = Application.streamingAssetsPath + json_filepath;

        Debug.Log(Application.streamingAssetsPath);
        Debug.Log(filePath);

        if (File.Exists(filePath))
        {
            // Read the json from the file into a string
            string dataAsJson = File.ReadAllText(filePath);
            // Pass the json to JsonUtility, and tell it to create a GameData object from it
            MusicFeatures loadedData = JsonUtility.FromJson<MusicFeatures>(dataAsJson);

            // Retrieve the allRoundData property of loadedData
            onset_array = loadedData.onsets;
        }
        else
        {
            Debug.LogError("Cannot load game data!");
        }
    }

    void ColorChanger()
    {
        cur_time += Time.deltaTime;

        if (onset_array[onset_idx] < cur_time)
        {
            audioSource.PlayOneShot(click, .4f);
            OnOnset();
            onset_idx++;
        }
       
    }
}
