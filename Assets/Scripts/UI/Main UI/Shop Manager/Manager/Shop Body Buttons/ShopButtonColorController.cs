using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class ShopButtonColorController : ShopButtonBehaviourController
{
    private string hexFormat = "#7D52D6";
    [Header("Color Change")]
    [SerializeField] private Color idleColor = new Color32(0x7D, 0x52, 0xD6, 0xFF);
    [SerializeField] private Color onClickColor = Color.white;

    private Image image;

    // Start is called before the first frame update
    void Start()
    {
        image = GetComponent<Image>();
        IdleAction();
    }

    private void Update()
    {
        base.ButtonBehaviour();
    }
    public override void IdleAction()
    {
        image.color = idleColor;
    }

    public override void OnClickAction()
    {
        image.color = onClickColor;
    }
}
