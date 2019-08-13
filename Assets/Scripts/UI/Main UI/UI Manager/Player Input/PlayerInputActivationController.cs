using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputActivationController : MonoBehaviour
{
    public GameObject playerInput;

    public void ActivatePlayerInput() { playerInput.SetActive(true); }
    public void DeactivatePlayerInput() { playerInput.SetActive(false); }
}
