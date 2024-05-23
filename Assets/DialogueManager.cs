using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;





public class DialogueManager : MonoBehaviour
{



    public Text nameText;
    public Text dialogueText;

    public static DialogueManager instance;





    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("Il n'y a plus d'instance de DialogueManager dans la scene");
            return;
        }

        instance = this;
    }
}