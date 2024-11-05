using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    private bool _isActivated;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !_isActivated)
        {
            other.GetComponent<PlayerController>().SetCurrentCheckpointPos(transform.position);
            _isActivated = true;
        }
    }

}
