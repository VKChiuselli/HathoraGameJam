
using UnityEngine;
using UnityEngine.AI;

namespace FSM
{
    [CreateAssetMenu(menuName = "FSM/Actions/CoolDown")]
    public class CoolDownAction : Action
    {
        public override void Execute(BaseStateMachine stateMachine)
        {
            var enemySightSensor = stateMachine.GetComponent<EnemySightSensor>();
            // Debug.Log("cooldown" + !enemySightSensor.OnCoolDown());
            if (!enemySightSensor.OnCoolDown())
            {
                enemySightSensor.StartCoolDown();
            }
        }
    }
}