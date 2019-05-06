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
    public float cur_time = 0;
    private float[] onset_array;
    private int onset_idx = 0;
    private bool shouldPlay = false;
    Renderer rend;

    public delegate void OnsetAction();
    public static event OnsetAction OnOnset;

    public List<List<GameObject>> masterObjectList = new List<List<GameObject>>(10);

    public List<float> onsetActivationTimes;
    private int onsetActivationTimesIdx = 0;

    [System.Serializable]
    public struct ActivationInfo
    {
        public float colorProb;
        public float vertexProb;
        public float dissolveProb;
        public float sizeProb;
    }

    [SerializeField] private ActivationInfo groupZeroInfo;
    [SerializeField] private ActivationInfo groupOneInfo;
    [SerializeField] private ActivationInfo groupTwoInfo;
    [SerializeField] private ActivationInfo groupThreeInfo;
    [SerializeField] private ActivationInfo groupFourInfo;
    [SerializeField] private ActivationInfo groupFiveInfo;
    [SerializeField] private ActivationInfo groupSixInfo;
    [SerializeField] private ActivationInfo groupSevenInfo;
    [SerializeField] private ActivationInfo groupEightInfo;
    [SerializeField] private ActivationInfo groupNineInfo;

    // Start is called before the first frame update
    void Start()
    {
        //Debug.Log(onset_array);
        //rend = GetComponent<Renderer>();
        LoadGameData();
        audioSource = GetComponent<AudioSource>();

        for(int i = 0; i < 10; i++)
        {
            masterObjectList.Add(new List<GameObject>());
        }
        Debug.Log(masterObjectList);
        

        GameObject[] allObjects = UnityEngine.Object.FindObjectsOfType<GameObject>();
        foreach (GameObject go in allObjects)
        {
            ActivationController myAC = go.GetComponent<ActivationController>();
            ShaderController mySC = go.GetComponent<ShaderController>();

            if(go.tag == "Tree" && myAC)
            {
                HandleTreeActivationGroup(go, myAC);
                continue;
            }

            if (myAC && mySC && go.activeInHierarchy)
            {
                HandleActivationGroup(go, myAC, mySC);
            }
        }

        Debug.Log("GROUP ZER0:");
        foreach (GameObject go in masterObjectList[0])
            Debug.Log(go);

        Debug.Log("GROUP One:");
        foreach (GameObject go in masterObjectList[1])
            Debug.Log(go);

        //Debug.Log("GROUP Two:");
        //foreach (GameObject go in masterObjectList[2])
        //    Debug.Log(go);


    }

    private void HandleTreeActivationGroup(GameObject go, ActivationController TreeLevelAC)
    {
        foreach(ShaderController ChildSC in go.GetComponentsInChildren<ShaderController>())
        {
            HandleActivationGroup(ChildSC.gameObject, TreeLevelAC, ChildSC);
        }
    }

    private void HandleActivationGroup(GameObject go, ActivationController myAC, ShaderController mySC)
    {
        mySC.shouldColor = false;
        mySC.shouldVertex = false;
        mySC.shouldDissolve = false;
        mySC.shouldSize = false;

        switch (myAC.myGroup)
        {
            case ActivationController.Group.Zero:
                masterObjectList[0].Add(go);
                break;
            case ActivationController.Group.One:
                masterObjectList[1].Add(go);
                break;
            case ActivationController.Group.Two:
                masterObjectList[2].Add(go);
                break;
            case ActivationController.Group.Three:
                masterObjectList[3].Add(go);
                break;
            case ActivationController.Group.Four:
                masterObjectList[4].Add(go);
                break;
            case ActivationController.Group.Five:
                masterObjectList[5].Add(go);
                break;
            case ActivationController.Group.Six:
                masterObjectList[6].Add(go);
                break;
            case ActivationController.Group.Seven:
                masterObjectList[7].Add(go);
                break;
            case ActivationController.Group.Eight:
                masterObjectList[8].Add(go);
                break;
            case ActivationController.Group.Nine:
                masterObjectList[9].Add(go);
                break;

        }
    }

    void Update()
    {
        if (Input.GetKeyDown("f"))
        {
            audioSource.PlayOneShot(song);
            cur_time = 0;
            onsetActivationTimesIdx = 0;
            shouldPlay = true;
            Debug.Log("PLAYING SONG NOW");
        }

        if (shouldPlay)
        {
            OnsetActivator();
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

    private void ActivateShaders(int groupNum)
    {
        List<GameObject> myGroup = new List<GameObject>();
        ActivationInfo AI = new ActivationInfo();

        switch (groupNum)
        {
            case 0:
                myGroup = masterObjectList[groupNum];
                AI = groupZeroInfo;
                break;
            case 1:
                myGroup = masterObjectList[groupNum];
                AI = groupOneInfo;
                break;
            case 2:
                myGroup = masterObjectList[groupNum];
                AI = groupTwoInfo;
                break;
            case 3:
                myGroup = masterObjectList[groupNum];
                AI = groupThreeInfo;
                break;
            case 4:
                myGroup = masterObjectList[groupNum];
                AI = groupFourInfo;
                break;
            case 5:
                myGroup = masterObjectList[groupNum];
                AI = groupFiveInfo;
                break;
            case 6:
                myGroup = masterObjectList[groupNum];
                AI = groupSixInfo;
                break;
            case 7:
                myGroup = masterObjectList[groupNum];
                AI = groupSevenInfo;
                break;
            case 8:
                myGroup = masterObjectList[groupNum];
                AI = groupEightInfo;
                break;
            case 9:
                myGroup = masterObjectList[groupNum];
                AI = groupNineInfo;
                break;
        }

        foreach (GameObject go in myGroup)
        {
            ActivateSingleShader(go, AI);
        }
    }

    private void ActivateSingleShader(GameObject go, ActivationInfo AI)
    {
        List<bool> actList = GetActivations(AI);
        bool shouldColor = actList[0];
        bool shouldVertex = actList[1];
        bool shouldDissolve = actList[2];
        bool shouldSize = actList[3];

        if (shouldColor) go.GetComponent<ShaderController>().shouldColor = true;
        if (shouldVertex) go.GetComponent<ShaderController>().shouldVertex = true;
        if (shouldDissolve) go.GetComponent<ShaderController>().shouldDissolve = true;
        if (shouldSize) go.GetComponent<ShaderController>().shouldSize = true;
    }

    private List<bool> GetActivations(ActivationInfo AI)
    {
        bool shouldColor = false;
        bool shouldVertex = false;
        bool shouldDissolve = false;
        bool shouldSize = false;

        if (Random.Range(1, 101) <= AI.colorProb) shouldColor = true;
        if (Random.Range(1, 101) <= AI.vertexProb) shouldVertex = true;
        if (Random.Range(1, 101) <= AI.dissolveProb) shouldDissolve = true;
        if (Random.Range(1, 101) <= AI.sizeProb) shouldSize = true;

        if(shouldVertex && shouldDissolve)
        { 
            if (Random.Range(1, 101) <= 50)
                shouldVertex = false;
            else
                shouldDissolve = false;
        }

        return new List<bool> { shouldColor, shouldVertex, shouldDissolve, shouldSize };
    }


    void OnsetActivator()
    {
        cur_time += Time.deltaTime;

        if (onset_array[onset_idx] < cur_time)
        {
            //audioSource.PlayOneShot(click, .4f);
            OnOnset();
            onset_idx++;
        }

        //var nearest = array.MinBy(x => Math.Abs((long)x - targetNumber));

        if (onsetActivationTimes[onsetActivationTimesIdx] <= cur_time)
        {
            ActivateShaders(onsetActivationTimesIdx);
            onsetActivationTimesIdx++;
        }
    }
}
