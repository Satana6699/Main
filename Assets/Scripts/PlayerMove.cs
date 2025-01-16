using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float Speed { private set; get; } = 0f;
    public bool IsJumpForAnimation { private set; get; } = false;
    public bool IsGroundedForAnimation { private set; get; } = false;
    public bool IsFreeFallForAnimation { private set; get; } = false;
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float freeFall = -9.8f;
    [SerializeField] private float jumpHeight = 100f;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundCheckRadius = 0.2f;
    [SerializeField] private LayerMask groundMask;
    
    private CharacterController _characterController;
    private Quaternion _originalRotation;
    private Vector3 _velocity;
    private bool _isGrounded;
    void Start()
    {
        _characterController = GetComponent<CharacterController>();
        _originalRotation = transform.rotation;
    }

    void FixedUpdate()
    {
        _isGrounded = Physics.CheckSphere(groundCheck.position, groundCheckRadius, groundMask);
        
        float moveX = Input.GetAxis("Horizontal");
        Speed = Math.Abs(moveX) * moveSpeed;
        
        if (_isGrounded && _velocity.y < 0)
        {
            _velocity.y = -2f;
        }
        
        Rotate(moveX);
        
        Vector3 move = new Vector3(moveX * moveSpeed  * Time.fixedDeltaTime, 0, 0);
        move = Vector3.ClampMagnitude(move, moveSpeed);
        _characterController.Move(move);
        

        if (Input.GetButton("Jump") && _isGrounded)
        {
            IsJumpForAnimation = true;
            _velocity.y = Mathf.Sqrt(jumpHeight * -2f * freeFall);
        }
        
        _velocity.y += freeFall * Time.fixedDeltaTime;
        _characterController.Move(_velocity * Time.deltaTime);
    }

    void Update()
    {
        CheckLanding();
    }
    
    private void Rotate(float moveX)
    {
        if (moveX != 0)
        {
            float targetRotationY = _originalRotation.eulerAngles.y + (moveX > 0 ? 0f : 180f);
        
            Quaternion targetRotation = Quaternion.Euler(0, targetRotationY, 0);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * 10f);
        }
    }

    private void CheckLanding()
    {
        if (!_isGrounded)
        {
            IsFreeFallForAnimation = true;
        }
        else if (IsFreeFallForAnimation)
        {
            IsFreeFallForAnimation = false;
            IsGroundedForAnimation = true;
        }
        else
        {
            IsGroundedForAnimation = false;
            IsJumpForAnimation = false;
        }
    }
}
