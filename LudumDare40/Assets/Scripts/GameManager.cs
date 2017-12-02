using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public static GameManager Instance = null;
    
    public List<string> GuitaristNames = new List<string>();
    public List<char> GuitaristLetters = new List<char>();

    public int numGuitars = 2;
    private int currentGuitarist = 1;

    private bool isChoosingName = false;

    [SerializeField]
    private TextAsset nameFile;
    [HideInInspector]
    public List<KeyValuePair<char,string>> allNames = new List<KeyValuePair<char,string>>();

    [HideInInspector]
    public UIManager UI;
    [HideInInspector]
    public AudioManager AM;
        
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
        InitializeGuitars();
        ParseTextNames();
        
	}

    void GetManagers()
    {
        UI = GetComponentInChildren<UIManager>();
        AM = GetComponentInChildren<AudioManager>();
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

    void Update()
    {
        if(isChoosingName){

        }
    }

    void InitializeGuitars()
    {

    }    
}
