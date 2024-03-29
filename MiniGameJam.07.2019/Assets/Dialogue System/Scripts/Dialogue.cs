﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System.Linq;

[CreateAssetMenu (fileName = "newDialogue", menuName = "MiniJam.07.2019/Dialogue")]
public class Dialogue : ScriptableObject
{
    //message to appear in the dialogue
    [SerializeField, TextArea]
    private string message = null;
    public string GetMessage () { return message; }

    [SerializeField]
    private AudioClip voiceOver = null;
    public AudioClip GetVoiceOver() { return voiceOver; }

    //list of dialogue options:
    [System.Serializable]
    public struct Option
    {
        public bool status; //is this the right/wrong answer?
        public string message; //the dialogue's option answer
    }
    [SerializeField]
    private List<Option> options = null;
    public IEnumerable<Option> GetOptions () {
        return options.OrderBy<Option, int>(i => Random.Range(-1,1)); }
}
