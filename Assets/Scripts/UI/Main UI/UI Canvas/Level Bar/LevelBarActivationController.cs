using UnityEngine;

public class LevelBarActivationController : MonoBehaviour
{
    public GameObject levelBar;

    public void OpenLevelBar() { levelBar.SetActive(true); }
    public void CloseLevelBar() { levelBar.SetActive(false); }
}
