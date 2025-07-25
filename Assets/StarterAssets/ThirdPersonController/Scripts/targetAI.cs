using UnityEngine;
using UnityEngine.AI;

public class targetAI : MonoBehaviour
{
    [SerializeField]
    Transform target;
    NavMeshAgent agent;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        if (target != null)
        {
            agent.SetDestination(target.position); //목적지를 정하면 그 위치까지 이동하게 하는 거
        }
        else
        {
            Debug.LogWarning("Target is not assigned in StaticAgent");
        }
        
    }

    // Update is called once per frame
    /*void Update()
    {
        
    }*/
}
