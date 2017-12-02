using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class AddNewGuitarist : MonoBehaviour {

    public Button SubmitButton { get; private set; }
    public InputField inputField { get; private set; }

    void Awake()
    {
        var addNew = GameObject.Find("AddNew");


        SubmitButton = addNew.GetComponentInChildren<Button>();
        inputField = addNew.GetComponentInChildren<InputField>();

        SubmitButton.onClick.AddListener(OnInputSubmit);

    }

    // Update is called once per frame
    void Update () {
		
	}

    void OnInputSubmit()
    {        
        if (!Global.Paused && inputField.text != "")
        {
            if (char.IsLetter(inputField.text[0]))
            {
                inputField.text = inputField.text.ToUpper();
                var letter = inputField.text[0];
                if (CheckIfLetterExists(letter))
                {
                    Global.CallDialog("This letter is already taken");
                }
                else
                {
                    var name = GetGuitaristsName(letter);
                    GameManager.Instance.GuitaristNames.Add(name);
                    GameManager.Instance.GuitaristLetters.Add(name[0]);
                    Global.CallDialog("Added " + name);
                }
            }
        }
    }

    private string GetGuitaristsName(char c)
    {
        // Create Name Generator
        var list = GameManager.Instance.allNames;
        var l = list.Where(kvp => kvp.Key == c);
        var rand = Random.Range(0, l.Count()-1);
        return l.ElementAt(rand).Value;
    }

    bool CheckIfLetterExists(char c)
    {
        return GameManager.Instance.GuitaristLetters.Contains(c);
    }
}
