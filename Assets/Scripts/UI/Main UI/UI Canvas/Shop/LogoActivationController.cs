using UnityEngine;

public class LogoActivationController : MonoBehaviour
{
    public GameObject logo;

    public void ActivateLogo() { logo.SetActive(true); }
    public void DisableLogo() { logo.SetActive(false); }
}
