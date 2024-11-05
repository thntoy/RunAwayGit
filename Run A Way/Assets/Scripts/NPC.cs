using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class NPC : Interactable
{
    [SerializeField] private List<Dialogue> _dialogues;
    private Transform _player;

    private void Start()
    {
        _player = GameObject.FindWithTag("Player").transform;
    }

    public override void Interact(GameObject interactor)
    {
        base.Interact(interactor);
        
        if (_dialogues.Count > 0)
            DialogueManager.Instance.StartDialogue(_dialogues[0]);
    }



    public override void Update()
    {
        base.Update();

        if (_player != null)
        {
            Vector3 direction = _player.position - transform.position;

            if (direction.x > 0)
            {
                transform.rotation = Quaternion.Euler(0, 180, 0); // Face right
            }
            else if (direction.x < 0)
            {
                transform.rotation = Quaternion.Euler(0, 0, 0); // Face left
            }
        }
    }
}