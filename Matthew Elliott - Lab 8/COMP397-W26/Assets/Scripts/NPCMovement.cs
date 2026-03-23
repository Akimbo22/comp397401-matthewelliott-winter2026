using KBCore.Refs;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class NPCMovement : MonoBehaviour
{
    [SerializeField, Self] private NavMeshAgent agent;
    [SerializeField] private GameObject[] waypoints;
    [SerializeField] private NPCStates currentState;
    [SerializeField] private Transform player;
    private Vector3 destination;
    private int index;

    private void OnValidate()
    {
        this.ValidateRefs();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentState = NPCStates.Patrol;
        waypoints = GameObject.FindGameObjectsWithTag("waypoint");
        if (waypoints.Length <= 0)
        {
            return;
        }
        agent.destination = destination = waypoints[index].transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        switch (currentState)
        {
            case NPCStates.Patrol:
                if (waypoints.Length <= 0)
                {
                    return;
                }
                if (Vector3.Distance(transform.position, destination) < 3f)
                {
                    index = (index + 1) % waypoints.Length;
                    destination = waypoints[index].transform.position;
                    agent.destination = destination;
                }
                break;
            case NPCStates.Chase:
                agent.destination = player.position;
                break;
            default:
                break;
        }
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            currentState = NPCStates.Chase;
            player = other.transform;
        }
        
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            currentState = NPCStates.Patrol;
            destination = waypoints[index].transform.position;
            agent.destination = destination;
        }
    }
}

[System.Serializable]
public enum NPCStates
{
    Patrol, Chase
}
