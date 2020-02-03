using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class M_UI_Manager : MonoBehaviour
{
    public GameObject UI_Object;
    public Text message;
    public Text header;

    public void Enable_UI(bool activate) 
    {
        UI_Object.SetActive(activate);
    }

    public void EditMessage(string header, string message)
    {
        this.header.text = header;
        this.message.text = message;
        Enable_UI(true);
    }

}
