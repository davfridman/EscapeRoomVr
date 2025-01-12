using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class NumberPadButtonScript : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textBox;
    [SerializeField] private int passwordLength = 4;
    [SerializeField] private string secretPassword;
    [SerializeField] private TeleportFromScript teleportActivation;
    [SerializeField] private bool isActivated = false;
    [SerializeField] private CommanderSoundManagerScript CommanderSoundManager;
    [SerializeField] private ChangeMaterialState ChangeMaterialStateScript;
    [SerializeField] private AudioSource numberBeep;
    [SerializeField] private AudioSource enterBeep;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Activate(){
        isActivated = true;
    }

    public void NumberButtonPressed(int buttonNum)
    {
        if(!isActivated){
            return;
        }
        CommanderSoundManager.PlaySound(5);
        Debug.Log("buttonNum: " + buttonNum);
        if(buttonNum == -1){
            bool isPasswordCorrect = secretPassword.Equals(textBox.text);
            textBox.text = "";
            enterBeep.Play();
            if(isPasswordCorrect){
                teleportActivation.ActivateTeleporter();
                ChangeMaterialStateScript.ChangeMaterial();

            }
        }
        else if(textBox.text.Length < passwordLength){ 
            textBox.text += buttonNum;
            numberBeep.Play();
        }
        
    }

}
