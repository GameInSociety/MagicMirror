using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FollowTransform : MonoBehaviour
{
    RectTransform rectTransform;
    public Transform target;
    Camera mCamera;
    public Transform child;
    public Transform initParent;

    // Start is called before the first frame update
    void Start()
    {
        mCamera = Camera.main;
        rectTransform = GetComponent<RectTransform>();   
    }

    // Update is called once per frame
    void LateUpdate()
    {
        child.SetParent(initParent);
        rectTransform.anchoredPosition = mCamera.WorldToScreenPoint(target.position);
        child.SetParent(rectTransform);

    }
}
