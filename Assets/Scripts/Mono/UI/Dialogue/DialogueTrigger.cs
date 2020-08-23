using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public Dialogue dialogue;

    public void OnTriggerStay2D(Collider2D col)
    {
        if(col.tag == "Player" && Input.GetKeyDown(KeyCode.E))
        {
            TriggerDialogue();
        }
    }

    public void TriggerDialogue()
    {
        GameObject.Find("DialogueManager").GetComponent<DialogueManager>().StartDialogue(dialogue);
    }
}
