using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
    [SerializeField]
    private Dialogue dialogue = null; //the dialogue held by this NPC.

    public bool IsLocked { set; get; }

    private DialogueManager manager;

    private void Awake()
    {
        manager = FindObjectOfType<DialogueManager>(); //get the manager component
        IsLocked = false; //dialogue is unlocke dy default
    }

    //called when the player clicks on the mouse
    public void OnClick()
    {
        if (IsLocked)
            return;

        manager.Display(dialogue, this);
    }
}
