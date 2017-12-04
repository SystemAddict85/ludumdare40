using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoloMeter : MonoBehaviour
{


    private float currentTime = 0f;
    public float playTime = 0f;
    private float goodTime;

    public bool isSoloTime = false;
    public Image guitarImage;

    public Guitarist soloGuitarist;
    public string soloKey;
    private bool hasPressed = false;
    private bool isGood = false;

    void Update()
    {
        if (!Global.Paused)
        {

            if (isSoloTime && !hasPressed)
            {
                UpdateMeter();
                GetPlayerInput();
            }
        }
    }

    private void GetPlayerInput()
    {
        if (Input.inputString.Length != 0)
        {
            hasPressed = true;
            if (Input.GetKey(soloKey) && IsGoodTime())
            {
                GoodPress();
            }
            else
            {
                BadPress();
            }
        }
    }

    private void BadPress()
    {
        Debug.Log("fail");
        var worldShader = GameManager.Instance.UI.worldShader;
        StartCoroutine(worldShader.FlashColor(worldShader.failColor));
        GameManager.Instance.JM.BadScore();
        AudioManager.PlaySFX(GameManager.Instance.BadNote, .5f);
    }
    private void GoodPress()
    {
        Debug.Log("good press");
        isGood = true;
        var worldShader = GameManager.Instance.UI.worldShader;
        StartCoroutine(worldShader.FlashColor(worldShader.nailedItColor));
        GameManager.Instance.JM.GoodScore();
        AudioManager.PlaySFX(soloGuitarist.goodRiff, 1f);
    }
    private bool IsGoodTime()
    {        
        if (currentTime / playTime >= goodTime)
        {
            return true;
        }

        return false;
    }

  
    private void UpdateMeter()
    {
        currentTime += Time.deltaTime;
        var image = GetComponent<Image>();
        image.fillAmount = currentTime / playTime;
        if (IsGoodTime())
        {
            image.color = GameManager.Instance.UI.worldShader.nailedItColor;
        }
        else
        {
            image.color = GameManager.Instance.UI.worldShader.failColor;
        }
        if (currentTime >= playTime)
        {
            currentTime = 0f;
            if (!hasPressed)
            {
                image.color = GameManager.Instance.UI.worldShader.failColor;
                isGood = false;
                BadPress();
            }
            isSoloTime = false;

        }
    }



    public void StartSoloMeter(Guitarist guit, float time)
    {
        soloGuitarist = guit;
        guitarImage.sprite = guit.Guitar;
        currentTime = 0f;
        playTime = time;
        soloKey = guit.Name[0].ToString().ToLower();
        goodTime = (100f - GameManager.Instance.goodRiffThreshold) / 100f;
        hasPressed = false;
        isSoloTime = true;

    }
}
