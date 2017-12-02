using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CostumeEditor : MonoBehaviour {
    
    public void ChangeCostume(Sprite sprite)
    {
        GetComponent<SpriteRenderer>().sprite = sprite;
    }
}
