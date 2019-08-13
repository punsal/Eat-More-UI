using UnityEngine;

public enum BackgroundType { Static, Animated }

public class AnimationController : MonoBehaviour
{
    public BackgroundType backgroundType;
    public Color backgroundColor;
    [Range(0.001f, 5f)]
    public float threshold;
    private BackgroundVisualController controller;

    private RectTransform rect;
    private Vector3 itemPosition;

    private PointManager points;

    private void Start()
    {
        rect = GetComponent<RectTransform>();
        itemPosition = rect.position;

        points = GetComponent<PointController>().GetManager();

        controller = GetComponentInChildren<BackgroundVisualController>();
    }

    private void Update()
    {
        itemPosition = rect.position;

        if (itemPosition.x > points.Center.GetPosition().x - threshold &&
            itemPosition.x < points.Center.GetPosition().x + threshold)
        {
            if (backgroundType == BackgroundType.Animated)
            {
                controller.SetColor(backgroundColor);
            }
            PlayAnim();
        }
        else
        {
            if (backgroundType == BackgroundType.Animated)
            {
                controller.SetColor(Color.white);
            }
            StopAnim();
        }
    }

    #region Item Animator Controllers
    private void PlayAnim()
    {
        GetComponent<Animator>().SetBool("IsSelected", true);
    }

    private void StopAnim()
    {
        GetComponent<Animator>().SetBool("IsSelected", false);
    }
    #endregion
}
