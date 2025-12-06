using UnityEngine;
using UnityEngine.AI;

public class EnemyManager : MonoBehaviour
{
    public Transform target;
    public NavMeshAgent agent;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
    }
}
