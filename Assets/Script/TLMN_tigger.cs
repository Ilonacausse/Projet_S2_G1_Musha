using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class TLMN_tigger : MonoBehaviour
{
    [SerializeField] PlayableDirector cinematic;
    [SerializeField] GameObject Trigger;
    [SerializeField] GameObject Dialogue;





    private void OnTriggerEnter2D(Collider2D collision)
    {
        StartCoroutine(StopMoving());

        cinematic.Play(); 
        Dialogue.SetActive(true);
    }


    IEnumerator StopMoving()
    {
        yield return new WaitForSeconds(5);
        Dialogue.SetActive(false);
        Trigger.SetActive(false);
    }
}
