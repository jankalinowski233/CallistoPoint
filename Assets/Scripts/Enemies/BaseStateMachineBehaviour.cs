using UnityEngine;
using UnityEngine.AI;

public class BaseStateMachineBehaviour : StateMachineBehaviour
{
    protected Enemy m_Enemy;
    protected NavMeshAgent m_navAgent;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        m_Enemy = animator.GetComponent<Enemy>();
        m_navAgent = animator.GetComponent<NavMeshAgent>();
    }

}
