using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[RequireComponent(typeof(CharacterController))]
public class FirstPersonController : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float gravity = -9.81f;
    [SerializeField] private float jumpHeight = 1.5f;

    [Header("Look Settings")]
    [SerializeField] private float mouseSensitivity = 100f;
    [SerializeField] private float upDownLookLimit = -90f;

    [Header("Gamplay")]
    [SerializeField] private LayerMask interactableLayer;
    [SerializeField] private float interactionDistance = 5f;
    [SerializeField] private Text interactText;

    private CharacterController _characterController;
    private Camera _playerCamera;
    private Vector3 _velocity;
    private float _verticalRotation = 0f;



    private void Start()
    {
        _characterController = GetComponent<CharacterController>();

        _playerCamera = GetComponentInChildren<Camera>();

        if (_playerCamera == null)
        {
            Debug.LogError("FirstPersonController: No Camera found in children!");
        }

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        HandleRotation();
        HandleMovement();
        HandleInteraction();
    }

    private void HandleInteraction()
    {
        Ray ray = new Ray(_playerCamera.transform.position, _playerCamera.transform.forward);
        if (Physics.Raycast(ray, out RaycastHit hit, interactionDistance, interactableLayer))
        {
            string objName = hit.collider.gameObject.name;

            interactText.text = $"Use {objName}";

            if (Input.GetMouseButtonDown(0))
            {
                switch (objName)
                {
                    case "Door to Level 1":
                        SceneManager.LoadScene("Level 1");
                        break;
                    case "Door to Outside":
                        SceneManager.LoadScene("Level 0");
                        break;
                    case "Elevator Up":
                        if (SceneManager.GetActiveScene().name == "Level 1")
                        {
                            SceneManager.LoadScene("Level 2");
                        }
                        break;
                    case "Elevator Down":
                        if (SceneManager.GetActiveScene().name == "Level 2")
                        {
                            SceneManager.LoadScene("Level 1");
                        }
                        break;
                    default:
                        Debug.LogError($"Unknown interactable: {objName}");
                        break;
                }

            }
        }
        else
        {
            interactText.text = "";
        }
    }

    private void HandleRotation()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        transform.Rotate(Vector3.up * mouseX);

        _verticalRotation -= mouseY;
        _verticalRotation = Mathf.Clamp(_verticalRotation, upDownLookLimit, 90f);

        if (_playerCamera != null)
        {
            _playerCamera.transform.localRotation = Quaternion.Euler(_verticalRotation, 0f, 0f);
        }
    }

    private void HandleMovement()
    {
        if (_characterController.isGrounded && _velocity.y < 0)
        {
            _velocity.y = -2f;
        }

        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        Vector3 move = transform.right * moveX + transform.forward * moveZ;

        if (Input.GetButtonDown("Jump") && _characterController.isGrounded)
        {
            _velocity.y = Mathf.Sqrt(jumpHeight * 2f * -gravity);
        }

        _velocity.y += gravity * Time.deltaTime;

        _characterController.Move((move * moveSpeed + _velocity) * Time.deltaTime);
    }
}
