using System;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class UIController : MonoBehaviour
{
    //different UI screens
    private GameObject player;
    private GameObject rdio;
    //What should the UI be showing?
    //0 = player, 1 = radio, 2 = KP
    public int mode;
    //Keypad Numbers
    public GameObject[] num;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        player = GameObject.Find("Canvas/PlayerUI");
        //rdio = GameObject.Find("Canvas/RadioUI");
        mode = 0;
        //rdio.SetActive(false);
    }

    public void SwitchUI()
    {
        switch (mode)
        {
            case 0:
                mode = 0;
                //rdio.SetActive(false);
                player.SetActive(true);
                break;
            case 1:
                mode = 1;
                player.SetActive(false);
                //rdio.SetActive(true);
                break;
        }
    }
}
