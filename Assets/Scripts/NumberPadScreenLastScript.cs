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
    [SerializeField] private AudioSource numberBeep;
    [SerializeField] private AudioSource enterBeep;
    [SerializeField] private AudioSource tryAgainSound;
    [SerializeField] public static bool lastPuzzleDone = false;

    // Start is called before the first frame update
    void Start()
    {

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
        Debug.Log("buttonNum: " + buttonNum);
        if (buttonNum == -1)
        {
            bool isPasswordCorrect = secretPassword[currentPassword].Equals(textBox.text);
            textBox.text = "";
            enterBeep.Play();
            if (isPasswordCorrect)
            {
                currentPassword++;
                if (currentPassword == numberOfPasswords)
                {
                    numberBeep.Play();
                    lastPuzzleDone = true;
                }

            }
            else
            {
                tryAgainSound.Play();
                currentPassword = 0;
            }
        }
        else if (textBox.text.Length < passwordLength)
        {
            textBox.text += buttonNum;
            numberBeep.Play();
        }

    }

}
