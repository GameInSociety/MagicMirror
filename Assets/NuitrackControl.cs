using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NuitrackSDK;
using NuitrackSDK.Avatar;
using JetBrains.Annotations;

public class NuitrackControl : MonoBehaviour
{
    public static NuitrackControl Instance;

    public Vector3 triggerDistance = Vector3.zero;

    public Material[] mats;

    public Transform center;
    public Transform target;

    public float duration = 1f;

    public bool inside = false;

    float timer = 0f;

    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        return;
        inside = Vector3.Distance(center.position, target.position) < triggerDistance.z;

        timer += Time.deltaTime;
        float lerp = timer / duration;

        if ( !inside)
        {
            if (Vector3.Distance(center.position, target.position) < triggerDistance.z)
            {
                inside = true;
                timer = 0f;
            }

            
        }
        else
        {
            if (Vector3.Distance(center.position, target.position) >= triggerDistance.z)
            {
                inside = false;
                timer = 0f;
            }
        }

        /*if ( timer <= duration+0.5f)
        {
            foreach (var mat in mats)
            {
                float f = mat.GetFloat("_DissolveAmount");
                mat.SetFloat("_DissolveAmount", Mathf.Lerp(f, inside ? 1f : 0f, lerp));
            }
        }*/
    }

    private void OnDrawGizmos()
    {
        //Gizmos.DrawCube(center.position, triggerDistance);
    }
}
