using System.Collections;
using UnityEngine;

public class ImageActivationController : MonoBehaviour
{
    private enum State { Active, Deactive };
    public GameObject image;
    public float yieldTime = 0.75f;

    public void ActivateImage() { StartCoroutine(Activator(State.Active)); }
    public void DeactivateImage() { StartCoroutine(Activator(State.Deactive)); }
    private IEnumerator Activator(State state)
    {
        yield return new WaitForSeconds(yieldTime);
        switch (state)
        {
            case State.Active:
                image.SetActive(true);
                break;
            case State.Deactive:
                image.SetActive(false);
                break;
            default:
                break;
        }
    }
}
