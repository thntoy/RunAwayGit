using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Hand : MonoBehaviour
{
    public static Hand Instance;
    public static event Action OnStartBossFight;

    [SerializeField] private AudioClip _smashSFX;
    [SerializeField] private Transform _smashPoint;  // The center point of the smash
    [SerializeField] private float _smashRadius = 2f;  // Radius for the overlap sphere

    private void Awake()
    {
        Instance = this;
    }

    public void StartBossFight()
    {
        OnStartBossFight?.Invoke();
    }

    public void Smash()
    {
        AudioManager.Instance.PlayEffect(_smashSFX);
        FeedbackManager.Instance.PlayFeedback("HandSmashFB");

        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(_smashPoint.position, _smashRadius);

        foreach (Collider2D hitCollider in hitColliders)
        {
            if (hitCollider.CompareTag("Player"))
            {
                Debug.Log("Player hit by hand");
                hitCollider.GetComponent<PlayerController>().Respawn();
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (_smashPoint != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(_smashPoint.position, _smashRadius);
        }
    }
}
