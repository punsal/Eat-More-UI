using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckAnimationController : MonoBehaviour
{
    private Animator anim;

    private void OnEnable()
    {
        anim = GetComponent<Animator>();
    }
    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void CheckItem()
    {
        anim.SetBool("IsChecked", true);
    }

    public void UncheckItem()
    {
        anim.SetBool("IsChecked", false);
    }
}
