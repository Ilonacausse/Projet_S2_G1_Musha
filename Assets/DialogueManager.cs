using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.InputSystem;
using Unity.VisualScripting;

public class DialogueManager : MonoBehaviour
{
    public Animator animator;
    public TMP_Text nameText;
    public TMP_Text DialogueText;
    private Queue<string> sentences;
    public bool DialogueOuvert = false;


    public static DialogueManager instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("Il y a plus d'une intestence de dialogue manager dans la scene");
            return;
        }
        instance = this;
        sentences = new Queue<string>();
    }
    public void StartDialogue(Dialogue dialogue)
    {
        if (DialogueOuvert == false)
        {
            DialogueOuvert = true;
            animator.SetBool("IsOpen", true);
            nameText.text = dialogue.name;

            sentences.Clear();

            foreach (string sentence in dialogue.sentences)
            {
                sentences.Enqueue(sentence);
            }
            DisplayNextSentences();

        }

    }
    public void Deplacement1(InputAction.CallbackContext context)
    {
        if (context.performed && DialogueOuvert == true)
        {
            DisplayNextSentences();
        }
    }





    public void DisplayNextSentences()
    {
        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }
        string sentence = sentences.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));


        IEnumerator TypeSentence(string sentence)
        {
            DialogueText.text = "";
            foreach (char letter in sentence.ToCharArray())
            {
                DialogueText.text += letter;
                yield return new WaitForSeconds(0.05f);
            }
        }
        void EndDialogue()
        {
            //Debug.Log("Femrer");

            animator.SetBool("IsOpen", false);
            StartCoroutine(delayDialogue());

        }
    }
    IEnumerator delayDialogue()
    {
        yield return new WaitForSeconds(0.5f);
        DialogueOuvert = false;

    }
}