using System.Collections;
using UnityEngine;
using UnityEngine.Playables;  // Added for PlayableDirector

public class CommanderSoundManagerScript : MonoBehaviour
{
    [SerializeField] private AudioSource[] commanderAudios;
    [SerializeField] private float postCutsceneDelay = 2f;  // Delay after the cutscene ends before playing the sound
    [SerializeField] private bool[] isAudioFinished;
    [SerializeField] private bool[] isAudioStarted;

    // Cutscene-related addition
    [SerializeField] private PlayableDirector cutsceneDirector;  // Reference to the cutscene's timeline

    void Start()
    {
        // Initialize audio states
        for (int i = 0; i < commanderAudios.Length; i++)
        {
            isAudioFinished[i] = false;
            isAudioStarted[i] = false;
        }

        // Cutscene change: Play sound after the cutscene ends instead of a fixed 5-second delay
        if (cutsceneDirector != null)
        {
            cutsceneDirector.stopped += OnCutsceneEnd;  // Subscribe to the 'stopped' event
        }
        else
        {
            Debug.LogError("PlayableDirector is not assigned!");
        }
    }

    void OnCutsceneEnd(PlayableDirector director)
    {
        // Start playing the first sound after a short post-cutscene delay
        StartCoroutine(PlaySoundAfterDelay(postCutsceneDelay));
    }

    IEnumerator PlaySoundAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);  // Wait for the specified delay
        if (commanderAudios.Length > 0)
        {
            commanderAudios[0].Play();  // Play the first sound
            isAudioStarted[0] = true;
        }
    }

    void Update()
    {
        // No changes to the Update method; it still handles finished audio tracking
        for (int i = 0; i < commanderAudios.Length; i++)
        {
            if (!commanderAudios[i].isPlaying && commanderAudios[i].time > 0 && !isAudioFinished[i])
            {
                isAudioFinished[i] = true;  // Mark audio as finished
            }
        }
    }

    public void PlaySound(int i)
    {
        // No changes to this method; it plays sounds sequentially if the previous one has finished
        if (isAudioFinished[i - 1] && !isAudioStarted[i])
        {
            isAudioStarted[i] = true;
            commanderAudios[i].Play();
        }
    }
}
