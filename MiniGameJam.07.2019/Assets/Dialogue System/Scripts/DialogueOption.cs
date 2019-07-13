using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueOptionUI : MonoBehaviour
{
    [SerializeField]
    private Text messageText;

    private Dialogue.Option option;
    private DialogueManager manager;

    //initialize this component
    public void Init (DialogueManager manager, Dialogue.Option option)
    {
        this.manager = manager;
        this.option = option;

        messageText.text = option.message;
    }

    //called when the player clicks on the dialogue option button
    public void OnClick ()
    {
        manager.OnOptionClick(option.status);
    }
}
