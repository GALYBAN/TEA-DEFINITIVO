using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGravity : MonoBehaviour
{
    [SerializeField] private float gravity = -9.81f;
    [SerializeField] private float jumpHeight = 1.7f;
    private Vector3 playerGravity;

    [SerializeField] private Animator _anim;

    private GroundSensor groundSensor;

    void Start()
    {
        _anim = GetComponentInChildren<Animator>();
        groundSensor = GetComponent<GroundSensor>();
    }

    public void ApplyGravity(CharacterController controller)
    {
        if (!groundSensor.IsGrounded())
        {
            playerGravity.y += gravity * Time.deltaTime;
        }
        else if (groundSensor.IsGrounded() && playerGravity.y < 0)
        {
            _anim.SetBool("IsJumping", false);
            playerGravity.y = -1;
        }

        controller.Move(playerGravity * Time.deltaTime);
    }

    public void Jump()
    {
        if (groundSensor.IsGrounded())
        {
            _anim.SetBool("IsJumping", true);
            playerGravity.y = Mathf.Sqrt(jumpHeight * -2 * gravity);
        }
    }
}
