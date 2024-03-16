using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Button_sound : MonoBehaviour, IPointerEnterHandler
{
    

public void OnPointerEnter(PointerEventData eventData)
    {
        AkSoundEngine.PostEvent("Play_SFX_bip_main_menu", gameObject);;
    }

  

}
