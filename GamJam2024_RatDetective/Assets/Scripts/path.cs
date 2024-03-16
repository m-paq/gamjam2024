using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class path : MonoBehaviour
{

    public GameObject rat;
    public GameObject cat;
    public float speed;
    public float pauseDuration= 3f;

    public bool isLoop= true;
    int index = 0;
    public List<Vector3> waypoints;
    private bool isPaused = false;

    private bool isInitialized = false;

    // Update is called once per frame
    void Update()
    {
        if (!isPaused)
        {
            MoveTowardsDestination();
        }
        
        
    }

   // void Start() {
  //      InitializeDirection();
   // }

    void MoveTowardsDestination(){
       /* if (!isInitialized)
            {
                InitializeDirection();
            }*/


        Vector3 destination = waypoints[index]; //.transform.position;

        
        Vector3 newPos = Vector3.MoveTowards(transform.position, destination , speed);
        transform.position= newPos;

        float distance = Vector3.Distance(transform.position, destination);
        
        if( distance <= 0.01){
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
        cat.transform.position = Vector3.MoveTowards(cat.transform.position, rat.transform.position, speed * 2);
    }

    void moveHeadTowardsDirection(){

        

        Vector3 destination = waypoints[index];//.transform.position;
        Debug.Log( destination);
        Vector3 direction = destination - cat.transform.position;

        Quaternion rotation = Quaternion.LookRotation(direction);
        if(direction != Vector3.zero)
        transform.forward = direction;
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


     void InitializeDirection()
    {
        Debug.Log("manger");
        Vector3 destination = waypoints[0];
        Vector3 direction = destination - cat.transform.position;
        Debug.Log( destination);
        Quaternion rotation = Quaternion.LookRotation(direction);
        transform.rotation = rotation;
        isInitialized = true;
    }

}

