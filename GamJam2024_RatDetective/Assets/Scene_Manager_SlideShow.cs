using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    // Time before switching scene (in seconds)
    public float timeBeforeSwitch = 30f;

    private float elapsedTime = 0f;
    private bool sceneSwitched = false;

    void Update()
    {
        // If the scene has not yet switched and the elapsed time exceeds the timeBeforeSwitch
        if (!sceneSwitched && elapsedTime >= timeBeforeSwitch)
        {
            // Load the next scene
            LoadNextScene();

            // Set the flag to true to prevent loading the scene again
            sceneSwitched = true;
        }
        else
        {
            // Increment the elapsed time
            elapsedTime += Time.deltaTime;
        }
    }

    void LoadNextScene()
    {
        // Get the current scene index
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

        // Calculate the index of the next scene
        int nextSceneIndex = (currentSceneIndex + 1) % SceneManager.sceneCountInBuildSettings;

        // Load the next scene
        SceneManager.LoadScene(nextSceneIndex);
    }
}
