using System;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;
using UnityEngine.AI;

[RequireComponent(typeof (NavMeshAgent))]
[RequireComponent(typeof (ThirdPersonCharacter))]
public class PlayerMovement : MonoBehaviour
{
 
    [SerializeField] float attackMeleeRadius = 2f;

	[SerializeField] const int walkableLayer = 8;
	[SerializeField] const int enemyLayer = 9;

    ThirdPersonCharacter m_Character = null;   
    AICharacterControl AIControl = null;
    CameraRaycaster cameraRaycaster = null;
    // create GameObject in order to get a transform to use/set
    GameObject walkTarget; 

// DO we need both now that cameraRaycaster script is changed?
    Vector3 currentClickTarget, clickPoint;

// NEEDED ?
    NavMeshAgent NavMesh;


    private void Start()
    {
        cameraRaycaster = Camera.main.GetComponent<CameraRaycaster>();
        m_Character = GetComponent<ThirdPersonCharacter>();
        AIControl = GetComponent<AICharacterControl>();
        NavMesh = GetComponent<NavMeshAgent>();
        // currentClickTarget = transform.position;
        walkTarget = new GameObject("WalkTarget");

        cameraRaycaster.notifyMouseClickObservers += OnClick;

    }

    // Fixed update is called in sync with physics
    private void FixedUpdate()
    {
        // processMouseMovement();

    }

//     private void processMouseMovement() 
//     {
//         // try mouse button 2 for pathifnding to enemies while still using ThirdPersonController script

// //

//         if (Input.GetMouseButton(0))
//         {
//             cameraRaycaster.notifyMouseClickObservers += OnClick(       );

//             // cameraRaycaster.notifyMouseClickObservers() += clickPoint;
// 			// AIControl.SetTarget(clickPoint.transform);
		
//         }


//     }


    void OnClick(RaycastHit raycastHit, int layerHit)
    {
        switch(layerHit)
        {
            case enemyLayer: 
            // navigate to enemy
                GameObject enemy = raycastHit.collider.gameObject;
                AIControl.SetTarget(enemy.transform);
                // walkTarget.transform.position = enemy.transform.position;
                break;
            case walkableLayer:
            // navigate to clickPoint
                // set walkTarget transform.position as the raycast hit
                walkTarget.transform.position = raycastHit.point;
                AIControl.SetTarget(walkTarget.transform);
                break;
            // case eventLayer:
// // TODO: navigate to Clickable Thing ( i.e. treasure, dead enemy, quest object...)

            //     break;
            default: 
                Debug.LogWarning("Don't know how to handle click, check script.");
            return;
        }
        // AIControl.SetTarget(walkTarget);

    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.black;
        Gizmos.DrawLine(transform.position, walkTarget.transform.position);
        Gizmos.DrawSphere(transform.position, 0.1f);
        Gizmos.DrawSphere(walkTarget.transform.position, 0.1f);
        //
        Gizmos.color = new Color(255f, 0f, 0f, .4f);
        Gizmos.DrawWireSphere(transform.position, attackMeleeRadius);
    }

// Target Enemy, without moving to:
    // void TargetEnemy() 
    // {
    //     if (Input.GetMouseButton(0))
    //     {

    //     }
    // }

}

