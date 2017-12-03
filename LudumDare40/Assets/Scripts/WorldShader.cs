using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WorldShader : MonoBehaviour {


    private Color originalColor;
    public Color failColor;
    public Color nailedItColor;

    [Range(0.1f,1f)]
    public float flashStep = .1f;

    public float flashInterval = .2f;
	
    void Awake()
    {
        originalColor = GetComponent<Image>().color;
    }
	
	// Update is called once per frame
	void Update () {
		
	}
    
    public IEnumerator FlashColor(Color color)
    {
        var image = GetComponent<Image>();
        image.color = color;
        
        while(image.color.a >= 0f)        {
            Debug.Log(image.color.a.ToString());
            var alpha = image.color.a - flashStep;
            image.color = new Color(color.r, color.g, color.b, alpha);
            yield return new WaitForSeconds(flashInterval);
        }
        image.color = originalColor;
        Debug.Log("flashed");
    }
}
