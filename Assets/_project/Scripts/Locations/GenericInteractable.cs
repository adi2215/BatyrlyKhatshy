using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Yarn.Unity;
using Zenject;
using Cinemachine;

public class GenericInteractable : MonoBehaviour
{
    [Inject] GameInputAction _input;
    private InputAction _interactAction;

    public string dialogueNodeName;
    public CinemachineVirtualCamera dialogueCinemachine;
    public CinemachineBrain cinemachineBrain;

    bool playerInRange;
    DialogueRunner runner;
    public GameObject contextClue;

    void Start()
    { 
        runner = GameObject.Find("Pixel Dialogue System").GetComponent<DialogueRunner>();
        _interactAction = _input.PlayerInput.Interact;

        _interactAction.performed += InteractAction_performed; 
        _interactAction.Enable();
        //Debug.Log("Interact");

    }

    public void SwitchCamera()
    {
        CinemachineVirtualCamera currentCamera = cinemachineBrain.ActiveVirtualCamera as CinemachineVirtualCamera;
        if (currentCamera != null)
        {
            currentCamera.Priority = 0;
        }

        dialogueCinemachine.Priority = 10;
    }

    private void InteractAction_performed(InputAction.CallbackContext obj)
    {
        if (playerInRange)
        {
            if (DialogueConcurrencyManager.TryEnter())
            {
                runner.StartDialogue(dialogueNodeName);
                SwitchCamera();
                contextClue.SetActive(false);
            }
            
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out CharacterController character))
        {
            contextClue.SetActive(true);
            playerInRange = true;
        }
        
    }
    private void OnTriggerExit(Collider other)
    {
        if(other.TryGetComponent(out CharacterController character))
        {
            contextClue.SetActive(false);
            playerInRange = false;
        }
    }
}
