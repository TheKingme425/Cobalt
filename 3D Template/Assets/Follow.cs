using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Follow : MonoBehaviour
{
    public NavMeshAgent Enemy;
    public Transform Player;
   // public float health = 10;
    // Start is called before the first frame update
    void Start()
    {
        //health = 10;
    }

    // Update is called once per frame
    void Update()
    {
        Enemy.SetDestination(Player.position);
        Debug.Log("Player Position: " + Player.transform.position);
        //if (health <= 0)
        // {
        //     Destroy(gameObject);
        // }
    }
}

