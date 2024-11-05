using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using Unity.VisualScripting;
using UnityEngine;

public class Rock : Interactable
{
    private Rigidbody2D _rb;
    private FixedJoint2D _joint;

    public bool IsPickedUp { get; private set; }

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _joint = GetComponent<FixedJoint2D>();
    }

    public override void Interact(GameObject interactor)
    {
        base.Interact(interactor);

        if(!IsPickedUp)
            OnPickUp(interactor);
        else
            OnDrop(interactor);
    }

    public void OnPickUp(GameObject picker)
    {
        _joint.connectedBody = picker.GetComponent<Rigidbody2D>();
        _joint.enabled = true;
        IsPickedUp = true;

        picker.GetComponent<PlayerController>().ObjectPickedMass = _rb.mass;
        picker.GetComponent<Interactor>().LockInteract();
        Debug.Log("Picked up");
    }

    public void OnDrop(GameObject picker)
    {
        _joint.connectedBody = null;
        _joint.enabled = false;
        IsPickedUp = false;

        picker.GetComponent<PlayerController>().ObjectPickedMass = 0;
        picker.GetComponent<Interactor>().UnlockInteract();
        Debug.Log("Dropped");
    }


}
