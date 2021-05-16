using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Vector3 movement;
    private Animator _animator;
    private Rigidbody _rigidbody;
    [SerializeField]
    private float turnSpeed;
    // Start is called before the first frame update
    private Quaternion rotation = Quaternion.identity;
    void Start()
    {
        _animator=GetComponent<Animator>();
        _rigidbody=GetComponent<Rigidbody>();
    }
    private void FixedUpdate() {
        float horizontal=Input.GetAxis("Horizontal");
        float vertical=Input.GetAxis("Vertical");
        movement.Set(horizontal,0,vertical);
        movement.Normalize();

        bool hasHorizontalInput = !Mathf.Approximately(horizontal,0);
        bool hasVerticalInput = !Mathf.Approximately(vertical,0);
        bool isWalking = hasHorizontalInput || hasVerticalInput;
        
        _animator.SetBool("isWalking",isWalking);

        Vector3 desiredForward = Vector3.RotateTowards(transform.forward,movement,turnSpeed * Time.fixedDeltaTime,0);

         rotation = Quaternion.LookRotation(desiredForward);

    }

    private void OnAnimatorMove() 
    {
        //Traigo la velocidad desde la animacion, ya que la animacion se mueve
        _rigidbody.MovePosition(_rigidbody.position+movement * _animator.deltaPosition.magnitude);
        _rigidbody.MoveRotation(rotation);
    }
}
