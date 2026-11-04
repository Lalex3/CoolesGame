using UnityEngine;
using Lean.Touch;

public class DialogueNPC : MonoBehaviour
{
    public DialogueLine[] dialogueLines;

    private bool playerInRange = false;
    public bool dialougeStarted = false;


    private void Update()
    {
        if(playerInRange && dialougeStarted == false && (Input.GetButtonDown("Jump")))
        {
            DialogueManager.instance.StartDialogue(dialogueLines);
            dialougeStarted = true;
        }

        if(dialougeStarted && DialogueManager.instance.dialogueFinished == true)
        {
            dialougeStarted = false;
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            playerInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        return;
    }
}
