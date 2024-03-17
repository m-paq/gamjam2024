using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class path : MonoBehaviour
{

    public NavMeshAgent ennemy;
    private Transform player;

    public FieldOfView fieldOfView;
    private GameObject rat;
    public GameObject cat;
    public float speed;
    public float pauseDuration= 3f;

    public bool isLoop= true;
    int index = 0;
    public List<Vector3> waypoints;
    private bool isPaused = false;

    private bool isInitialized = false;

    private bool isWalking= false;

    private bool isIdle= false;

    private Animator _animator;
    

    void Start(){
        rat = GameObject.FindGameObjectWithTag("Player");
        player= rat.transform;

        _animator = GetComponentInChildren<Animator>();
        if(_animator == null)
        {
            Debug.LogError("Can't find animator component");
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (!isPaused && !fieldOfView.canSeePlayer)
        {
            isWalking= true;
            isIdle= false;
            _animator.SetBool("isIdle", isIdle);
            _animator.SetBool("isWalking", isWalking);
            MoveTowardsDestination();
        }
        if(fieldOfView.canSeePlayer){
            isWalking= true;
            isIdle= false;
            _animator.SetBool("isIdle", isIdle);
            _animator.SetBool("isWalking", isWalking);
            followPlayer();
        }
        else{
            cancelFollowPlayer();
        }
        
        
    }

   // void Start() {
  //      InitializeDirection();
   // }

    void MoveTowardsDestination(){
        
        isIdle= false;
        isWalking= true;
        if (!isInitialized)
            {
                moveHeadTowardsDirection();
                isInitialized=true;
             
            }
        
        isWalking= true;

        Vector3 destination = waypoints[index]; //.transform.position;

        
        Vector3 newPos = Vector3.MoveTowards(transform.position, destination , speed);
        transform.position= newPos;

        float distance = Vector3.Distance(transform.position, destination);
        
        if( distance <= 0.15){
            isWalking= false;
             isIdle = true;
        
            _animator.SetBool("isWalking", isWalking);
             _animator.SetBool("isIdle", isIdle);
            StartCoroutine(PauseBeforeNextMove());
            moveHeadTowardsDirection();
            
            if(index < waypoints.Count-1){
                
                index++;
            }
            else{
                if( isLoop){
                    index = 0;  
                }
            }
        }

    }

    void followPlayer(){
        //ennemy.updatePosition = true;
        ennemy.SetDestination(player.position);
        
    }

    void cancelFollowPlayer(){
        ennemy.ResetPath();
    }

    void moveHeadTowardsDirection(){
        Vector3 destination = waypoints[index];
        Vector3 direction = cat.transform.position - destination;

        //Quaternion rotation = Quaternion.LookRotation(direction);
        if(direction != Vector3.zero)
             transform.right = direction;
    }

     IEnumerator PauseBeforeNextMove()
    {
        
        
        isPaused = true;
        yield return new WaitForSeconds(pauseDuration);
        isPaused = false;
    }


    private void OnDrawGizmos()
    {
        // Draw spheres at each point
        Gizmos.color = Color.blue;
        foreach (Vector3 point in waypoints)
        {
            Gizmos.DrawSphere(point, 0.1f);
        }

        // Draw lines to connect the points
        int cpt=0;
        Gizmos.color = Color.green;
        for (int i = 0; i < waypoints.Count - 1; i++)
        {
            Gizmos.DrawLine(waypoints[i], waypoints[i + 1]);
            cpt++;
        }

        Gizmos.DrawLine(waypoints[cpt],waypoints[0]);
    }

    private void OnTriggerEnter(Collider other) {

        Debug.Log("Touched player");
        if (other.gameObject.CompareTag("Player"))
        {
            EndScreenTransitionLogic.GameLost = true;
        }
    }

}

