using UnityEngine;
using UnityEngine.AI;

public class followplayer : MonoBehaviour
{
    public Transform player;
    private NavMeshAgent agent;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
       
    }
    void Update()
    {
        Debug.Log("Player position: " + player.position);
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;
            Debug.Log("Player not found, searching for player...");
        }
    }
}
