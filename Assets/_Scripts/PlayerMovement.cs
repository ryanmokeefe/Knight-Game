using System;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;

[RequireComponent(typeof (ThirdPersonCharacter))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    float stopRadius = 0.2f;
    [SerializeField]
    float attackMeleeRadius = 1f;

    ThirdPersonCharacter m_Character;   // A reference to the ThirdPersonCharacter on the object
    CameraRaycaster cameraRaycaster;
    Vector3 currentClickTarget, clickPoint;
        
    private void Start()
    {
        cameraRaycaster = Camera.main.GetComponent<CameraRaycaster>();
        m_Character = GetComponent<ThirdPersonCharacter>();
        currentClickTarget = transform.position;
    }

    // Fixed update is called in sync with physics
    private void FixedUpdate()
    {
        if (Input.GetMouseButton(0))
        {
            // print("Cursor raycast hit layer: " + cameraRaycaster.hit.collider.gameObject.name.ToString());
            print("Cursor raycast hit layer: " + cameraRaycaster.layerHit);
            clickPoint = cameraRaycaster.hit.point;
            switch (cameraRaycaster.layerHit)
            {
                case Layer.Walkable: 
                // remove ok?
                    // currentClickTarget = clickPoint; 
                    currentClickTarget = ShortDestination(clickPoint, stopRadius); 
                break;
                case Layer.Enemy: 
    // TODO: fix circling/can't find direction to face
                    currentClickTarget = ShortDestination(clickPoint, attackMeleeRadius); 
                    // transform.LookAt(currentClickTarget);
                break;
                default: 
                    print("Error: not a valid layer");
                return;
            }
        }

        WalkToDestination();
    
    }

    private void WalkToDestination() 
    {
        var playerClickPoint = currentClickTarget - transform.position;
        // stop player from moving if magnitude of move is not enough (to stop crazy circling)
        if (playerClickPoint.magnitude >= stopRadius)
        {
            m_Character.Move(playerClickPoint, false, false);
        }
        else 
        {
            m_Character.Move(Vector3.zero, false, false);
        }
        // m_Character.Move(currentClickTarget - transform.position, false, false);
        
    }

    Vector3 ShortDestination(Vector3 destination, float shortening) 
    {
        Vector3 reductionVector = (destination - transform.position).normalized * shortening;
        return destination - reductionVector;
    }

    void OnDrawGizmos()
    {
        print("GIZMOS drawn...");
        // draw movement gizmos
        Gizmos.color = Color.black;
        Gizmos.DrawLine(transform.position, currentClickTarget);
        Gizmos.DrawSphere(currentClickTarget, 0.1f);
        Gizmos.DrawSphere(clickPoint, 0.1f);
        //
        Gizmos.color = new Color(255f, 0f, 0f, .4f);
        Gizmos.DrawWireSphere(transform.position, attackMeleeRadius);
    }

}

