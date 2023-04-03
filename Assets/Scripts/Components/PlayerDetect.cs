using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDetect : MonoBehaviour
{
    private Collider selected;

    private void Awake()
    {
        MovementSystem.Instance.ConnectToPlayer();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out IInteractable interactable) && selected != other)
        {
            selected = other;
            StartCoroutine(SpawnItems(interactable));
        }
        
        if (other.TryGetComponent(out ITalking talking))
        {
            talking.Talk();
        }
    }

    private IEnumerator SpawnItems(IInteractable interactable)
    {
        interactable.Enter();
        yield return new WaitForSeconds(0.7f);
        StartCoroutine(SpawnItems(interactable));
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out IInteractable interactable))
        {
            if (other.Equals(selected))
            {
                StopAllCoroutines();
                selected = null;
            }
        }

        if (other.TryGetComponent(out ITalking talking))
        {
            selected = null;
            talking.HideTalk();
        }
    }
}
