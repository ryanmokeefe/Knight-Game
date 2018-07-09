using System;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;
using UnityEngine.AI;
using RPG.CameraUI;


namespace RPG.Characters
{
    [RequireComponent(typeof (NavMeshAgent))]
    [RequireComponent(typeof (ThirdPersonCharacter))]
    public class PlayerMovement : MonoBehaviour
    {
    
        [SerializeField] float attackMeleeRadius = 2f;
        [SerializeField] const int walkableLayer = 8;

        ThirdPersonCharacter m_Character = null;   
        AICharacterControl AIControl = null;
        CameraRaycaster cameraRaycaster = null;
        // GameObject walkTarget = null;  // removing walkTarget with WASD movement
        NavMeshAgent NavMesh;


        private void Start()
        {
            // walkTarget = new GameObject("WalkTarget");
            cameraRaycaster = Camera.main.GetComponent<CameraRaycaster>();
            m_Character = GetComponent<ThirdPersonCharacter>();
            AIControl = GetComponent<AICharacterControl>();
            NavMesh = GetComponent<NavMeshAgent>();
            cameraRaycaster.mouseOverTerrain += MouseOverTerrain;
            cameraRaycaster.mouseOverEnemy += MouseOverEnemy;
        }

        // try mouse button (1) for pathifnding to enemies while still using ThirdPersonController script

        void MouseOverEnemy(Enemy enemy)
        {
            if (Input.GetMouseButtonDown(1))
            {
                AIControl.SetTarget(enemy.transform);
                float distance = (transform.position - enemy.transform.position).magnitude;
                if(distance <= NavMesh.stoppingDistance)
                {
                    AIControl.ResetTarget();
                }
            }
        }

        // void MouseOverTerrain(Vector3 destination)
        void MouseOverTerrain()
        {
            // if (Input.GetMouseButtonDown(0))
            // {
            //     walkTarget.transform.position = destination;
            //     AIControl.SetTarget(walkTarget.transform);
            // }
        }

        void OnDrawGizmos()
        {
            Gizmos.color = Color.black;
            // Gizmos.DrawLine(transform.position, walkTarget.transform.position);
            Gizmos.DrawSphere(transform.position, 0.1f);
            // Gizmos.DrawSphere(walkTarget.transform.position, 0.1f);
            
            Gizmos.color = new Color(255f, 0f, 0f, .4f);
            Gizmos.DrawWireSphere(transform.position, attackMeleeRadius);
        }
    }
}
