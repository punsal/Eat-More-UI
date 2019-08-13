using UnityEngine;

public class InteractionController : MonoBehaviour
{
    [Header("Character")]
    public Animator characterAnim;
    [Header("Item")]
    public Animator itemAnim;

    [Header("Test Animation")]
    public bool isActive = false;

    private void Update()
    {
        if (isActive)
        {
            isActive = false;
            TriggerBite();
        }

        if (Input.touches.Length > 0 && Input.touches.Length < 2)
        {
            Touch touch = Input.touches[0];
            switch (touch.phase)
            {
                case TouchPhase.Began:
                    TriggerBite();
                    break;
                default:
                    throw new System.Exception("Touch Phase problem.");
            }
        }
    }

    public void TriggerBite()
    {
        characterAnim.SetTrigger("IsBite");
        itemAnim.SetTrigger("IsBite");
    }
}
