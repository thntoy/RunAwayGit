using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventAreaDetector : MonoBehaviour
{
    [SerializeField] private UnityEvent OnEnterArea;
    [SerializeField] private bool _isOneTimeEvent;

    private bool _hasEntered;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !_hasEntered)
        {
            OnEnterArea?.Invoke();
            _hasEntered = _isOneTimeEvent;

            Debug.Log("Player entered the event area");
        }
    }


}
