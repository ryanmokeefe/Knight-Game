using System;
using UnityEngine;
using UnityEngine.AI;
using RPG.Characters;


namespace UnityStandardAssets.Characters.ThirdPerson
{
    [RequireComponent(typeof (UnityEngine.AI.NavMeshAgent))]
    [RequireComponent(typeof (ThirdPersonCharacter))]
    public class AICharacterControl : MonoBehaviour
    {
        public UnityEngine.AI.NavMeshAgent agent { get; private set; }             // the navmesh agent required for the path finding
        public ThirdPersonCharacter character { get; private set; } // the character we are controlling
        [SerializeField] public Transform target;                                    // target to aim for


        private void Start()
        {
            // get the components on the object we need ( should not be null due to require component so no need to check )
            agent = GetComponentInChildren<UnityEngine.AI.NavMeshAgent>();
            character = GetComponent<ThirdPersonCharacter>();

	        agent.updateRotation = false;
	        agent.updatePosition = true;
        }


        private void Update()
        {

            if (target != null)
            {                
                agent.SetDestination(target.position);
                agent.updatePosition = true;
            }
            if (agent.remainingDistance > agent.stoppingDistance)
            {                
                character.Move(agent.desiredVelocity, false, false);
            }
            else
            {     
                agent.updatePosition = false;
                // stop enemies sliding
                if (GetComponent<Enemy>())
                {
                    agent.velocity = Vector3.zero;
                }           
                // character.Move(Vector3.zero, false, false);
            }        
}


        public void SetTarget(Transform target)
        {
            this.target = target;
        }

        public void ResetTarget()
        {
            // SerializedProperty.Reset(target);
            this.target = null;
        }
    }
}
