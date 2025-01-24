using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class NumberPadButtonLastScript : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textBox;
    [SerializeField] private int passwordLength = 4;
    [SerializeField] private string[] secretPassword;
    [SerializeField] private int currentPassword = 0;
    [SerializeField] private int numberOfPasswords = 5;
    [SerializeField] private bool isActivated = false;
    [SerializeField] private CommanderSoundManagerScript CommanderSoundManager;
    [SerializeField] private AudioSource numberBeep;
    [SerializeField] private AudioSource enterBeep;
    [SerializeField] private FlagsManagerScript flagsManager;

    // Start is called before the first frame update
    void Start()
    {
        flagsManager.ActivateFlag(0);

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void Activate()
    {
        isActivated = true;
    }

    public void NumberButtonPressed(int buttonNum)
    {
        if (!isActivated)
        {
            return;
        }
        CommanderSoundManager.PlaySound(5);
        Debug.Log("buttonNum: " + buttonNum);
        if (buttonNum == -1)
        {
            bool isPasswordCorrect = secretPassword[currentPassword].Equals(textBox.text);
            textBox.text = "";
            enterBeep.Play();
            if (isPasswordCorrect)
            {
                currentPassword++;
                flagsManager.ActivateFlag(currentPassword);
                if (currentPassword == numberOfPasswords)
                {
                    numberBeep.Play();
                }

            }
        }
        else if (textBox.text.Length < passwordLength)
        {
            textBox.text += buttonNum;
            numberBeep.Play();
        }

    }

}
