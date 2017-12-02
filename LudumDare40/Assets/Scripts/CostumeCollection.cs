using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CostumeCollection : MonoBehaviour {

    public List<Sprite> GuitaristCostumes = new List<Sprite>();

    public Sprite GetRandomCostume()
    {
        if (GuitaristCostumes.Count > 0)
        {
            var rand = Random.Range(0, GuitaristCostumes.Count);
            return GuitaristCostumes[rand];
        }
        return null;
    }
}
