#pragma warning disable 649

using UnityEngine;
using UnityEngine.UI;

public class CoinIndicator : MonoBehaviour, IIndicate
{
    public GameObject IndicatorObject { get { return gameObject; } }

    [SerializeField]
    private Text coinText;

    public void ApplyChanges(int data)
    {
        //TODO : cast data, resize panel
        coinText.text = data.ToString();
    }

    public GameObject GetIndicateObject()
    {
        return gameObject;
    }
}
