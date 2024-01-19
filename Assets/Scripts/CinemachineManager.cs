using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CinemachineManager : MonoBehaviour
{
    public CinemachineVirtualCamera vcam;
    public float zoom_factor = 1f;
    private float zoom;
    private float totalZoom = 0;
    private float maxZoom;

    // We get the current size to use it when clamping the value
    void Start() {
        totalZoom = vcam.m_Lens.OrthographicSize;
        maxZoom = totalZoom;

    }

    // We change the current orthographic size using the mouse wheel delta
    void Zoom() {
        float zoom = -(Input.mouseScrollDelta.y) * zoom_factor * Time.deltaTime;
        totalZoom += zoom;
        totalZoom = Mathf.Clamp(totalZoom, 0f, maxZoom);
        vcam.m_Lens.OrthographicSize = totalZoom;

    }

    // Update is called once per frame
    void Update()
    {
        Zoom();
    }
}
