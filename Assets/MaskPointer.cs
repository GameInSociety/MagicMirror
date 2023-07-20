using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaskPointer : MonoBehaviour
{
    Camera cam;

    public RectTransform rectTransform;

    public Transform target;

    private void Start()
    {
        cam = Camera.main;
        rectTransform = GetComponent<RectTransform>();
    }

    private void LateUpdate()
    {
        rectTransform.anchoredPosition = cam.WorldToScreenPoint(target.position);
    }
}
