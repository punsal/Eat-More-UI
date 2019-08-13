#pragma warning disable 649

using UnityEngine;
using UnityEngine.UI;

public class KillIndicator : MonoBehaviour, IIndicate
{
    [SerializeField]
    private Text killText;

    public void ApplyChanges(int data)
    {
        //TODO : cast data, resize panel 
        killText.text = data.ToString();
    }

    public GameObject GetIndicateObject()
    {
        return gameObject;
    }
}
