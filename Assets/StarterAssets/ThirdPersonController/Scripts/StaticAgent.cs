using UnityEngine;
using UnityEngine.AI;

public class StaticAgent : MonoBehaviour
{
    NavMeshAgent agent;
    Animator animator;
    [SerializeField]
    private GameObject destinationMarkerPrefab;
    private GameObject currentMarker;
    private LineRenderer pathLine;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        pathLine = GetComponent<LineRenderer>();

    }

    // Update is called once per frame
    void Update()
    {
        // ...existing code...

        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                animator.SetFloat("Speed", 2);
                animator.SetFloat("MotionSpeed", 1.0f);
                agent.SetDestination(hit.point);
                if (currentMarker != null)
                    Destroy(currentMarker);
                if (destinationMarkerPrefab != null)
                    currentMarker = Instantiate(destinationMarkerPrefab, hit.point, Quaternion.identity);
                pathLine.enabled = true;
            }
        }

        //경로 시각화 갱신
        if (agent.hasPath && pathLine.enabled)
        {
            var path = agent.path;
            pathLine.positionCount = path.corners.Length;
            for (int i = 0; i < path.corners.Length; i++)
            {
                pathLine.SetPosition(i, path.corners[i]);
            }
        }
        else
        {
            pathLine.positionCount = 0;
        }

        //목적지 도달 체크
        if (!agent.pathPending && agent.remainingDistance < 0.1f)
        {
            animator.SetFloat("Speed", 0);
            animator.SetFloat("MotionSpeed", 0);
            if (currentMarker != null)
            {
                Destroy(currentMarker);
                currentMarker = null;
            }
            pathLine.enabled = false;
        }
    }

    // ...existing code...
}
