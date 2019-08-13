using UnityEngine;

/// <summary>
/// Caches PointManager belongs to Item, then distrubutes attributes to controllers.
/// </summary>

public class PointController : MonoBehaviour
{
    private PointManager manager;

    public void SetManager(PointManager manager)
    {
        this.manager = manager;
    }
    public PointManager GetManager()
    {
        return manager;
    }
}
