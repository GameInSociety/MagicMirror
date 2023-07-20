using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MaskControl : MonoBehaviour
{
    public static MaskControl instance;

    private Camera cam;
    public Transform target;
    private RectTransform rectTransform;
    public Transform child;
    public Transform initParent;

    Transform[] points;

    public MaskPointer prefab;

    public MaskPointer[] pointers;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
        rectTransform = GetComponent<RectTransform>();

        pointers = new MaskPointer[GetPoints().Length];
        for (int i = 0; i < 6; i++)
        {
            MaskPointer maskPointer = Instantiate(prefab, transform.parent);
            pointers[i] = maskPointer;
        }

    }

    private void LateUpdate()
    {
        UpdateMask();
    }

    public enum Dir
    {
        Left, Right, Top, Bottom, Front, Back
    }
    Transform GetPoint(Dir dir, out int index)
    {
        Transform p = GetPoints()[0];
        int i = 0;
        index = i;
        foreach (var item in GetPoints())
        {
            switch(dir)
            {
                case Dir.Left:
                    if ( item.position.x < p.position.x)
                    {
                        p = item;
                        index = i;
                    }
                    break;
                case Dir.Right:
                    if (item.position.x > p.position.x)
                    {
                        p = item;
                        index = i;
                    }
                    break;
                case Dir.Top:
                    if (item.position.y > p.position.y)
                    {
                        p = item;
                        index = i;
                    }
                    break;
                case Dir.Bottom:
                    if (item.position.y < p.position.y)
                    {
                        p = item;
                        index = i;
                    }
                    break;
                case Dir.Front:
                    if (item.position.z > p.position.z)
                    {
                        p = item;
                        index = i;
                    }
                    break;
                case Dir.Back:
                    if (item.position.z < p.position.z)
                    {
                        p = item;
                        index = i;
                    }
                    break;
                default:
                    index = -1;
                    break;
            }

            ++i;
        }
        return p;
    }

    public MaskPointer GetMaskPointer(Dir dir)
    {
        return pointers[(int)dir];
    }

    // Update is called once per frame
    public void UpdateMask()
    {
        child.SetParent(initParent);

        MaskPointer[] _pointers = new MaskPointer[6];

        for (int i = 0; i < 6; i++)
        {
            int index;
            pointers[i].target = GetPoint((Dir)i, out index);
        }

        float x = GetMaskPointer(Dir.Left).rectTransform.anchoredPosition.x;
        float y = GetMaskPointer(Dir.Bottom).rectTransform.anchoredPosition.y;

        float w = GetMaskPointer(Dir.Right).rectTransform.anchoredPosition.x - GetMaskPointer(Dir.Left).rectTransform.anchoredPosition.x;
        float h = GetMaskPointer(Dir.Top).rectTransform.anchoredPosition.y - GetMaskPointer(Dir.Bottom).rectTransform.anchoredPosition.y;

        if (GetMaskPointer(Dir.Front).rectTransform.anchoredPosition.x < GetMaskPointer(Dir.Left).rectTransform.anchoredPosition.x)
        {
            x = GetMaskPointer(Dir.Front).rectTransform.anchoredPosition.x;
            w = GetMaskPointer(Dir.Back).rectTransform.anchoredPosition.x - GetMaskPointer(Dir.Front).rectTransform.anchoredPosition.x;
        }
        else if (GetMaskPointer(Dir.Front).rectTransform.anchoredPosition.x > GetMaskPointer(Dir.Right).rectTransform.anchoredPosition.x)
        {
            w = GetMaskPointer(Dir.Front).rectTransform.anchoredPosition.x - GetMaskPointer(Dir.Left).rectTransform.anchoredPosition.x;
        }

        Vector2 p = new Vector2(x, y);
        Vector2 scale = new Vector2(w, h);

        rectTransform.anchoredPosition = p;


        rectTransform.sizeDelta = Vector2.Lerp(rectTransform.sizeDelta, scale, 10f * Time.deltaTime);

        /*if (NuitrackControl.Instance.inside)
        {
            rectTransform.sizeDelta = Vector2.Lerp(rectTransform.sizeDelta, scale, 10f * Time.deltaTime);
        }
        else
        {
            rectTransform.sizeDelta = Vector2.Lerp(rectTransform.sizeDelta, Vector2.zero, 10f * Time.deltaTime);
        }*/

        child.SetParent(transform);

    }

    Transform[] GetPoints()
    {
        return target.GetComponentsInChildren<Transform>();
    }

    private void OnDrawGizmos()
    {
        foreach (Transform t in GetPoints())
        {
            Gizmos.color = Color.magenta;
            Gizmos.DrawWireSphere(t.position, 0.01f);
        }

        for (int i = 0; i < 6; i++)
        {
            Gizmos.color = Color.blue;
            int index = 0;
            Gizmos.DrawSphere(GetPoint((Dir)i, out index).position, 0.02f);
        }
    }
}
