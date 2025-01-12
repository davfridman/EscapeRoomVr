using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommanderSoundManagerScript : MonoBehaviour
{
    
    [SerializeField] private AudioSource[] commanderAudios;
    [SerializeField] private float delaySound1 = 5f;
    [SerializeField] private bool[] isAudioFinished;
    [SerializeField] private bool[] isAudioStarted;

    // Start is called before the first frame update
    void Start()
    {

        StartCoroutine(PlaySoundAfterDelay(delaySound1));  // 5-second delay
        for (int i = 0; i < commanderAudios.Length; i++)
        {

            isAudioFinished[i] = false;
            isAudioStarted[i] = false;// Mark audio as finished

        }
    }

    IEnumerator PlaySoundAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);  // Wait for 5 seconds
        commanderAudios[0].Play();                      // Play the sound
    }

    // Update is called once per frame
    void Update()
    {
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
        if (isAudioFinished[i-1] && !isAudioStarted[i])
        {
            isAudioStarted[i] = true;
            commanderAudios[i].Play();                      // Play the sound
        }


    }

}
