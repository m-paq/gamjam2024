using UnityEngine;
using UnityEngine.SceneManagement;

public class EndScreenTransitionLogic : MonoBehaviour
{
    public static bool GameWon = false;
    public static bool GameLost = false;

    private bool transitionOccurred = false; // Flag to track if transition has occurred

    void Update()
    {
        if (GameWon && !transitionOccurred)
        {
            SceneManager.LoadScene("WinScene");
            transitionOccurred = true; // Set the flag to true to indicate that transition has occurred
        }
        else if (GameLost && !transitionOccurred)
        {
            SceneManager.LoadScene("LoseScene");
            transitionOccurred = true; // Set the flag to true to indicate that transition has occurred
        }
    }
}
