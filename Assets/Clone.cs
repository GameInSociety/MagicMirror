using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clone : MonoBehaviour
{
    public static Clone Instance;

    public Transform[] targetBones;
    public Transform[] bones;

    public Transform targetParent;
    public Transform bonesParent;

    public Transform initScaleTransform;
    public Transform targetScaleTransform;

    public Transform pointerParent;
    public Transform targetPointerParent;

    public Vector3 decal;

    public float moveSpeed = 1f;
    public float rotSpeed = 1f;


    private void Awake()
    {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        targetBones = targetParent.GetComponentsInChildren<Transform>();
        bones = bonesParent.GetComponentsInChildren<Transform>();


        targetScaleTransform.localScale = initScaleTransform.localScale;

        pointerParent.parent = targetPointerParent;

        Debug.LogAssertion("init bones l : " + targetBones.Length);
        Debug.LogAssertion("current bones l : " + bones.Length);

        UpdateBones(1f, 1f);
    }

    public void UpdateBones()
    {
        UpdateBones(moveSpeed * Time.deltaTime, rotSpeed * Time.deltaTime);
    }

    // Update is called once per frame
    void UpdateBones(float moveLerp, float rotLerp)
    {
        for (int i = 0; i < targetBones.Length; i++)
        {
            Transform t = targetBones[i];
            Vector3 p = t.localPosition + decal;
            Quaternion r = t.localRotation;
            bones[i].localPosition = Vector3.Lerp(bones[i].localPosition, p, moveLerp);
            bones[i].localRotation = Quaternion.Lerp(bones[i].localRotation, r, rotLerp);

        }
    }
}
