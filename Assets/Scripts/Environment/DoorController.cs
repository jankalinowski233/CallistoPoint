using UnityEngine;
using System.Collections;

public class DoorController : MonoBehaviour
{

    private void Awake()
    {
    }

    IEnumerator CheckForInput()
    {
        while(true)
        {
            if(Input.GetKeyDown(KeyCode.E))
            {
                print("Opening door");
            }

            yield return null;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            StartCoroutine(CheckForInput());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            StopCoroutine(CheckForInput());
        }
    }
}
