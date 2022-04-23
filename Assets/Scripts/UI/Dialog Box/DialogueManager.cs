using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI dialogueText;
    public Animator animator;
    public float typingSpeed;
    public int dialogueCounter;

    private Queue<string> sentences;
    private Dialogue currentDialog;

    [SerializeField]
    private Animator[] myAnimationControllers;

    // Start is called before the first frame update
    void Start()
    {
        sentences = new Queue<string>();
        dialogueCounter = 0;
    }

    public void StartDialogue(Dialogue dialogue)
    {
        currentDialog = dialogue;   

        animator.SetBool("IsOpen", true);

        Debug.Log("Starting conversation with " + dialogue.name);

        nameText.text = dialogue.name;

        sentences.Clear();

        foreach( string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if(sentences.Count == 0)
        {
            EndDialogue();
            return;
        }

        string sentence = sentences.Dequeue();
        Debug.Log(sentence);
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
    }

    IEnumerator TypeSentence (string sentence)
    {
        dialogueText.text = "";
        foreach(char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
    }

    void EndDialogue()
    {
        Debug.Log("End of conversation");
        animator.SetBool("IsOpen", false);
        currentDialog.EnvokeOnComplete();

        // Open a door to the next area
        //playAnimation(dialogueCounter);
        dialogueCounter++;

    }
    // public void playAnimation(int animatorNum)
    // {
    //     myAnimationControllers[animatorNum].SetBool("OpenDoor", true);
    // }
}
