using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class M_UI_Manager : MonoBehaviour
{
    public GameObject UI_Object;
    public Text message;
    public Text header;
    public GameObject continueButton;
    public GameObject exitButton;

    public void Enable_UI(bool activate) 
    {
        UI_Object.SetActive(activate);
    }

    private void EnnableContinue(bool canContinue)
    {
        continueButton.SetActive(canContinue);
        exitButton.SetActive(!canContinue);
    }

    public void EditMessage(string header, string message, bool isCorrect)
    {
        this.header.text = header;
        this.message.text = message;
        EnnableContinue(isCorrect);
        Enable_UI(true);
    }

}
