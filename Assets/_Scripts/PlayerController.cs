#if UNITY_ANDROID || UNITY_IOS
    #define USING_MOBILE
#endif

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class PlayerController : MonoBehaviour
{
    private Vector3 movement;
    private Animator _animator;
    private Rigidbody _rigidbody;
    [SerializeField]
    private float turnSpeed;
    // Start is called before the first frame update
    private Quaternion rotation = Quaternion.identity;
    public AudioSource _audioSource;
    private NavMeshAgent _navMeshAgent;
    void Start()
    {
        _animator=GetComponent<Animator>();
        _rigidbody=GetComponent<Rigidbody>();
        _audioSource=GetComponent<AudioSource>();
        _navMeshAgent = GetComponent<NavMeshAgent>();
        //plane = GetComponent(typeof(MeshFilter)) as MeshFilter;
    }
    private void Update() 
    {
       bool isWalking=false;
       #if USING_MOBILE
           WalkingMovil(ref isWalking); 
       #else 
           WalkingPC(ref isWalking);
       #endif

        _animator.SetBool("isWalking",isWalking);
        if (isWalking)
        {
            if (!_audioSource.isPlaying)
            { _audioSource.Play();}
        }
        else
        {
            _audioSource.Stop();
        }

        Vector3 desiredForward = Vector3.RotateTowards(transform.forward,movement,turnSpeed * Time.fixedDeltaTime,0);
        rotation = Quaternion.LookRotation(desiredForward);
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }
   
    void WalkingPC(ref bool isWalking)
    {
         float horizontal=Input.GetAxis("Horizontal");
         float vertical=Input.GetAxis("Vertical");
         movement.Set(horizontal,0,vertical);
         movement.Normalize();

          bool hasHorizontalInput = !Mathf.Approximately(horizontal,0);
          bool hasVerticalInput = !Mathf.Approximately(vertical,0);
          isWalking = hasHorizontalInput || hasVerticalInput;
    }
    void WalkingMovil(ref bool isWalking)
    {
        if (Input.GetMouseButton(0))
        {
            
            Plane p = new Plane (Camera.main.transform.forward,transform.position);
            Ray r = Camera.main.ScreenPointToRay (Input.mousePosition);
            float d;
            if (p.Raycast(r,out d))
            {
                Vector3 v = r.GetPoint(d);
                _navMeshAgent.SetDestination(new Vector3 (v.x,0f,v.z));
                //Debug.Log("Punto a Caminar: " + v.x + v.z));
            }
        }
        if (_navMeshAgent.remainingDistance <= _navMeshAgent.stoppingDistance)
            {isWalking=false;}
            else
            { isWalking=true;} 
    }
    private void OnAnimatorMove() 
    {
        //Traigo la velocidad desde la animacion, ya que la animacion se mueve
        _rigidbody.MovePosition(_rigidbody.position+movement * _animator.deltaPosition.magnitude);
        _rigidbody.MoveRotation(rotation);
    }
}
