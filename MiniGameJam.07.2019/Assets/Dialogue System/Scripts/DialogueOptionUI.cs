using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueOptionUI : MonoBehaviour
{
    [SerializeField]
    private Text messageText;

    private Dialogue.Option option;

    //initialize this component
    public void Init (Dialogue.Option option)
    {
        this.option = option;

        messageText.text = option.message;

        gameObject.SetActive(true);
    }

    //called when the player clicks on the dialogue option button
    public void OnClick ()
    {
        DialogueManager.instance.OnOptionClick(option.status);
    }
}
