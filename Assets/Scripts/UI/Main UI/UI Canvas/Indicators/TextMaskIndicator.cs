#pragma warning disable 649

using UnityEngine;
using UnityEngine.UI;

[ExecuteAlways]
[RequireComponent(typeof(Text))]
public class TextMaskIndicator : MonoBehaviour
{
    [SerializeField] private Text indicator;
    private Text maskText;

    // Start is called before the first frame update
    void Start()
    {
        maskText = GetComponent<Text>();
        maskText.text = indicator.text;
    }

    // Update is called once per frame
    void Update()
    {
        maskText.text = indicator.text;
    }
}
