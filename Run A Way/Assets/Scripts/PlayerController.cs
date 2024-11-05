using System.Collections;
using Unity.VisualScripting;
using UnityEngine;


public class PlayerController : MonoBehaviour
{
    private Rigidbody2D _rb;
    private Animator _animator;
    private float _moveInput;
    private bool _isRunning;
    private bool _isJumping;
    private bool _isGrounded;

    [SerializeField] private float _walkSpeed = 5f;
    [SerializeField] private float _runSpeed = 10f;
    [SerializeField] private float _groundCheckDistance = 0.1f;
    [SerializeField] private LayerMask _groundLayer;
    [SerializeField] private float _gravity = 2f;
    [SerializeField] private float _jumpHeight = 15f;
    [SerializeField] private AudioClip _jumpSFX;

    private Vector3 _currentCheckpointPos;

    public float ObjectPickedMass; // Mass of the object picked up by the player

    void Start()
    {        
        _rb = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
    }

    void Update()
    {
        _moveInput = Input.GetAxis("Horizontal");
        _isRunning = Input.GetKey(KeyCode.LeftShift);

        if (Input.GetButtonDown("Jump") && _isGrounded)
        {
            _isJumping = true;
        }

        // Flip the sprite based on the direction of movement
        if (_moveInput > 0)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        else if (_moveInput < 0)
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }

        _animator.SetBool("isRunning", _moveInput != 0);
        _animator.SetBool("isGrounded", _isGrounded);
        
    }

    void FixedUpdate()
    {
        // Ground check
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, _groundCheckDistance, _groundLayer);
        _isGrounded = hit.collider != null;

        // Movement
        float speed = _isRunning ? _runSpeed : _walkSpeed;
        _rb.velocity = new Vector2(_moveInput * speed, _rb.velocity.y);
        _rb.AddForce(Vector2.down * _gravity);

        // Jump
        if (_isJumping)
        {
            Jump();
            _isJumping = false; 
        }    
    }

    private void Jump()
    {
        float jumpVelocity; 

        // Calculate the jump velocity based on the mass of the object picked up
        if(ObjectPickedMass > 0)
            jumpVelocity = Mathf.Sqrt(2 * (_jumpHeight * (ObjectPickedMass * 2)) * Mathf.Abs(Physics2D.gravity.y));
        else
            jumpVelocity = Mathf.Sqrt(2 * (_jumpHeight * (1)) * Mathf.Abs(Physics2D.gravity.y));


        _rb.velocity = new Vector2(_rb.velocity.x, 0); // Reset the vertical velocity before applying the jump
        _rb.AddForce(Vector2.up * jumpVelocity, ForceMode2D.Impulse);

        AudioManager.Instance.PlayEffect(_jumpSFX);
    }

    public void Respawn()
    {
        transform.position = _currentCheckpointPos;
    }

    public void SetCurrentCheckpointPos(Vector3 pos)
    {
        _currentCheckpointPos = pos;
    }
}