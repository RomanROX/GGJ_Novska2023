using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] private float PlayerWalkSpeed;
    [SerializeField] private float PlayerRunSpeed;
    [SerializeField] private float PlayerSpeed;

    [SerializeField] private float Gravity = -9.81f;
    [SerializeField] private float VelocityY;
    [SerializeField] private float JumpHeight;

    public Transform GroundCheck;
    public LayerMask GroundMask;
    public float GroundDistance;

    CharacterController ch;

    public bool IsRunning;

    [SerializeField] private bool IsGrounded;
    // Start is called before the first frame update
    void Start()
    {
        ch = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        InputMovement();
        Jump();
        
    }

    public void InputMovement()
    {
        if (Input.GetKey(KeyCode.LeftShift)) IsRunning = true;
        else IsRunning = false;

        PlayerSpeed = IsRunning ? PlayerRunSpeed : PlayerWalkSpeed;

        Vector2 keybInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;
        Vector3 move = (transform.forward * keybInput.y + transform.right * keybInput.x) * PlayerSpeed;
        ch.Move(move * Time.deltaTime);
    }

    public void Jump()
    {
        IsGrounded = Physics.CheckSphere(GroundCheck.position, GroundDistance, GroundMask);

        if (Input.GetKeyDown(KeyCode.Space) && IsGrounded)
        {

            VelocityY = Mathf.Sqrt(JumpHeight * -1f * Gravity);
            IsGrounded = false;
        }

        VelocityY += Gravity * Time.deltaTime;
        ch.Move(new Vector3(0, VelocityY, 0) * Time.deltaTime);
    }


}
