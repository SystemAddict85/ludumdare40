using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CameraManager : MonoBehaviour {

    Camera cam;
    private Transform cameraTarget;
    private bool isCameraMoving = false;

    [Range(0.025f, 0.1f)]
    public float zoomInStep = 0.05f;
    [Range(0.025f, 0.1f)]
    public float zoomOutStep = 0.1f;

    void Awake () {
        cam = Camera.main;
    }

    void Update()
    {
        if (isCameraMoving && !Global.Paused)
        {
            var target = new Vector3(cameraTarget.position.x, cameraTarget.position.y, -10f);
            cam.transform.position = Vector3.MoveTowards(cam.transform.position, target, .2f);
        }
    }

    public void ChangeCameraMoving(bool isMoving)
    {
        isCameraMoving = isMoving;
    }

    public void ChangeCameraTarget(Transform target)
    {
        cameraTarget = target;
    }

    public void ZoomCameraTo(Transform target)
    {
        if (target == null)
            target = GameManager.Instance.JM.ChooseRandomGuitarist().GO.transform;

        StartCoroutine(ZoomIn());
        MoveCameraTo(target);
    }

    void MoveCameraTo(Transform target)
    {
        ChangeCameraTarget(target);
        ChangeCameraMoving(true);
    }
    
    public IEnumerator ZoomIn()
    {
        while (cam.orthographicSize > 1)
        {
            yield return new WaitForSeconds(0.01f);
            cam.orthographicSize -= zoomInStep;
        }
    }

    public IEnumerator ZoomOut()
    {
        while (cam.orthographicSize < 2)
        {
            yield return new WaitForSeconds(0.01f);
            cam.orthographicSize += zoomOutStep;
        }
        cam.orthographicSize = 2f;
    }
}
