using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class LogoShineController : MonoBehaviour, IShineController
{
    [Header("Logo Animation")]
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
        anim.SetTrigger("IsShine");
    }
}
