﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
    [SerializeField]
    private Dialogue dialogue = new Dialogue(); //the dialogue held by this NPC.

    private DialogueManager manager;

    private void Awake()
    {
        manager = FindObjectOfType<DialogueManager>(); //get the manager component
    }

    //called when the player clicks on the mouse
    private void OnMouseDown()
    {
        manager.Display(dialogue);
    }
}