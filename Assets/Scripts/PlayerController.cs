using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    //action variables
    private InputAction moveIA;
    private InputAction lookIA;
    //camera
    private GameObject cam;
    //move input active
    private bool moving = false;
    //character controller component
    private CharacterController charCont;
    //look coroutine in progress?
    private bool lcip = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        cam = GameObject.Find("Player/Main Camera");
        charCont = this.GetComponent<CharacterController>();
        moveIA = InputSystem.actions.FindAction("Character/Move");
        moveIA.Enable();
        lookIA = InputSystem.actions.FindAction("Character/Look");
        Cursor.lockState = CursorLockMode.Locked;
        lookIA.Enable();
        moveIA.started += StartMove;
        moveIA.canceled += StopMove;
        lookIA.started += StartLook;
        lookIA.canceled += StopLook;
    }

    private void StartMove(InputAction.CallbackContext ctx)
    {
        if (!moving) { moving = true;  StartCoroutine(MoveCoroutine()); }
    }

    private void StopMove(InputAction.CallbackContext ctx)
    {
        moving = false;
    }

    private IEnumerator MoveCoroutine()
    {
        while (moving == true)
        {
            float mx = moveIA.ReadValue<Vector2>().x * 0.3f;
            float my = moveIA.ReadValue<Vector2>().y * 0.3f;
            charCont.Move(transform.TransformDirection(new Vector3(mx != 0 ? mx : 0, 0, my != 0 ? my : 0)) * 150f * Time.deltaTime);
            yield return new WaitForSeconds(0.1f);
        }
        yield break;
    }

    private void StartLook(InputAction.CallbackContext ctx)
    {
        if (lcip == false) { lcip = true; StartCoroutine(LookCoroutine()); }
    }

    private void StopLook(InputAction.CallbackContext ctx)
    {
        StopCoroutine(LookCoroutine());
        lcip = false;
    }

    private IEnumerator LookCoroutine()
    {
        while(lookIA.ReadValue<float>() != 0)
        {
            transform.Rotate(new Vector3(0, lookIA.ReadValue<float>() * 0.5f, 0));
            yield return new WaitForEndOfFrame();
        }
        yield break;
    }
}
