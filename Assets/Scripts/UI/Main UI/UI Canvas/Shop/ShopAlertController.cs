using UnityEngine;

public class ShopAlertController : MonoBehaviour
{
    private Animator anim;

    public void PlayAnim()
    {
        anim = GetComponentInParent<Animator>();
        anim.SetTrigger("IsAlert");
    }

    public void StopAnim()
    {
        anim = GetComponentInParent<Animator>();
        anim.SetTrigger("IsIdle");
    }
}
