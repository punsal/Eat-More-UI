using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ReSharper disable once CheckNamespace
public class NewMapInfoAnimController : MonoBehaviour
{
    public float stopTime = 3f;
    public float increaseThreshold = 1.01f;
    public float decreaseThreshold = 1.25f;
    private readonly WaitForSeconds yieldTime = new WaitForSeconds(0.001f);
    private readonly Vector3 scaleVector = Vector3.one * 0.0005f;

    private bool isIncreasing = false;
    private bool IsIncreasing
    {
        get
        {
            if (transform.localScale.x >= decreaseThreshold && isIncreasing)
            {
                isIncreasing = false;
                return isIncreasing;
            }
            if (transform.localScale.x <= increaseThreshold && !isIncreasing)
            {
                isIncreasing = true;
                return isIncreasing;
            }

            return isIncreasing;
        }
    }

    /// <summary>
    /// Animates bouncing effect. 
    /// </summary>
    public void Animate()
    {
        if (gameObject.activeSelf == false)
        {
            gameObject.SetActive(true);
        }
        PlayerData.instance.SetPlayerLevelUp(0);
        StartCoroutine(Animation());
        Invoke(nameof(StopAnimation), stopTime);
    }

    private IEnumerator Animation()
    {
        while (true)
        {
            if (IsIncreasing)
            {
                transform.localScale += scaleVector;
            }
            else
            {
                transform.localScale -= scaleVector;
            }
            Debug.Log("Animating..");
            yield return yieldTime;
        }
    }

    private void StopAnimation()
    {
        StopCoroutine(Animation());
        gameObject.SetActive(false);
    }
}
