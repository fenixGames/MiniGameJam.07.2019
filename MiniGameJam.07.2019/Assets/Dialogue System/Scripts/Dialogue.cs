using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu (fileName = "newDialogue", menuName = "MiniJam.07.2019/Dialogue")]
public class Dialogue : ScriptableObject
{
    //message to appear in the dialogue
    [SerializeField]
    private string message = "MESSAGE";
    public string GetMessage () { return message; }

    //list of dialogue options:
    [System.Serializable]
    public struct Option
    {
        public bool status; //is this the right/wrong answer?
        public string message; //the dialogue's option answer
    }
    [SerializeField]
    private List<Option> options = new List<Option>();
    public IEnumerable<Option> GetOptions () { return options; }
}
