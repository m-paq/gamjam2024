using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ItemCounterLogic : MonoBehaviour
{
    public TextMeshProUGUI textComponent; 

    private static int _itemCounter = 0;

    // Update is called once per frame
    void Update()
    {
        textComponent.text = _itemCounter + "/5";
    }

    public static void UpdateItemCounter()
    {
        _itemCounter ++;
        if(_itemCounter == 5){
            EndScreenTransitionLogic.GameWon = true;
        }
    }
}
