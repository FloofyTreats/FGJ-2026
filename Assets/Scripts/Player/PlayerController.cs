using UnityEngine;
using TMPro;
using System.Collections;

[RequireComponent(typeof(CharacterController))]
public class FirstPersonController : MonoBehaviour
{
    [Header("Movement")]
    public float walkSpeed = 5f;
    public float sprintSpeed = 8f;
    public float crouchSpeed = 3f;
    public float jumpHeight = 1.5f;
    public float gravity = -9.81f;
    public float crouchTransitionSpeed = 10f;

    [Header("Mouse Look")]
    public Camera playerCamera;
    public float mouseSensitivity = 200f;
    public float maxLookAngle = 90f;

    [Header("Miscellaneous")]
    public TextMeshProUGUI captionText;
    public float stepInterval = 200f;

    private CharacterController controller;
    private Vector3 velocity;
    private float xRotation = 0f;
    private Transform standingCameraPos;
    private Transform crouchingCameraPos;
    private bool isCrouching;
    public bool inUI;
    private bool locked;
    private AudioSource audioSource;
    private float untilNextStep;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        audioSource = GetComponent<AudioSource>();

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        standingCameraPos = transform.Find("StandingCamPos");
        crouchingCameraPos = transform.Find("CrouchingCamPos");
        isCrouching = false;
        untilNextStep = stepInterval;
    }

    void Update()
    {
        Vector3 up = transform.TransformDirection(Vector3.up);
        if (!Input.GetButton("Sprint") && Input.GetButtonDown("Crouch"))
        {
            isCrouching = true;
            controller.height = 1f;
        } else if (Input.GetButtonUp("Crouch") && !Physics.Raycast(transform.position, up, 1))
        {
            isCrouching = false;
            controller.height = 2f;
        }

        if (isCrouching)
        {
            playerCamera.transform.localPosition = Vector3.Lerp(playerCamera.transform.localPosition, crouchingCameraPos.localPosition, crouchTransitionSpeed * Time.deltaTime);
        }
        else
        {
            playerCamera.transform.localPosition = Vector3.Lerp(playerCamera.transform.localPosition, standingCameraPos.localPosition, crouchTransitionSpeed * Time.deltaTime);
        }


        Look();
        Move();
    }

    void Look()
    {
        if (inUI || locked)
        {
            return;
        }

        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -maxLookAngle, maxLookAngle);

        playerCamera.transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        transform.Rotate(Vector3.up * mouseX);
    }

    void Move()
    {
        bool grounded = controller.isGrounded;

        if (grounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        if(!inUI && !locked)
        {
            float speed = isCrouching ? crouchSpeed : (Input.GetButton("Sprint") ? sprintSpeed : walkSpeed);

            float x = Input.GetAxis("Horizontal");
            float z = Input.GetAxis("Vertical");

            if (x > 0f || z > 0f) {
                untilNextStep -= speed;

                if(untilNextStep <= 0)
                {
                    PlayFootstep();
                }
            }

            Vector3 move = transform.right * x + transform.forward * z;
            controller.Move(move * speed * Time.deltaTime);
        }

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
   }

    public void ToggleInUI()
    {
        inUI = !inUI;
        if(inUI)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }

    public void LockMovement()
    {
        locked = true;
    }

    public void PlayFootstep()
    {
        audioSource.Play();
        untilNextStep = stepInterval;
    }

    public IEnumerator displayCaption(string text, float time)
    {
        captionText.text = text;
        yield return new WaitForSeconds(time);
        captionText.text = "";
    }
}