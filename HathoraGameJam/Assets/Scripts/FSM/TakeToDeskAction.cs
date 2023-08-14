
using UnityEngine;
using UnityEngine.AI;

namespace FSM
{
    [CreateAssetMenu(menuName = "FSM/Actions/TakeToDesk")]
    public class TakeToDeskAction : Action
    {
        public override void Execute(BaseStateMachine stateMachine)
        {
            var navMeshAgent = stateMachine.GetComponent<NavMeshAgent>();
            var patrolPoints = stateMachine.GetComponent<PatrolPoints>();


            if (!patrolPoints.HeadingToDesk(navMeshAgent))
            {
                navMeshAgent.SetDestination(patrolPoints.deskPoint.position);
            }

        }
    }
}