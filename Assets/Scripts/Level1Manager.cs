using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;
using static UnityEditor.ShaderGraph.Internal.KeywordDependentCollection;

public class Level1Manager : MonoBehaviour
{
    public static Level1Manager Instance { get; private set; }

    public bool powerOn = false;
    public bool radioGot = false;
    public bool metronomeGot = false;
    public bool soundboardGot = false;
    public bool pianoGot = false;
    public GameObject lightsObject;
    
    public GameObject metronomeUI;
    public GameObject metronomeSprite;

    public GameObject soundboardUI;
    public GameObject soundboardSprite;

    public Sprite radioGotSprite;
    public string radioGotText;
    public Sprite metronomeGotSprite;
    public string metronomeGotText;
    public Sprite soundboardGotSprite;
    public string soundboardGotText;

    private void Awake()
    {
        // If there is an instance, and it's not me, delete myself.

        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GetUpgrade(int whichUpgrade)
    {
        switch (whichUpgrade) {
            case 0:
                EnableRadio();
                break;
            case 1:
                EnableMetronome();
                break;
            case 2:
                EnableSoundboard();
                break;
        }

        DisplayUpgradeGetUI();
    }

    void DisplayUpgradeGetUI()
    {

    }

    void EnableRadio()
    {
        radioGot = true;
    }

    void EnableMetronome()
    {
        metronomeGot = true;

        metronomeUI.SetActive(true);
        metronomeSprite.SetActive(true);
    }

    void EnableSoundboard()
    {
        soundboardGot = true;

        soundboardUI.SetActive(true);
        soundboardSprite.SetActive(true);
    }

    public void TurnPowerOn()
    {
        powerOn = true;
        lightsObject.SetActive(true);
    }
}
