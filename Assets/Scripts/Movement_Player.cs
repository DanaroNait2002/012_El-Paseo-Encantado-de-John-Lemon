using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Movement_Player : MonoBehaviour
{
    [Header("Player")]
    [SerializeField]
    Rigidbody rigidbodyPlayer;

    [Header("Movement Values")]
    [SerializeField]
    Vector3 movementPlayer;
    [SerializeField]
    Vector2 inputMovement;
    [SerializeField]
    float movementVertical;
    [SerializeField]
    float movementHorizontal;
    [SerializeField]
    float turnSpeed = 20f;
    Quaternion rotation = Quaternion.identity;

    [Header("Animations")]
    [SerializeField]
    bool isWalking;
    [SerializeField]
    bool hasVerticalInput;
    [SerializeField]
    bool hasHorizontalInput;
    [SerializeField]
    Animator animatorPlayer;

    [Header("Audio")]
    [SerializeField]
    AudioSource footsteps;
    
    void Start()
    {
        animatorPlayer = GetComponent<Animator>();

        rigidbodyPlayer = GetComponent<Rigidbody>();

        footsteps = GetComponent<AudioSource>();
    }

    public void OnMovement(InputAction.CallbackContext value)
    {
        inputMovement = value.ReadValue<Vector2>();

        movementPlayer.Set(inputMovement.x, 0f, inputMovement.y);
        movementPlayer.Normalize();
    }
    
    void FixedUpdate()
    {
        /*
        //Gives movement a value depending on the key being press on the keyboard
        movementVertical = Input.GetAxis("Vertical");
        movementHorizontal = Input.GetAxis("Horizontal");
        

        //Gives those values to a vector
        movementPlayer.Set(movementHorizontal, 0f, movementVertical);
        movementPlayer.Normalize();
        */
        float horizontal = inputMovement.x;
        float vertical = inputMovement.y;


        //Makes a bool to know when a key is being press
        hasVerticalInput = !Mathf.Approximately(vertical, 0f);
        hasHorizontalInput = !Mathf.Approximately(horizontal, 0f);

        //Makes the bool true if the conditions are true
        isWalking = hasHorizontalInput || hasVerticalInput;

        //Set the animation in case the bool is true
        animatorPlayer.SetBool("IsWalking", isWalking);

        if (isWalking)
        {
            if (!footsteps.isPlaying)
            {
                footsteps.Play();
            }
        }
        else
        {
            footsteps.Stop();
        }

        //I HAVE NO IDEA
        Vector3 desiredForward = Vector3.RotateTowards(transform.forward, movementPlayer, turnSpeed * Time.deltaTime, 0f);

        //Makes the rotation according of what forward is¿?
        rotation = Quaternion.LookRotation(desiredForward);
    }

    void OnAnimatorMove()
    {
        rigidbodyPlayer.MovePosition(rigidbodyPlayer.position + movementPlayer * animatorPlayer.deltaPosition.magnitude);

        rigidbodyPlayer.MoveRotation(rotation);
    }
}
