using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvidenceLogic : MonoBehaviour
{

    public Transform panelTransform;
    // Start is called before the first frame update

    private void OnTriggerEnter(Collider other) {
        
        if (other.gameObject.CompareTag("Player"))
        {
            // Activate the Panel
            panelTransform.gameObject.SetActive(true);

            // Deactivate or destroy the evidence object
            gameObject.SetActive(false); // Or: Destroy(gameObject);
            Debug.Log("Evidence collected");
        }
    }
    
}
