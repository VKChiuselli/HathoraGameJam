using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace FSM
{
    [CreateAssetMenu(menuName = "FSM/Decisions/ReachedDesk")]
    public class ReachedDeskDecision : Decision
    {
        public override bool Decide(BaseStateMachine stateMachine)
        {
            var navMeshAgent = stateMachine.GetComponent<NavMeshAgent>();
            return navMeshAgent.remainingDistance == 0;
        }
    }
}