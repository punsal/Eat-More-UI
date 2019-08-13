using UnityEngine;

public interface IPoint
{
    Vector3 GetPosition();
    Vector3 GetScale();
    float GetOpacity();
    Vector2 GetAnchorSlip();
    PointType GetPointType();
}