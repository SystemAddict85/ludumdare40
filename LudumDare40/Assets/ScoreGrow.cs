using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreGrow : MonoBehaviour
{

    public int maxSize = 250;
    public int minSize = 175;
    private bool isGrowing = false;
    public int sizeChangeSpeed = 1;

    private float currentSize;

    public Text text;

    private bool readyToGrow = true;
    public float delaySpeed = .1f;

    
    void OnEnable()
    {
        StopAllCoroutines();
        readyToGrow = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (readyToGrow)
            ChangeFontSize();
    }

    private IEnumerator ReadyToChangeFontSize()
    {
        readyToGrow = false;
        yield return new WaitForSeconds(delaySpeed);
        readyToGrow = true;
    }

    private void ChangeFontSize()
    {

        StartCoroutine(ReadyToChangeFontSize());
        var step = sizeChangeSpeed;
        if (!isGrowing)
        {
            step *= -1;
        }

        text.fontSize += step;

        if (text.fontSize >= maxSize)
            isGrowing = false;
        else if (text.fontSize <= minSize)
            isGrowing = true;
    }

}
