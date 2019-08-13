#pragma warning disable 649

using UnityEngine;
using UnityEngine.UI;

public class LevelIndicator : MonoBehaviour, IIndicate
{
    [SerializeField]
    private Text levelText;

    public void ApplyChanges(int data)
    {
        levelText.text = data.ToString();
    }

    public GameObject GetIndicateObject()
    {
        return gameObject;
    }
}
