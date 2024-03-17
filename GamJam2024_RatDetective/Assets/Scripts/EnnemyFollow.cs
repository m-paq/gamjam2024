using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class EnnemyFollow : MonoBehaviour
{

    public FieldOfView fieldOfView;
    public NavMeshAgent ennemy;
    public Transform player;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(fieldOfView.canSeePlayer)
            ennemy.SetDestination(player.position);
        
    }
}
