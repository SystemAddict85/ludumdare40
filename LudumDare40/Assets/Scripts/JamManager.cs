using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class JamManager : MonoBehaviour {

    public Transform rehearsalStartingSpots;
    private Transform[] startLocations = new Transform[8];
    public List<Guitarist> activeGuits = new List<Guitarist>();

    public float riffClip = 1f;
    
	// Use this for initialization
	void Awake () {
        var trans = rehearsalStartingSpots.GetComponentsInChildren<Transform>();
        for (int i = 1; i < trans.Length; ++i)
            startLocations[i - 1] = trans[i];        
	}

    public void SetupRound()
    {
        StartCoroutine(PlaceGuitarists());
    }


    IEnumerator PlaceGuitarists()
    {
        int i = 0;
        foreach (var guit in activeGuits)
        {
            Destroy(guit.GO);
            guit.GO = Instantiate(Resources.Load("Prefabs/GuitaristPlaying")) as GameObject;
            guit.ChangeGameObject();
            guit.GO.transform.parent = startLocations[i];
            guit.GO.transform.position = startLocations[i].position;
            guit.GO.SetActive(true);
            ++i;
            yield return new WaitForSeconds(.02f);
        }
        
        StartGame();
    }		
	

    void StartGame()
    {
        Global.isGameStarted = true;
    }
  

    public Guitarist ChooseRandomGuitarist()
    {
        var rand = Random.Range(0, activeGuits.Count);
        return activeGuits[rand];
    }

    public void GuitarSolo()
    {
        var guit = ChooseRandomGuitarist();
        var riff = guit.jamRiff;
        AudioManager.PlaySFX(riff, guit.jamVolume);
        GameManager.Instance.CM.ZoomCameraTo(guit.GO.transform);
        GameManager.Instance.UI.soloMeter.transform.parent.gameObject.SetActive(true);
        var riffLength = riff.length - riffClip;
        GameManager.Instance.UI.soloMeter.StartSoloMeter(guit, riffLength);
        StartCoroutine(ZoomSoloTime(riffLength));
    }

    IEnumerator ZoomSoloTime(float timeToZoomOut)
    {
        Global.ShowMessageBox("Press the key corresponding to the first letter of their name when the meter goes green!");
        var cam = GameManager.Instance.CM;
        yield return new WaitForSeconds(timeToZoomOut);
        StartCoroutine(cam.ZoomOut());
        cam.ChangeCameraTarget(EventSystem.current.transform);
        yield return new WaitForSeconds(1f);
        cam.ChangeCameraMoving(false);
        Global.HideMessageBox();
        GameManager.Instance.UI.soloMeter.transform.parent.gameObject.SetActive(false);
    }

    public void GoodScore()
    {
        StartCoroutine(Global.ChangeScore(1000));
    }

    public void BadScore()
    {
        StartCoroutine(Global.ChangeScore(-1000));
    }
}
