using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HathoraGameJam.CubicleEscape
{
    public class TargetGroupManager : MonoBehaviour
    {
        public static TargetGroupManager instance;

        private CinemachineTargetGroup group;

        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }

            if (group == null)
            {
                group = GetComponent<CinemachineTargetGroup>();
            }
        }

        public void AddToGroup(Transform _target, float _weight, float _radius)
        {
            // Create a new target
            CinemachineTargetGroup.Target newTarget = new CinemachineTargetGroup.Target
            {
                target = _target,
                weight = _weight,
                radius = _radius
            };

            // Add the new target to the list
            CinemachineTargetGroup.Target[] newTargets = new CinemachineTargetGroup.Target[group.m_Targets.Length + 1];
            group.m_Targets.CopyTo(newTargets, 0);
            newTargets[newTargets.Length - 1] = newTarget;
            group.m_Targets = newTargets;
        }

        public void RemoveFromGroup(Transform targetTransformToRemove)
        {
            // Find the index of the target with the matching transform
            int targetIndexToRemove = -1;
            for (int i = 0; i < group.m_Targets.Length; i++)
            {
                if (group.m_Targets[i].target == targetTransformToRemove)
                {
                    targetIndexToRemove = i;
                    break;
                }
            }

            // If the target was found, remove it from the array
            if (targetIndexToRemove != -1)
            {
                CinemachineTargetGroup.Target[] newTargets = new CinemachineTargetGroup.Target[group.m_Targets.Length - 1];
                int newIndex = 0;
                for (int i = 0; i < group.m_Targets.Length; i++)
                {
                    if (i != targetIndexToRemove)
                    {
                        newTargets[newIndex] = group.m_Targets[i];
                        newIndex++;
                    }
                }

                // Update the target list of the Cinemachine Target Group
                group.m_Targets = newTargets;
            }
        }

        public int GetTargetIndex(Transform _target)
        {
            int targetIndex = -1;
            for (int i = 0; i < group.m_Targets.Length; i++)
            {
                if (group.m_Targets[i].target == _target)
                {
                    return i;
                }
            }

            return -1;
        }

        public void UpdateWeight(int targetIndex, float newWeight)
        {
            group.m_Targets[targetIndex].weight = newWeight;
        }
    }
}

