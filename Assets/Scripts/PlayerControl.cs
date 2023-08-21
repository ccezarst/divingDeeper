using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    private Rigidbody player;
    private float moveSpeed = 130f;
    public bool isRagdolled = false;
    public bool isGrounded = false;
    public bool isFlying = false;
    private bool waiting = false;

    void Start()
    {
        player = this.gameObject.GetComponent<Rigidbody>();
    }


    void OnCollisionStay(Collision hit)
    {
        isGrounded = true;
    }

    private void OnCollisionExit(Collision hit)
    {
        isGrounded = false;
    }
    void FixedUpdate()
    {
        if (isRagdolled == false)
        {
            player.freezeRotation = true;
            // W key
            if (Input.GetKey(KeyCode.W))
            {
                player.AddForce(transform.forward * moveSpeed * Time.deltaTime, ForceMode.VelocityChange);
            }

            // S key
            if (Input.GetKey(KeyCode.S))
            {
                player.AddForce(transform.forward * -moveSpeed * Time.deltaTime, ForceMode.VelocityChange);
            }

            // A key
            if (Input.GetKey(KeyCode.A))
            {
                if (Input.GetKey(KeyCode.W) | Input.GetKey(KeyCode.S))
                {
                    player.AddForce(transform.right * -moveSpeed / 2 * Time.deltaTime, ForceMode.VelocityChange);
                }
                else
                {
                    player.AddForce(transform.right * -moveSpeed * Time.deltaTime, ForceMode.VelocityChange);
                }
            }

            // D key
            if (Input.GetKey(KeyCode.D))
            {
                if (Input.GetKey(KeyCode.W) | Input.GetKey(KeyCode.S))
                {
                    player.AddForce(transform.right * moveSpeed / 2 * Time.deltaTime, ForceMode.VelocityChange);
                }
                else
                {
                    player.AddForce(transform.right * moveSpeed * Time.deltaTime, ForceMode.VelocityChange);
                }
            }
            
            // space key
            if (Input.GetKey(KeyCode.Space) && isGrounded == true)
            {
                player.AddForce(new Vector3(0,1,0) * 10000, ForceMode.Impulse);
            }
        }
    }
}
