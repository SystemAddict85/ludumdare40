using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogBox : MonoBehaviour
{

    public Text TextBox { get; private set; }
    public Button ContinueButton { get; private set; }
    public GameObject Panel { get; private set; }

    [SerializeField]
    private int maxWordsToShow = 18;
    public Queue<string> DialogToShow = new Queue<string>();
    [SerializeField]
    private float typeDelay = .03f;

    void Awake()
    {
        TextBox = GetComponentInChildren<Text>(true);
        ContinueButton = GetComponentInChildren<Button>(true);
        Panel = GetComponentInChildren<Image>(true).gameObject;
        ContinueButton.onClick.AddListener(Continue);
        ContinueButton.interactable = false;        
    }

    // Update is called once per frame
    void Update()
    {
        if(DialogToShow.Count > 0)
        {
            Global.Paused = true;
        }
    }

    void Continue()
    {
        ContinueButton.interactable = false;
        if (DialogToShow.Count > 0)
        {
            StartCoroutine(PanelBlink());
        }
        else
        {
            StartCoroutine(WaitToContinue());
        }
    }

    IEnumerator PanelBlink()
    {
        yield return new WaitForSeconds(1f);
        Hide();
        StartCoroutine(TypeText(DialogToShow.Dequeue()));        
    }

    IEnumerator WaitToContinue()
    {
        Hide();
        yield return new WaitForSeconds(1f);
        Global.Paused = false;
    }

    public void CallDialogBox(string text)
    {
        Global.Paused = true;
        ParseStringIntoQueue(text);
        StartCoroutine(TypeText(DialogToShow.Dequeue()));
    }

    void Show()
    {
        Panel.SetActive(true);
    }

    void Hide()
    {
        Panel.SetActive(false);
    }

    void ParseStringIntoQueue(string text)
    {
        char[] delimiterChars = { ' ' }; //, ',', '.', ':', '\t' };
        string[] words = text.Split(delimiterChars);
        int i = 0;
        var queue = new Queue<string>(words);
        var textToQueue = "";
        while (queue.Count > 0)
        {
            if (i < maxWordsToShow)
            {
                textToQueue += queue.Dequeue() + ' ';
                ++i;
            }
            else
            {
                DialogToShow.Enqueue(textToQueue);
                textToQueue = "";
                i = 0;
            }
        }

        DialogToShow.Enqueue(textToQueue);
    }

    IEnumerator TypeText(string text)
    {
        TextBox.text = "";
        yield return new WaitForSeconds(.5f);
        Show();

        bool clicked = false;
        foreach(char c in text)
        {
            TextBox.text += c;
            if (char.IsLetterOrDigit(c))
                AudioManager.PlaySFX(AudioManager.GlobalSounds.typingBlip, .2f);
            if (Input.GetMouseButton(0))
            {
                TextBox.text = text;
                break;
            }
            yield return new WaitForSeconds(typeDelay);
        }
        if (clicked)
            yield return new WaitForSeconds(1f);
        ContinueButton.interactable = true;
        ContinueButton.Select();
    }
}
