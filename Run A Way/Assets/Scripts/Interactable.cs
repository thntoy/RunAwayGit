using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Interactable : MonoBehaviour
{
    public KeyCode InteractKey = KeyCode.E;
    public UnityEvent OnInteract; // This is a list of functions that will be called when the event is invoked
    private GameObject _interactKeyPopupPanel;

    [SerializeField] private AudioClip _interactSFX;

    public virtual void Update()
    {
        if (_interactKeyPopupPanel != null)
        {
            Vector3 worldPosition = transform.position + new Vector3(1, 1.0f, 0);
            Vector2 screenPosition = Camera.main.WorldToScreenPoint(worldPosition);
            _interactKeyPopupPanel.GetComponent<RectTransform>().position = screenPosition;
        }
    }
    public virtual void Interact(GameObject interactor)
    {
        OnInteract?.Invoke();

        if (_interactSFX != null)
        {
            AudioManager.Instance.PlayEffect(_interactSFX);
        }
        
        Debug.Log("Interacting with " + gameObject.name);
    }

    public virtual void DisplayInteractKey()
    {
        if (_interactKeyPopupPanel == null)
        {
            GameObject mainCanvas = GameObject.Find("MainCanvas"); // Ensure that the canvas is named "MainCanvas"
            if (mainCanvas != null)
            {
                _interactKeyPopupPanel = Instantiate(Resources.Load("InteractKeyPopup"), mainCanvas.transform) as GameObject;
                _interactKeyPopupPanel.transform.SetAsFirstSibling();
                if (_interactKeyPopupPanel != null)
                {
                    RectTransform popupRectTransform = _interactKeyPopupPanel.GetComponent<RectTransform>();
                }
            }
        }
    }

    public virtual void HideInteractKey()
    {
        if (_interactKeyPopupPanel != null)
        {
            Destroy(_interactKeyPopupPanel);
            _interactKeyPopupPanel = null;
        }
    }
}