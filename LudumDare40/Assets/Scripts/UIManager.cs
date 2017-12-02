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




    void Awake()
    {        
        dialogBox = GetComponentInChildren<DialogBox>(true);
        GetNonStandardUI();        
    }
    
    void GetNonStandardUI()
    {
        addNew = GetComponentInChildren<AddNewGuitarist>(true);
    }   

}
