using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuitarCollection : MonoBehaviour {

    public List<Sprite> GuitarSprites = new List<Sprite>();

    public Sprite GetRandomGuitar()
    {
        if (GuitarSprites.Count > 0)
        {
            var rand = Random.Range(0, GuitarSprites.Count);
            return GuitarSprites[rand];
        }
        return null;
    }
}
