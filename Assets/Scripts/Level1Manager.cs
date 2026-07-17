using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;
using static UnityEditor.ShaderGraph.Internal.KeywordDependentCollection;

public class Level1Manager : MonoBehaviour
{
    public static Level1Manager Instance { get; private set; }

    public bool powerOn = false;
    public GameObject lightsObject;

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

    public void TurnPowerOn()
    {
        powerOn = true;
        lightsObject.SetActive(true);
    }
}
