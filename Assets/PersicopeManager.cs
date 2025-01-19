using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PersicopeManager : MonoBehaviour
{
    [SerializeField] private bool isHoldingLeft = false;
    [SerializeField] private bool isHoldingRight = false;
    [SerializeField] private string sceneToLoad;        // Name of the scene to load
    [SerializeField] private Camera mainCamera;        // The main camera to disable
    [SerializeField] private Camera secondaryCamera;   // The camera to enable
    [SerializeField] private float delayBeforeLoading = 6f; // Delay time before loading the preloaded scene

    private AsyncOperation asyncLoad;

    // Start is called before the first frame update
    void Start()
    {
        if (mainCamera == null || secondaryCamera == null)
        {
            Debug.LogError("Cameras are not assigned!");
            return;
        }
    }

    // Function to start the entire process
    public void StartCameraSwitchAndLoadScene()
    {
        StartCoroutine(SwitchCameraAndLoadScene());
    }

    private IEnumerator SwitchCameraAndLoadScene()
    {
        // Start loading the scene asynchronously (in the background)
        asyncLoad = SceneManager.LoadSceneAsync(sceneToLoad);
        asyncLoad.allowSceneActivation = false;  // Prevent automatic scene activation until we are ready

        // Disable the main camera and enable the secondary camera
        mainCamera.gameObject.SetActive(false);
        secondaryCamera.gameObject.SetActive(true);

        // Wait for the specified time (e.g., 3 seconds)
        yield return new WaitForSeconds(delayBeforeLoading);

        // Now we allow the scene to activate
        asyncLoad.allowSceneActivation = true;

        // Wait until the scene is fully loaded
        while (!asyncLoad.isDone)
        {
            yield return null;
        }

        // Once the scene is fully loaded, we can switch to it
        SceneManager.SetActiveScene(SceneManager.GetSceneByName(sceneToLoad));
    }

    // Call this method to start the process when both hands are holding
    public void hold(bool isLeft)
    {
        if (isLeft)
        {
            isHoldingLeft = true;
        }
        else
        {
            isHoldingRight = true;
        }

        if (isHoldingLeft && isHoldingRight)
        {
            StartCameraSwitchAndLoadScene();
        }
    }

    // Call this method to stop holding when a hand is released
    public void unhold(bool isLeft)
    {
        if (isLeft)
        {
            isHoldingLeft = false;
        }
        else
        {
            isHoldingRight = false;
        }
    }
}
