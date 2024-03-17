using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class StopMusicButton : MonoBehaviour, IPointerEnterHandler
{

  
public void OnPointerEnter(PointerEventData eventData)
    {
        AkSoundEngine.PostEvent("Play_SFX_bip_main_menu", gameObject);;
    }

    // Define the Wwise Event ID for the music event you want to stop
    public uint Stop_intro; // You can set this in the Unity Inspector
    
    // Handle the button click event
    public void OnStopMusicButtonClick()
    {
        // Stop the music event using Wwise
        AkSoundEngine.PostEvent(Stop_intro, gameObject);
    }
}
