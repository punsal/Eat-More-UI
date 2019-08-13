using System;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// A behaviour to control its Image color and alpha attributes relative to given PointManager.
/// </summary>

public class OpacityController : MonoBehaviour
{
    private RectTransform rect;
    private Image img;

    private PointManager points;

    private Vector3 itemPosition;
    private Color color;

    private void Start()
    {
        rect = GetComponent<RectTransform>();
        img = GetComponent<Image>();
        points = GetComponentInParent<PointController>().GetManager();

        itemPosition = rect.position;
        color = img.color;
    }

    private void Update()
    {
        color = img.color;
        img.color = UpdateOpacity();
    }

    private float GetRelativeTo(float position, float start, float finish)
    {
        float distance = Mathf.Abs(finish - start);
        float abs_FinishPosition = Mathf.Abs(finish);
        float abs_Position = Mathf.Abs(position);
        float relativePosition = Mathf.Abs(abs_Position - abs_FinishPosition);

        return distance - relativePosition;
    }

    private Color GetColor(Vector3 position, Color color, IPoint start, IPoint finish)
    {
        float coefficient = Mathf.Abs(finish.GetOpacity() - start.GetOpacity());
        float distance = Mathf.Abs(finish.GetPosition().x - start.GetPosition().x);
        float relative = GetRelativeTo(position.x, start.GetPosition().x, finish.GetPosition().x);

        float alpha = start.GetOpacity() + ((relative / distance) * coefficient);

        return new Color(color.r, color.g, color.b, alpha);
    }

    private Color UpdateOpacity()
    {
        itemPosition = rect.position;
        if(itemPosition.x < points.MostLeft.GetPosition().x)
        {
            //Far From MostLeft
            color = GetColor(itemPosition, color, points.MostLeft, points.MostLeft);
        }
        else if(itemPosition.x >= points.MostLeft.GetPosition().x && itemPosition.x < points.Left.GetPosition().x)
        {
            //Between MostLeft AND Left
            color = GetColor(itemPosition, color, points.MostLeft, points.Left);
        } 
        else if(itemPosition.x >= points.Left.GetPosition().x && itemPosition.x < points.MidLeft.GetPosition().x)
        {
            //Between Left AND MidLeft
            color = GetColor(itemPosition, color, points.Left, points.MidLeft);
        }
        else if(itemPosition.x >= points.MidLeft.GetPosition().x && itemPosition.x < points.Center.GetPosition().x)
        {
            //Between MidLeft AND Center
            color = GetColor(itemPosition, color, points.MidLeft, points.Center);
        }
        else if (itemPosition.x <= points.MidRight.GetPosition().x && itemPosition.x > points.Center.GetPosition().x)
        {
            //Between MidRight AND Center
            color = GetColor(itemPosition, color, points.MidRight, points.Center);
        }
        else if (itemPosition.x <= points.Right.GetPosition().x && itemPosition.x > points.MidRight.GetPosition().x)
        {
            //Between Right AND MidRight
            color = GetColor(itemPosition, color, points.Right, points.MidRight);
        }
        else if(itemPosition.x <= points.MostRight.GetPosition().x && itemPosition.x > points.Right.GetPosition().x)
        {
            //Between MostRight AND Right
            color = GetColor(itemPosition, color, points.MostRight, points.Right);
        }
        else if(itemPosition.x > points.MostRight.GetPosition().x)
        {
            //Far From MostRight
            color = GetColor(itemPosition, color, points.MostRight, points.MostRight);
        }
        return color;
    }
}
