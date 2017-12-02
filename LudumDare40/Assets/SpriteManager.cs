using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class SpriteManager : MonoBehaviour
{

    public List<Guitarist> guitarists = new List<Guitarist>();

    CostumeCollection costumes;

    void Awake()
    {
        costumes = GetComponent<CostumeCollection>();

    }

    public Guitarist AddGuitarist()
    {
        var guitarist = new Guitarist();
        guitarist.GO = Instantiate(Resources.Load("Prefabs/Guitarist")) as GameObject;
        guitarist.CasualSprite = costumes.GetRandomCostume();
        guitarist.Stance = Guitarist.SpriteStance.CHAT;
        guitarists.Add(guitarist);
        return guitarist;
    }

    public void NameGuitarist(string name)
    {
        var guit = guitarists[guitarists.Count - 1];
        guit.Name = name;
        var textMesh = guit.GO.GetComponentInChildren<TextMesh>(true);
        textMesh.text = name;
        guit.ChangeNameTagDisplay(true);
        textMesh.GetComponent<Renderer>().sortingLayerName = "UI";
    }

    public Guitarist GetGuitarist(int ID)
    {
        return guitarists.Find(x => x.ID == ID);
    }
    public Guitarist GetGuitarist(string name)
    {
        return guitarists.Find(x => x.Name == name);
    }

}
public class Guitarist
{
    public string Name { get; set; }
    public Sprite CasualSprite { get; set; }
    public Sprite JamSprite { get; set; }
    public Sprite GoodRiffSprite { get; set; }
    public Sprite BadRiffSprite { get; set; }
    public AudioClip goodRiff { get; set; }
    public AudioClip badRiff { get; set; }
    public GameObject GO { get; set; }
    public int ID { get; private set; }

    public enum SpriteStance { CHAT, JAM, BAD, GOOD };

    private SpriteStance stance = SpriteStance.CHAT;
    public SpriteStance Stance
    {
        get
        {
            return stance;
        }
        set
        {
            GO.GetComponentInChildren<CostumeEditor>().ChangeCostume(ChangeSpriteStance());
            if (value == SpriteStance.BAD)
            {
                AudioManager.PlaySFX(badRiff, 1f);
            }
            else if (value == SpriteStance.GOOD)
            {
                AudioManager.PlaySFX(goodRiff, 1f);
            }
            stance = value;

        }
    }

    public Guitarist()
    {
        ID = GameManager.Instance.SM.guitarists.Count;
    }

    public void ChangeNameTagDisplay(bool isShowing)
    {
        GO.GetComponentInChildren<TextMesh>(true).gameObject.SetActive(isShowing);
    }

    public Sprite ChangeSpriteStance()
    {
        var sprite = CasualSprite;

        switch (stance)
        {
            case SpriteStance.BAD:
                sprite = BadRiffSprite;
                break;
            case SpriteStance.JAM:
                sprite = JamSprite;
                break;
            case SpriteStance.GOOD:
                sprite = GoodRiffSprite;
                break;
            case SpriteStance.CHAT:
            default:
                sprite = CasualSprite;
                break;
        }

        return sprite;
    }
}
