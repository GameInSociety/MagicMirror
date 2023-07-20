using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    public float decalX = 0.5f;
    public float center_DecalX;
    public float decalY = 0.5f;
    public float center_DecalY = 0.5f;
    public float decalZ = 0.5f;
    public float scale = 1f;

    public float amount = 0.01f;

    public GameObject bg_obj;

    public Transform lerpTarget;

    public GameObject mask_obj;
    public GameObject head_obj;
    public GameObject bgMask_Obj;

    public bool mask_active = false;

    public float screenLerp_X = 0f;
    public float screenLerp_Y = 0f;

    public Transform[] lockTransforms;
    public Quaternion[] lockRotations;
    public float maxDecalX = 1f;
    public float maxDecalY = 1f;

    public bool debugPosition = false;

    private void Awake()
    {
        for (int i = 0; i < lockTransforms.Length; i++)
        {
            lockRotations[i] = lockTransforms[i].localRotation;
        }

    }

    private void LateUpdate()
    {
        screenLerp_X = Camera.main.WorldToViewportPoint(lerpTarget.position).x;
        screenLerp_X = screenLerp_X * 2f - 1;

        screenLerp_Y = Camera.main.WorldToViewportPoint(lerpTarget.position).y;
        screenLerp_Y = screenLerp_Y * 2f - 1;

        center_DecalX = screenLerp_X * maxDecalX;
        center_DecalY = screenLerp_Y * maxDecalY;

        if (debugPosition)
        {
            UpdateDebug();
        }

        transform.position += Vector3.right * center_DecalX;
        transform.position += Vector3.up * center_DecalY;

        MaskControl.instance.UpdateMask();
        Clone.Instance.UpdateBones();
    }


    public void UpdateDebug()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            bg_obj.SetActive(!bg_obj.activeSelf);
        }

        if (Input.GetKeyDown(KeyCode.UpArrow))
            scale += amount;
        if (Input.GetKeyDown(KeyCode.DownArrow))
            scale -= amount;

        if (Input.GetKeyDown(KeyCode.Z))
            decalY += amount;
        if (Input.GetKeyDown(KeyCode.S))
            decalY -= amount;

        if (Input.GetKeyDown(KeyCode.D))
            decalX += amount;
        if (Input.GetKeyDown(KeyCode.Q))
            decalX -= amount;

        if (Input.GetKeyDown(KeyCode.E))
            decalZ += amount;
        if (Input.GetKeyDown(KeyCode.A))
            decalZ -= amount;

        transform.position += Vector3.right * decalX;
        transform.position += Vector3.up * decalY;
        transform.position += Vector3.forward * decalZ;
        transform.localScale = Vector3.one * scale;
    }
}
