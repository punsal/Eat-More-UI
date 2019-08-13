#pragma warning disable 649
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Point : MonoBehaviour, IPoint
{
    public Vector3 position;
    [SerializeField]
    private float scale;

    [SerializeField]
    private int slip;

    [SerializeField]
    private float opacity;

    [SerializeField]
    PointType type;

    private RectTransform rect;

    private void Awake()
    {
        rect = GetComponent<RectTransform>();
        position = GetPosition();
    }

    private void Update()
    {
        position = GetPosition();
    }

    public Vector3 GetPosition()
    {
        return rect.position;
    }

    public Vector3 GetScale()
    {
        return Vector3.one * scale;
    }

    public PointType GetPointType()
    {
        return type;
    }

    public float GetOpacity()
    {
        return opacity;
    }

    public Vector2 GetAnchorSlip()
    {
        return new Vector2(slip, 0);
    }
}
