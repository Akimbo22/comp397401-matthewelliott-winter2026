using KBCore.Refs;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class NPCMovement : MonoBehaviour
{
    [SerializeField, Self] private NavMeshAgent agent;
    [SerializeField] private GameObject[] waypoints;
    private Vector3 destination;
    private int index;

    private void OnValidate()
    {
        this.ValidateRefs();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
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
    }
}
