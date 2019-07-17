using UnityEngine;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour
{
    public Camera mainCamera;

    NavMeshAgent navAgent;

    [HideInInspector] public bool isAlive = true;

    private bool isWalking = false;
    private bool isShooting = false;

    private void Awake()
    {
        navAgent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        if(isAlive == true)
        {
            ProcessMouseInput();
            ProcessPlayerActions();
        }
    }

    void ProcessMouseInput()
    {
        //on LMB hold
        if (Input.GetMouseButton(0) && isWalking == false)
        {
            if(isShooting == false)
            {
                isShooting = true;
            }

            Rotate();
            Shoot();
        }

        //on single RMB click
        if (Input.GetMouseButtonDown(1) && isShooting == false)
        {
            Walk();
        }

        if(Input.GetMouseButtonUp(0))
        {
            isShooting = false;
        }
    }

    void ProcessPlayerActions()
    {
        //check if agent reached destination
        if(navAgent.pathPending == false)
        {
            if(navAgent.remainingDistance <= navAgent.stoppingDistance)
            {
                if(navAgent.hasPath == false || navAgent.velocity.sqrMagnitude == 0)
                {
                    isWalking = false;
                }
            }
        }
    }

    void Walk()
    {
        isWalking = true;   
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit rayHit;

        if (Physics.Raycast(ray, out rayHit))
        {
            navAgent.SetDestination(rayHit.point);
        }
    }

    void Rotate()
    {
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit rayHit;

        if(Physics.Raycast(ray, out rayHit))
        {
            Vector3 clickPoint = rayHit.point;
            Vector3 characterToMouseVector = rayHit.point - transform.position;

            print(characterToMouseVector);
            characterToMouseVector.y = 0f;

            Quaternion rotation = Quaternion.LookRotation(characterToMouseVector);
            transform.rotation = rotation;
        }
    }

    void Shoot()
    {
        print("shooting");
    }
}
