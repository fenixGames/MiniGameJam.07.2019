using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager instance = null;

    //UI elements:
    [SerializeField]
    private GameObject panel = null;
    [SerializeField]
    private Text messageText = null;
    [SerializeField]
    private GridLayoutGroup optionUIParent = null;
    [SerializeField]
    private DialogueOptionUI optionUIPrefab = null;
    private Stack<DialogueOptionUI> activeOptions = new Stack<DialogueOptionUI>();
    private Stack<DialogueOptionUI> inactiveOptions = new Stack<DialogueOptionUI>();

    //sound effects:
    [SerializeField]
    private AudioClip correctAudio = null;
    [SerializeField]
    private AudioClip wrongAudio = null;

    //current dialogue info:
    private Dialogue currentDialogue = null;
    private NPC currentNPC = null;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(instance);
    }

    //called to display a dialogue
    public void Display (Dialogue dialogue, NPC npc)
    {
        Hide();

        currentDialogue = dialogue;
        currentNPC = npc;

        AudioManager.instance.UpdateVoiceOver(true, dialogue.GetVoiceOver());

        RefreshUI();
    }

    //refresh the UI elements:
    public void RefreshUI ()
    {
        if (!currentDialogue)
            return;

        messageText.text = currentDialogue.GetMessage();

        foreach(Dialogue.Option option in currentDialogue.GetOptions())
        {
            DialogueOptionUI optionUI = (inactiveOptions.Count > 0) ? inactiveOptions.Pop() : null;
            if(optionUI == null)
            {
                optionUI = Instantiate(optionUIPrefab);
                optionUI.transform.SetParent(optionUIParent.transform, true);
            }

            activeOptions.Push(optionUI);

            optionUI.Init(option);
        }

        panel.SetActive(true); 
    }

    public void Hide ()
    {
        currentDialogue = null;
        currentNPC = null;
        panel.SetActive(false);

        AudioManager.instance.UpdateVoiceOver(false);

        while(activeOptions.Count > 0)
        {
            DialogueOptionUI nextOption = activeOptions.Pop();
            nextOption.gameObject.SetActive(false);
            inactiveOptions.Push(nextOption);
        }
    }

    public void OnOptionClick (bool correct)
    {
        AudioManager.instance.UpdateVoiceOver(false);

        //remove health in case of uncorrect answer
        if (correct) //if the answer is correct
        {
            currentNPC.IsLocked = true; //lock the NPC

            AudioManager.instance.PlayAudio(correctAudio);
        }
        else
            AudioManager.instance.PlayAudio(wrongAudio);

        Hide();

        GameSystem.instance.UpdateState(correct); //update the state depending on the answer
    }
}
