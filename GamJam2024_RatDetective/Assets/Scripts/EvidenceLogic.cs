using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EvidenceLogic : MonoBehaviour
{

    public Transform panelTransform;

    private void OnTriggerEnter(Collider other) {

        if (other.gameObject.CompareTag("Player"))
        {
            // Activate the Panel
            panelTransform.gameObject.SetActive(true);

            // Deactivate or destroy the evidence object
            gameObject.SetActive(false); // Or: Destroy(gameObject);

            ItemCounterLogic.UpdateItemCounter();

            Debug.Log("Evidence collected");
        }
    }

    
}
