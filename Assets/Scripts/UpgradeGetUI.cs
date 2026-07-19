using TMPro;
using UnityEngine;
using UnityEngine.InputSystem.XR;

public class UpgradeGetUI : MonoBehaviour
{
    public UISpriteAnimation upgradeSprite;
    public TextMeshProUGUI label;
    public string onCloseCaption;
    public FirstPersonController controller;
    public AudioClip connectSound;

    private void Start()
    {
        upgradeSprite = GetComponentInChildren<UISpriteAnimation>();
        label = GetComponentInChildren<TextMeshProUGUI>();
        controller = FindAnyObjectByType<FirstPersonController>();
    }

    private void Update()
    {
        if(gameObject.activeSelf && (Input.GetButtonDown("Cancel") || Input.GetButtonDown("Radio")))
        {
            controller.ToggleInUI();

            Level1Manager.Instance.levelAudio.PlayOneShot(connectSound);

            if(onCloseCaption != null)
            {
                StartCoroutine(controller.displayCaption(onCloseCaption, 5.0f));
            }

            gameObject.SetActive(false);
        }
    }
}
