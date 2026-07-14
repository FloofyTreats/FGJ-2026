using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering.Universal;

public class PlayerController : MonoBehaviour
{
    //actions
    private InputAction moveAct;
    private InputAction intAct;
    private InputAction rdioAct;
    private InputAction returnAct;
    private InputAction lookAct;
    private InputAction confAct;
    //camera object
    private GameObject cam;
    //Is the player interacting with something?
    private bool busy = true;
    //Is the player moving
    private bool moving = false;
    //Is camera bobbing
    private bool bob = false;
    //Because I got tired of manually typing the "transform.position"
    private Vector3 pos;
    //Radio frequency for puzzles
    //Public so UIController can use it to reflect in the UI
    public int rf = 0;
    //UIController
    private UIController uic;
    //object interaced with
    public GameObject objInt;
    //Character controller
    private CharacterController pC;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        cam = GameObject.Find("Player/Main Camera");
        pos = this.transform.position;
        pC = this.GetComponent<CharacterController>();
        uic = GameObject.Find("Canvas").GetComponent<UIController>();
        rdioAct = InputSystem.actions.FindAction("Character/Radio");
        lookAct = InputSystem.actions.FindAction("Character/Look");
        intAct = InputSystem.actions.FindAction("Character/Interact");
        returnAct = InputSystem.actions.FindAction("Object/Back");
        confAct = InputSystem.actions.FindAction("Object/Confirm");
        SwapModes();
        moveAct.started += StartMove;
        moveAct.canceled += StopMove;
        lookAct.started += Look;
        intAct.performed += Interact;
        returnAct.performed += Back;
    }

    private void SwapModes()
    {
        busy = !busy;
        moveAct = busy == false ? InputSystem.actions.FindAction("Character/Move") : InputSystem.actions.FindAction("Object/Move");
        intAct = busy == false ? InputSystem.actions.FindAction("Character/Interact") : InputSystem.actions.FindAction("Object/Press");
        if (busy == true ) { rdioAct.Disable(); lookAct.Disable(); returnAct.Enable(); confAct.Enable(); }
        else { rdioAct.Enable(); lookAct.Enable(); returnAct.Disable(); confAct.Disable(); }
    }

    private void StartMove(InputAction.CallbackContext ctx)
    {
        if (busy == false)
        {
            moving = true;
            if (bob == false) { StartCoroutine(MoveCoroutine()); }
        }
        else if (uic.mode == 1)
        {
            if (ctx.ReadValue<Vector2>().x < 0 || ctx.ReadValue<Vector2>().y < 0) { rf = rf == 20 ? 20 : rf + 1; }
            else if (ctx.ReadValue<Vector2>().x > 0 || ctx.ReadValue<Vector2>().y > 0) { rf = rf == 0 ? 0 : rf - 1; }
        }
    }

    private void StopMove(InputAction.CallbackContext ctx)
    {
        if (busy == false) { moving = false; }
    }

    private void Look(InputAction.CallbackContext ctx)
    {
        if (!busy)
        {
            StartCoroutine(Look());
        }
    }

    private void Back(InputAction.CallbackContext ctx)
    {
        if (busy) { uic.GetComponent<UIController>().mode = 0; uic.GetComponent<UIController>().SwitchUI(); }
    }

    private IEnumerator Look()
    {
        while (lookAct.ReadValue<float>() != 0f)
        {
            transform.Rotate(new Vector3(0, lookAct.ReadValue<float>()*0.5f, 0));
            yield return new WaitForEndOfFrame();
        }
        yield break;
    }

    private IEnumerator MoveCoroutine()
    {
        bob = true;
        bool bDown = true;
        while (moving == true) 
        {
            float mx = moveAct.ReadValue<Vector2>().x * 0.3f;
            float my = moveAct.ReadValue<Vector2>().y * 0.3f;
            pC.Move(new Vector3(mx != 0 ? mx : 0, 0, my != 0 ? my : 0) * 100f * Time.deltaTime);
            pos = this.transform.position;
            if (cam.transform.position.y >= pos.y + 0.75f) { cam.transform.Translate(new Vector3(0, -0.1f, 0)); bDown = true; yield return new WaitForSeconds(0.1f); continue; }
            else if (cam.transform.position.y <= pos.y + 0.4f) { bDown = false; }
            cam.transform.Translate(new Vector3(0, bDown == true ? -0.1f : 0.1f, 0));
            yield return new WaitForSeconds(0.1f);
        }
        if (cam.transform.position.y < pos.y + 0.75f) { cam.transform.position = new Vector3(pos.x, pos.y + 0.75f, pos.z); }
        bob = false;
        yield break;
    }

    private void Interact(InputAction.CallbackContext ctx)
    {
        /*if (!busy)
        {
            RaycastHit hit;
            Physics.Raycast(pos, transform.forward, out hit, 2);
            if (hit.collider.CompareTag("KP"))
            {
                objInt = hit.collider.gameObject;
                uic.mode = 2;
                uic.SwitchUI();
                busy = true;
            }
        }*/
    }
}
