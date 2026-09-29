using UnityEngine;
using UnityEngine.InputSystem;

public class OpenChest : MonoBehaviour
{
    InputAction interactAction;
    bool Opened;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        interactAction = InputSystem.actions.FindAction("Interact");
    }

    // Update is called once per frame
    void Update()
    {
        Search();
    }
    void Search()
    {
        if(interactAction.WasPressedThisFrame())
        {
            bool Opened = true;
        }
        else
        {
            bool Opened = false;
        }
    }
    



}
