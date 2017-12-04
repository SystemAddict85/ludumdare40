using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LifeBar : MonoBehaviour {

    public Image[] lifeImages = new Image[3];

    void Awake()
    {
        lifeImages = GetComponentsInChildren<Image>();
    }
	
    public void LowerLife()
    {
        lifeImages[Global.remainingLives].enabled = false;
    }
}
