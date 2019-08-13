#pragma warning disable 649

using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class SnapManager : MonoBehaviour
{
    [Header("Scroll Rect")]
    public ScrollRect levelsScroll;
    [Range(0.1f, 1f)]
    public float deceleration = 0.135f;

    [Header("Button Animation")]
    [SerializeField] private float yieldTime;
    [SerializeField] private float translationFactor;
    [SerializeField] private int elasticity;

    #region Editor Fields
    [SerializeField]
    [Range(1f, 20f)]
    private float snapSpeed;
    #endregion

    #region Component Fields
    private RectTransform container;
    private PointManager points;
    #endregion

    #region Behavioural Fields
    public int SnapIndex { get { return GetNearestIndexToCenter(); } }

    private Item[] items;
    private float firstItemPosition;
    private float lastItemPosition;

    private float[] distances;
    private float[] positions;

    private bool isDragging = false;
    private bool isCoroutine = false;
    #endregion

    private void Start()
    {
        //Get Component Fields From LevelScrollManager
        ItemScrollManager scrollManager = GetComponent<ItemScrollManager>();
        container = scrollManager.GetItemContainer();
        points = scrollManager.GetPointManager();

        //Get items in container
        items = scrollManager.GetItems();
        firstItemPosition = -1f * items[0].Rect.position.x;
        lastItemPosition = -1f * items[items.Length - 1].Rect.anchoredPosition.x;

        //initialize distances & positions
        distances = new float[items.Length];
        positions = new float[items.Length];

        //Open ScrollRect.inertia
        levelsScroll.inertia = true;
        levelsScroll.decelerationRate = deceleration;
    }

    private void Update()
    {
        UpdatePositions();
        UpdateDistances();

        if (!isDragging && !isAtPosition && !isCoroutine)
        {
            if (container.anchoredPosition.x > firstItemPosition)
            {
                levelsScroll.inertia = false;
                Snap(CalculateItemPositionAt(GetNearestIndexToCenter()));
            } else if (container.anchoredPosition.x < lastItemPosition)
            {
                levelsScroll.inertia = false;
                Snap(CalculateItemPositionAt(GetNearestIndexToCenter()));
            } else if (container.anchoredPosition.x > lastItemPos - 1f &&
                  container.anchoredPosition.x < lastItemPos + 1f)
            {
                container.anchoredPosition = new Vector2((int)lastItemPos, 0);
                levelsScroll.inertia = true;
            } else
            {
                Snap(CalculateItemPositionAt(GetNearestIndexToCenter()));
            }
        }
    }

    private Vector2 SnapTo(float itemPos)
    {
        float containerPos = container.anchoredPosition.x;
        float position = Mathf.Lerp(containerPos, itemPos, Time.deltaTime * snapSpeed);

        return new Vector2(position, container.anchoredPosition.y);
    }

    private bool isAtPosition = false;
    private float lastItemPos = 0f;
    private void Snap(float itemPos)
    {
        if (itemPos == lastItemPos)
        {
            if (container.anchoredPosition.x == itemPos)
            {
                isAtPosition = true;
                return;
            } else if (container.anchoredPosition.x >= itemPos + translationFactor)
            {
                //On Left
                container.anchoredPosition -= new Vector2(translationFactor, 0);
            } else if (container.anchoredPosition.x > itemPos - translationFactor &&
                  container.anchoredPosition.x < itemPos + translationFactor)
            {
                levelsScroll.inertia = false;
                container.anchoredPosition = new Vector2(itemPos, 0);
            } else if (container.anchoredPosition.x <= itemPos - translationFactor)
            {
                //On Right
                container.anchoredPosition += new Vector2(translationFactor, 0);
            }
        } else
        {
            lastItemPos = itemPos;
        }
    }

    #region Communication With ScrollRect
    public void BeginDrag()
    {
        isDragging = true;
        isAtPosition = false;
        levelsScroll.inertia = true;
    }
    public void EndDrag() { isDragging = false; }
    #endregion

    #region Controllers
    private void UpdatePositions()
    {
        for (int i = 0; i < items.Length; i++)
        {
            float center = points.Center.GetPosition().x;
            float itemPos = items[i].Rect.position.x;
            positions[i] = center - itemPos;
        }
    }

    private void UpdateDistances()
    {
        for (int i = 0; i < items.Length; i++)
        {
            float center = points.Center.GetPosition().x;
            float itemPos = items[i].Rect.position.x;
            float distance = center - itemPos;
            distances[i] = Mathf.Abs(distance);
        }
    }

    private int GetNearestIndexToCenter()
    {
        int index = 0;
        if(distances == null)
        {
            return index;
        }

        float minDistance = Mathf.Min(distances);

        for (int i = 0; i < items.Length; i++)
        {
            if (minDistance == distances[i])
            {
                index = i;
            }
        }
        return index;
    }

    private float CalculateItemPositionAt(int index)
    {
        float itemPos = items[index].Rect.anchoredPosition.x;

        return -1f * itemPos;
    }
    #endregion

    #region Control via Buttons
    public void SnapToNext()
    {
        if (!isCoroutine)
        {
            isCoroutine = true;
            StartCoroutine(AnimateToRight());
        }
    }

    public void SnapToPrevious()
    {
        if (!isCoroutine)
        {
            isCoroutine = true;
            StartCoroutine(AnimateToLeft());
        }
    }

    private IEnumerator AnimateToRight()
    {
        int expectedPosition = (int)container.anchoredPosition.x - elasticity - 1;
        int currentPosition = (int)container.anchoredPosition.x;

        while (currentPosition >= expectedPosition)
        {
            container.anchoredPosition -= new Vector2(translationFactor, 0f);
            currentPosition = (int)container.anchoredPosition.x;

            yield return new WaitForSeconds(yieldTime);
        }
        isCoroutine = false;
    }

    private IEnumerator AnimateToLeft()
    {
        int expectedPosition = (int)container.anchoredPosition.x + elasticity + 1;
        int currentPosition = (int)container.anchoredPosition.x;

        while (currentPosition <= expectedPosition)
        {
            container.anchoredPosition += new Vector2(translationFactor, 0f);
            currentPosition = (int)container.anchoredPosition.x;

            yield return new WaitForSeconds(yieldTime);
        }
        isCoroutine = false;
    }
    #endregion
}
