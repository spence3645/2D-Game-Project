using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{

    public Text nameText;
    public Text dialogueText;

    public Animator animator;

    public Queue<string> sentences = new Queue<string>();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void StartDialogue(Dialogue dialogue)
    {
        animator.SetBool("isOpen", true);

        nameText.text = dialogue.name;

        sentences.Clear();
        
        foreach(string sentence in dialogue.sentences)
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
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));

        IEnumerator TypeSentence(string s)
        {
            dialogueText.text = "";
            foreach(char letter in s.ToCharArray())
            {
                dialogueText.text += letter;
                yield return null;
            }
        }
    }

    void EndDialogue()
    {
        animator.SetBool("isOpen", false);
    }
}
