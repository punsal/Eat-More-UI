using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PlayShineController : MonoBehaviour, IShineController
{
    public ClickAnimationController controller;

    [Header("Play Animation")]
    [Range(0.05f, 10f)]
    public float rate;

    private Animator anim;

    private void OnEnable()
    {
        anim = GetComponent<Animator>();
        InvokeRepeating("Play", 0f, rate);
    }

    public void Play()
    {
        if (controller.State == ClickState.Released)
        {
            anim.SetTrigger("IsShine");
        }
    }
}
