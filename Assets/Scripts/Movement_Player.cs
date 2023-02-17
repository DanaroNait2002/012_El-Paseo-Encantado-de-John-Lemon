using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement_Player : MonoBehaviour
{
    [Header("Player")]
    [SerializeField]
    Rigidbody rigidbodyPlayer;

    [Header("Movement Values")]
    [SerializeField]
    Vector3 movementPlayer;
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
    
    void Start()
    {
        animatorPlayer = GetComponent<Animator>();

        rigidbodyPlayer = GetComponent<Rigidbody>();
    }

    
    void FixedUpdate()
    {
        //Gives movement a value depending on the key being press on the keyboard
        movementVertical = Input.GetAxis("Vertical");
        movementHorizontal = Input.GetAxis("Horizontal");
        
        //Gives those values to a vector
        movementPlayer.Set(movementHorizontal, 0f, movementVertical);
        movementPlayer.Normalize();

        //Makes a bool to know when a key is being press
        hasVerticalInput = !Mathf.Approximately(movementVertical, 0f);
        hasHorizontalInput = !Mathf.Approximately(movementHorizontal, 0f);

        //Makes the bool true if the conditions are true
        isWalking = hasHorizontalInput || hasVerticalInput;

        //Set the animation in case the bool is true
        animatorPlayer.SetBool("IsWalking", isWalking);

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
