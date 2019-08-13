using System.Collections;
using UnityEngine;

public class ActivationController : MonoBehaviour, IActivate, IDeactivate
{
    public enum State { Active, Deactive};
    public GameObject[] Objects;
    public float yieldTime = 0.75f;

    public void Activate() { StartCoroutine(Activator(State.Active)); }

    public void Deactivate() { StartCoroutine(Activator(State.Deactive)); }

    public IEnumerator Activator(State state)
    {
        yield return new WaitForSeconds(yieldTime);
        switch (state)
        {
            case State.Active:
                foreach (var Object in Objects)
                {
                    Object.SetActive(true);
                }
                break;
            case State.Deactive:
                foreach (var Object in Objects)
                {
                    Object.SetActive(false);
                }
                break;
            default:
                break;
        }
    }
}
