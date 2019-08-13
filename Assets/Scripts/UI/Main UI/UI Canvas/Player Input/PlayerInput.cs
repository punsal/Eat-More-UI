#pragma warning disable 649

using UnityEngine;
using UnityEngine.UI;

public class PlayerInput : MonoBehaviour
{
    [SerializeField] private InputField playerInput;
    [SerializeField] private Text defaultName;

    private void Start()
    {
        playerInput.shouldHideMobileInput = true;
        playerInput.contentType = InputField.ContentType.Alphanumeric;
        playerInput.characterLimit = 8;
    }

    public void SetDefaultName(string name)
    {
        defaultName.text = name;
        playerInput.text = name;
    }

    public string GetPlayerName()
    {
        if(playerInput.text != "")
        {
            return playerInput.text;
        }
        return defaultName.text;
    }

    public void ControlField()
    {
        if(playerInput.text == string.Empty)
        {
            playerInput.text = defaultName.text;
        }
    }

}
