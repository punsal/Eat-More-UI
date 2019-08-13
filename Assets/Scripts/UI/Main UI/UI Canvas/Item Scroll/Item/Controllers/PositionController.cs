using UnityEngine;

/// <summary>
/// A behaviour to position Item relative to PointManager attributes. 
/// </summary>

public class PositionController : MonoBehaviour
{
    private RectTransform rect;

    private PointManager points;

    private Vector3 itemPosition;
    private Vector2 originalAnchor;

    private void Start()
    {
        rect = GetComponent<RectTransform>();
        points = GetComponent<PointController>().GetManager();

        itemPosition = rect.position;
        originalAnchor = rect.anchoredPosition;
    }

    private void Update()
    {
        rect.anchoredPosition = UpdateSlip();
    }



    private float GetRelativeTo(float position, float start, float finish)
    {
        float distance = Mathf.Abs(finish - start);
        float abs_FinishPosition = Mathf.Abs(finish);
        float abs_Position = Mathf.Abs(position);
        float relativePosition = Mathf.Abs(abs_Position - abs_FinishPosition);

        return distance - relativePosition;
    }

    private Vector2 GetAnchorPosition(Vector3 position, IPoint start, IPoint finish)
    {
        float coefficient = finish.GetAnchorSlip().x - start.GetAnchorSlip().x;
        float distanceBetween = Mathf.Abs(finish.GetPosition().x - start.GetPosition().x);
        float relativePosition = GetRelativeTo(position.x, start.GetPosition().x, finish.GetPosition().x);
        float percentage = relativePosition / distanceBetween;
        
        //FORMULA
        // return : original + <Start.Slip> + (relativePos / distance) * <Finish.Slip - Start.Slip>;
        float slip = originalAnchor.x + start.GetAnchorSlip().x + (percentage * coefficient);

        return new Vector2(slip, originalAnchor.y);
    }

    private Vector2 UpdateSlip()
    {
        itemPosition = rect.position;
        Vector2 itemAnchor = Vector3.zero;

        if(itemPosition.x < points.MostLeft.GetPosition().x)
        {
            //Far From MostLeft
            itemAnchor = originalAnchor + points.MostLeft.GetAnchorSlip();
        }
        else if(itemPosition.x >= points.MostLeft.GetPosition().x && 
            itemPosition.x < points.Left.GetPosition().x)
        {
            //Between MostLeft AND Left
            itemAnchor = GetAnchorPosition(itemPosition, points.Left, points.MostLeft);
        }
        else if(itemPosition.x >= points.Left.GetPosition().x &&
            itemPosition.x < points.MidLeft.GetPosition().x)
        {
            //Between Left AND MidLeft
            itemAnchor = GetAnchorPosition(itemPosition, points.MidLeft, points.Left);
        }
        else if(itemPosition.x >= points.MidLeft.GetPosition().x &&
            itemPosition.x <= points.MidRight.GetPosition().x)
        {
            //Between MidLeft AND MidRight
            itemAnchor = GetAnchorPosition(itemPosition, points.MidLeft, points.MidRight);
        }
        else if(itemPosition.x <= points.Right.GetPosition().x &&
            itemPosition.x > points.MidRight.GetPosition().x)
        {
            //Between Right AND MidRight
            itemAnchor = GetAnchorPosition(itemPosition, points.MidRight, points.Right);
        }
        else if(itemPosition.x > points.Right.GetPosition().x &&
            itemPosition.x <= points.MostRight.GetPosition().x)
        {
            //Between MostRight AND Right
            itemAnchor = GetAnchorPosition(itemPosition, points.Right, points.MostRight);
        }
        else if (itemPosition.x > points.MostRight.GetPosition().x)
        {
            //Far From MostRight
            itemAnchor = originalAnchor + points.MostRight.GetAnchorSlip();
        }
        
        return itemAnchor;
    }

}
