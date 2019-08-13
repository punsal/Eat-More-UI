using UnityEngine;

public class PointManager : MonoBehaviour
{
    #region Public Point Variables
    public IPoint MostLeft { get { return FindPoint(PointType.MostLeft); } }
    public IPoint Left { get { return FindPoint(PointType.Left); } }
    public IPoint MidLeft { get { return FindPoint(PointType.MidLeft); } }
    public IPoint Center { get { return FindPoint(PointType.Center); } }
    public IPoint MidRight { get { return FindPoint(PointType.MidRight); } }
    public IPoint Right { get { return FindPoint(PointType.Right); } }
    public IPoint MostRight { get { return FindPoint(PointType.MostRight); } }
    #endregion

    private IPoint[] points;

    private void FillPoints()
    {
        //initialize array
        points = new IPoint[transform.childCount];

        //fill the array
        for (int i = 0; i < points.Length; i++)
        {
            points[i] = transform.GetChild(i).GetComponent<Point>();
        }
    }
    private IPoint FindPoint(PointType type)
    {
        if (points == null)
        {
            FillPoints();
        }
        int index = 0;
        for (int i = 0; i < points.Length; i++)
        {
            if (points[i].GetPointType() == type)
            {
                index = i;
            }
        }

        return points[index];
    }
}
