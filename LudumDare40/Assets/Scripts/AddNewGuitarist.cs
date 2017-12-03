using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class AddNewGuitarist : MonoBehaviour
{

    public InputField inputField { get; private set; }

    public SpawnSpots spawns;


    void Awake()
    {
        inputField = GetComponentInChildren<InputField>();

    }

    public void OnInputSubmit()
    {
        if (!Global.Paused && inputField.text != "" && Input.GetKey(KeyCode.Return))
        {
            if (char.IsLetter(inputField.text[0]))
            {
                inputField.text = inputField.text.ToUpper();
                inputField.interactable = false;                
                var letter = inputField.text[0];
                AddGuitarist(letter);
            }
        }
    }

    private string GetGuitaristsName(char c)
    {
        // Create Name Generator
        var list = GameManager.Instance.allNames;
        var l = list.Where(kvp => kvp.Key == c);
        var rand = Random.Range(0, l.Count());
        return l.ElementAt(rand).Value;
    }

    bool CheckIfLetterExists(char c)
    {
        return GameManager.Instance.GuitaristLetters.Contains(c);
    }

    public void AddGuitarist(char letter)
    {
        if (CheckIfLetterExists(letter))
        {
            Global.CallDialog("This letter is already taken!");
            StartCoroutine(WaitForOKAfterWrong());
        }
        else
        {
            var name = GetGuitaristsName(letter);
            GameManager.Instance.GuitaristNames.Add(name);
            GameManager.Instance.SM.NameGuitarist(name);
            GameManager.Instance.GuitaristLetters.Add(name[0]);
            Global.CallDialog("Added " + name);
            StartCoroutine(WaitForOK());
        }
        inputField.text = "";

    }

    IEnumerator WaitForOKAfterWrong()
    {
        while (Global.Paused)
            yield return true;

        inputField.interactable = true;
        inputField.ActivateInputField();        
    }

    IEnumerator WaitForOK()
    {

        while (Global.Paused)
        {
            yield return true;

        }

        var guits = GameManager.Instance.SM.guitarists;
        var spawn = spawns.TalkingSpots[guits.Count - 1];
        var trans = guits[guits.Count - 1].GO.transform;
        trans.parent = spawn;
        trans.position = spawn.position;
        trans.GetComponentInChildren<CostumeEditor>().transform.localScale = new Vector3(trans.localScale.x == -1 ? -1 : 1, 1, 1);

        if (guits.Count != GameManager.Instance.numGuitars)
            GameManager.Instance.CreateNextGuitarist();
        else
            GameManager.Instance.BandReady();
    }


}
