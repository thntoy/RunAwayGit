using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactor : MonoBehaviour
{
    [SerializeField] private LayerMask _interactableLayerMask;
    [SerializeField] private LayerMask _groundLayerMask;
    [SerializeField] private float _interactDistance = 1.0f; // Distance to check for interactables

    [SerializeField] private Interactable _currentInteractable;
    private bool _lockedInteract { get; set; }

    private void OnEnable()
    {
        DialogueManager.OnDialogueStart += LockAndRemoveInteractable;
        DialogueManager.OnDialogueEnd += UnlockInteract;
    }
    
    private void OnDisable()
    {
        DialogueManager.OnDialogueStart -= LockAndRemoveInteractable;
        DialogueManager.OnDialogueEnd -= UnlockInteract;
    }
    
    void Update()
    {
        if(!_lockedInteract)
            CheckForInteractable();

        if(_currentInteractable != null)
        {
            _currentInteractable.DisplayInteractKey();
            if (Input.GetKeyDown(_currentInteractable.InteractKey))
                _currentInteractable.Interact(this.gameObject);
        }

    }

    private void CheckForInteractable()
    {
        Vector2 direction = transform.right; 
        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, _interactDistance, _interactableLayerMask | _groundLayerMask);

        if (hit.collider != null)
        {
            Interactable interactable = hit.collider.GetComponent<Interactable>();
            if (interactable != null && interactable != _currentInteractable)
            {
                if (_currentInteractable != null)
                    _currentInteractable.HideInteractKey();
                _currentInteractable = interactable;
                Debug.Log("Interactable found");    
            }
        }
        else
        {
            if (_currentInteractable != null)
                _currentInteractable.HideInteractKey();
            
            _currentInteractable = null;
        }
    }
    public void LockInteract()
    {
        _lockedInteract = true;
    }

    public void UnlockInteract()
    {
        _lockedInteract = false;
    }

    public void LockAndRemoveInteractable()
    {
        if (_currentInteractable != null)
        {
            _currentInteractable.HideInteractKey();
            _currentInteractable = null;
        }
        _lockedInteract = true;
    }
}