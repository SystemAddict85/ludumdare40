using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    public static GameManager Instance = null;

    [HideInInspector]
    public List<string> GuitaristNames = new List<string>();
    [HideInInspector]
    public List<char> GuitaristLetters = new List<char>();

    public int numGuitars = 2;
    [Range(1f, 10f)]
    public float goodRiffThreshold = 1f;
    
    [SerializeField]
    private TextAsset nameFile;
    [HideInInspector]
    public List<KeyValuePair<char, string>> allNames = new List<KeyValuePair<char, string>>();
    
    [HideInInspector]
    public UIManager UI;
    [HideInInspector]
    public AudioManager AM;
    [HideInInspector]
    public SpriteManager SM;
    [HideInInspector]
    public JamManager JM;
    [HideInInspector]
    public CameraManager CM;

    #region Initialization
    void Awake () {
		
        if(Instance == null)
        {
            Instance = this;
        }
        else if(Instance != this)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);  

        GetManagers();
        if(allNames.Count == 0)
            ParseTextNames();

        StartCoroutine(StartGame());
        
	}

    void GetManagers()
    {
        UI = GetComponentInChildren<UIManager>();
        AM = GetComponentInChildren<AudioManager>();
        SM = GetComponentInChildren<SpriteManager>();
        JM = GetComponentInChildren<JamManager>(true);
        CM = GetComponentInChildren<CameraManager>();
    }

    void ParseTextNames()
    {
        var totalList = nameFile.text;
        var names = new List<string>(totalList.Split('\n'));
        foreach(var name in names)
        {
            if (name != "")
            {
                var n = name.ToLower();
                n = n[0].ToString().ToUpper() + n.Substring(1);

                allNames.Add(new KeyValuePair<char, string>(n[0], n));
            }
        }

    }

    IEnumerator StartGame()
    {
        yield return new WaitForSeconds(1f);
        StartCoroutine(CreateGuitarist());
    }
    #endregion    

    #region Creation
    public IEnumerator CreateGuitarist()
    {        
        var guit = SM.AddGuitarist();
        guit.GO.transform.position = UI.addNew.spawns.NameSpot.position;
        yield return new WaitForSeconds(.2f);
        Global.CallDialog("Enter a letter for the first name of your new guitarist. Then press enter.");
        StartCoroutine(WaitForOk());
    }

    IEnumerator WaitForOk()
    {
        yield return new WaitForSeconds(.4f);

        while (Global.Paused)
            yield return true;

        UI.addNew.gameObject.SetActive(true);
        UI.addNew.GetComponentInChildren<InputField>().interactable = true;
        UI.addNew.GetComponentInChildren<InputField>().ActivateInputField();
    }

    public void CreateNextGuitarist()
    {        
        StartCoroutine(CreateGuitarist());
    }
    #endregion

    public void BandReady()
    {
        UI.addNew.gameObject.SetActive(false);
        JM.activeGuits = SM.guitarists;
        JM.SetupRound();
        Debug.Log("Band Ready");
    }

    
}
