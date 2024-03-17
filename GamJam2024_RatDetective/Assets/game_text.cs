using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
 
public class Game_text : MonoBehaviour
{
    
    public Texture[] imageArray; 
    private int currentImage;
        
    float deltaTime = 0.0f;
 
    public float timer1 = 5.0f;
    public float timer1Remaining = 5.0f;
    public bool timer1IsRunning = true;
    public string timer1Text;
    
// added ergonomic functionality, 
// escape key to exit, 
// p key or right mouse to pause the timer1
// left mouse or spacebar to skip to next slide
    
    void OnGUI()
{
    int w = Screen.width, h = Screen.height;

    // Define the new dimensions for the imageRect
    float imageWidth = Screen.width / 2;
    float imageHeight = Screen.height / 2;
    Rect imageRect = new Rect((w - imageWidth) / 2, h - imageHeight, imageWidth, imageHeight);

    // Draw the texture with the new dimensions
    GUI.DrawTexture(imageRect, imageArray[currentImage]);

    if (currentImage >= imageArray.Length)
        currentImage = 0;
}

    // Update is called once per frame
    void Update()
    {
        Cursor.visible= false;
        Screen.lockCursor = true;
                
        deltaTime += (Time.unscaledDeltaTime - deltaTime) * 0.1f;
        
            if (Input.GetKey(KeyCode.Escape))
            {
                #if UNITY_EDITOR
                if (EditorApplication.isPlaying)
                {
                    EditorApplication.isPlaying = false;
                }
 
                #else 
                Application.Quit();
                #endif
            }
            
            if (Input.GetMouseButtonDown(0))
            {
                UnityEngine.Debug.Log("Pressed primary button.");
                    currentImage++;
        
                if(currentImage >= imageArray.Length)
                    currentImage = 0;
            }
            
            
            if (Input.GetKey(KeyCode.Space))
            {
                UnityEngine.Debug.Log("Pressed space bar.");
                    currentImage++;
        
                if(currentImage >= imageArray.Length)
                    currentImage = 0;
            }
            
             if (Input.GetMouseButtonDown(1))
            {
                UnityEngine.Debug.Log("Pressed secondary button.");
                timer1IsRunning = !timer1IsRunning;
            }           
            
            if (Input.GetKey (KeyCode.P)) 
            {
                //ispaused = !ispaused;
                timer1IsRunning = !timer1IsRunning;
            }
            
             if (timer1IsRunning)
             
            {
                if (timer1Remaining > 0)
                {
                    timer1Remaining -= Time.deltaTime;
                    
                }
            
                else
                {
                    UnityEngine.Debug.Log("Time has run out!");
                
                    currentImage++;
        
                    if(currentImage >= imageArray.Length)
                        currentImage = 0;
                
                    timer1Remaining = timer1;
                }
            }
        
    }
}
 