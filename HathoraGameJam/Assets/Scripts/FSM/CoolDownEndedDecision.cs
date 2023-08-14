using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace FSM
{
    [CreateAssetMenu(menuName = "FSM/Decisions/Cool Down Ended")]
    public class CoolDownEndedDecision : Decision
    {
        public override bool Decide(BaseStateMachine stateMachine)
        {
            return true;
        }
    }
}