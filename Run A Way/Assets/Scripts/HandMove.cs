using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandMove : StateMachineBehaviour
{
    [SerializeField] private float _moveSpeed = 1.0f;

    Transform _playerTransform;
    Transform _handTransform;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        _handTransform = Hand.Instance.transform;
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
        _handTransform.position = new Vector3(Mathf.MoveTowards(_handTransform.position.x, _playerTransform.position.x, _moveSpeed * Time.deltaTime), _handTransform.position.y, _handTransform.position.z);
        
        if(_handTransform.position.x > _playerTransform.position.x - 0.5f && _handTransform.position.x < _playerTransform.position.x + 0.5f)
        {
            animator.SetTrigger("Attack");
        }
    }
 
}
