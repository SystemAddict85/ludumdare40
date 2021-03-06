﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class SpriteManager : MonoBehaviour
{

    public List<Guitarist> guitarists = new List<Guitarist>();

    CostumeCollection costumes;
    public Animator[] animators = new Animator[5];

    private bool isSorting = false;

    void Awake()
    {
        costumes = GetComponent<CostumeCollection>();

    }

    void Update()
    {
        if (Global.isGameStarted && !isSorting && !Global.Paused)
        {
            isSorting = true;
            StartCoroutine(WaitToSortLayers());
        }
    }
    
    IEnumerator WaitToSortLayers()
    {
        yield return new WaitForSeconds(.5f);
        var guits = GameManager.Instance.JM.activeGuits;

        var sortedList = guits.OrderBy(guit => guit.GO.transform.position.y).ToArray();
        for (int i = 0; i < sortedList.Count(); ++i)
        {
            var rends = sortedList[i].GO.GetComponentsInChildren<SpriteRenderer>();
            foreach(var r in rends)
            {
                r.sortingLayerName = "Guitarist" + (i + 1);
            }
        }

        isSorting = false;

    }

    public Guitarist AddGuitarist()
    {
        var guitarist = new Guitarist();
        guitarist.GO = Instantiate(Resources.Load("Prefabs/Guitarist")) as GameObject;
        guitarist.sprite = costumes.GetRandomCostume();
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
    public Sprite sprite { get; set; }
    public AudioClip jamRiff { get; set; }
    public AudioClip goodRiff { get; set; }
    public AudioClip badRiff { get; set; }
    public float jamVolume { get; set; }
    public GameObject GO { get; set; }
    public Sprite Guitar { get; set; }
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
            GO.GetComponentInChildren<CostumeEditor>().ChangeCostume(sprite);
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
        var guitar = GameManager.Instance.SM.GetComponent<GuitarCollection>();
        Guitar = guitar.GetRandomGuitar();
        var riffs = GameManager.Instance.AM.GetComponent<RiffCollection>().RiffClips;
        var levels = GameManager.Instance.AM.GetComponent<RiffCollection>().AudioLevels;
        var flourishes = GameManager.Instance.AM.GetComponent<RiffCollection>().FlourishClips;
        jamRiff = riffs[guitar.GuitarSprites.IndexOf(Guitar)];
        goodRiff = flourishes[guitar.GuitarSprites.IndexOf(Guitar)];
        jamVolume = levels[guitar.GuitarSprites.IndexOf(Guitar)];
    }

    public void ChangeNameTagDisplay(bool isShowing)
    {
        GO.GetComponentInChildren<TextMesh>(true).gameObject.SetActive(isShowing);
    }

    public Sprite ChangeSpriteStance()
    {
        return sprite;
    }

    public void ChangeGameObject()
    {
        GO.GetComponentInChildren<Animator>().runtimeAnimatorController = GameManager.Instance.SM.animators[GameManager.Instance.SM.GetComponent<CostumeCollection>().GuitaristCostumes.IndexOf(sprite)].runtimeAnimatorController;
        GO.GetComponentInChildren<GuitarEditor>().GetComponent<SpriteRenderer>().sprite = Guitar;
        GO.GetComponentInChildren<TextMesh>().text = Name;
        GO.GetComponentInChildren<TextMesh>().GetComponent<Renderer>().sortingLayerName = "UI";
    }
}
