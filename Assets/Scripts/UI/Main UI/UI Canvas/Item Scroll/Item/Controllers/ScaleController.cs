using UnityEngine;

/// <summary>
/// A behaviour to scale Item relative to Point Manager attributes.
/// </summary>

public class ScaleController : MonoBehaviour
{
    private RectTransform rect;

    private PointManager points;

    private Vector3 itemPosition;
    private Vector3 itemScale;

    private void Start()
    {
        rect = GetComponent<RectTransform>();
        points = GetComponent<PointController>().GetManager();

        itemPosition = rect.position;
        itemScale = rect.transform.localScale;
    }

    private void Update()
    {
        rect.transform.localScale = UpdateScale();
    }

    private float GetRelativeTo(float position, float start, float finish)
    {
        float distance = Mathf.Abs(finish - start);
        float abs_FinishPosition = Mathf.Abs(finish);
        float abs_Position = Mathf.Abs(position);
        float relativePosition = Mathf.Abs(abs_Position - abs_FinishPosition);

        return distance - relativePosition;
    }

    private Vector3 GetScaleVector(Vector3 position, IPoint start, IPoint finish)
    {
        float coefficient = Mathf.Abs(finish.GetScale().x - start.GetScale().x);
        float distanceBetween = Mathf.Abs(finish.GetPosition().x - start.GetPosition().x);
        float relativePosition = GetRelativeTo(position.x, start.GetPosition().x, finish.GetPosition().x);

        float scale = start.GetScale().x + ((relativePosition / distanceBetween) * coefficient);

        return new Vector3(scale, scale, scale);
    }

    private Vector3 UpdateScale()
    {
        itemPosition = rect.position;
        if(itemPosition.x < points.MostLeft.GetPosition().x)
        {
            //Far From MostLeft
            itemScale = points.MostLeft.GetScale();
        }
        if (itemPosition.x >= points.MostLeft.GetPosition().x && itemPosition.x < points.Left.GetPosition().x)
        {
            //Between MostLeft AND Left
            itemScale = GetScaleVector(itemPosition, points.MostLeft, points.Left);
        }
        else if (itemPosition.x >= points.Left.GetPosition().x &&
            itemPosition.x < points.MidLeft.GetPosition().x)
        {
            //Between Left AND MidLeft
            itemScale = GetScaleVector(itemPosition, points.Left, points.MidLeft);
        }
        else if (itemPosition.x >= points.MidLeft.GetPosition().x &&
            itemPosition.x < points.Center.GetPosition().x)
        {
            //Between MidLeft AND Center
            itemScale = GetScaleVector(itemPosition, points.MidLeft, points.Center);
        }
        else if (itemPosition.x == points.Center.GetPosition().x)
        {
            //At Center
            itemScale = points.Center.GetScale();
        }
        else if (itemPosition.x <= points.MidRight.GetPosition().x &&
            itemPosition.x > points.Center.GetPosition().x)
        {
            //Between MidRight AND Center
            itemScale = GetScaleVector(itemPosition, points.MidRight, points.Center);
        }
        else if (itemPosition.x > points.MidRight.GetPosition().x &&
            itemPosition.x <= points.Right.GetPosition().x)
        {
            //Between Right AND MidRight
            itemScale = GetScaleVector(itemPosition, points.Right, points.MidRight);
        }
        else if (itemPosition.x <= points.MostRight.GetPosition().x && 
            itemPosition.x > points.Right.GetPosition().x)
        {
            //Between MostRight AND Right
            itemScale = GetScaleVector(itemPosition, points.MostRight, points.Right);
        }
        else if(itemPosition.x > points.MostRight.GetPosition().x)
        {
            //Far From MostRight
            itemScale = points.MostRight.GetScale();
        }
        return itemScale;
    }
}
