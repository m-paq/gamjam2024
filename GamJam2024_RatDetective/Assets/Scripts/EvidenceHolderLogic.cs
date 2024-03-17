using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvidenceHolderLogic : MonoBehaviour
{
    public Transform panelTransform;
    
        private void Update()
    {
        // Check if the Enter key is pressed
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // Close the canvas by disabling it
            panelTransform.gameObject.SetActive(false);
        }
    }
}
