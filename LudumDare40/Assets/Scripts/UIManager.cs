using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

   
    [HideInInspector]
    public DialogBox dialogBox;


    // Non-standard UI elements
    [HideInInspector]
    public AddNewGuitarist addNew;

    [HideInInspector]
    public SoloMeter soloMeter;

    [HideInInspector]
    public WorldShader worldShader;
    
    public Image MessageBox;
    public Text MessageBoxText;

    [HideInInspector]
    public ScoreBoard score;
    [HideInInspector]
    public ScoreGrow addScore;


    void Awake()
    {        
        dialogBox = GetComponentInChildren<DialogBox>(true);
        MessageBoxText = MessageBox.GetComponentInChildren<Text>();
        GetNonStandardUI();        
    }
    
    void GetNonStandardUI()
    {
        addNew = GetComponentInChildren<AddNewGuitarist>(true);
        soloMeter = GetComponentInChildren<SoloMeter>(true);
        worldShader = GetComponentInChildren<WorldShader>();
        score = GetComponentInChildren<ScoreBoard>();
        addScore = GetComponentInChildren<ScoreGrow>(true);
    }   

}
