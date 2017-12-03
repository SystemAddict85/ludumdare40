using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OKButton : MonoBehaviour
{

    public float maxSize = 60f;
    public float minSize = 50f;
    private bool isGrowing = false;
    public float sizeChangeSpeed = 1f;

    private float currentSize;

    private RectTransform rect;

    void Awake()
    {
        rect = GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        var step = sizeChangeSpeed * Time.deltaTime;

        if (!isGrowing)
        {
            step *= -1;
        }

        rect.sizeDelta = new Vector2(rect.sizeDelta.x + step, rect.sizeDelta.y + step);

        if (rect.sizeDelta.x >= maxSize)
            isGrowing = false;
        else if(rect.sizeDelta.x <= minSize)
            isGrowing = true;
    }
}
