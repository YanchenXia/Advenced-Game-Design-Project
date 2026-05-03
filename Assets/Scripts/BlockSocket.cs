using System.Collections.Generic;
using UnityEngine;

public class BlockSocket : MonoBehaviour
{
    [Header("Socket Settings")]
    public Transform exactSnapPoint; 
    
    [Header("The Valid Block Pool")]
    [Tooltip("Add all 9 blocks to this list!")]
    public List<GameObject> validBlocksToAccept; 

    //limits each to 1 block
    private Rigidbody currentlySnappedBlock = null;

    void OnTriggerEnter(Collider other)
    {
        //if socket is taken, ignore
        if (currentlySnappedBlock != null) return;

        //sees if block is part of list
        if (validBlocksToAccept.Contains(other.gameObject))
        {
            Rigidbody rb = other.attachedRigidbody;
            
            if (rb != null)
            {
                Debug.Log("SUCCESS! A valid pool block snapped into place.");
                rb.position = exactSnapPoint.position;
                rb.rotation = exactSnapPoint.rotation;
                
                //stop momentum
                rb.linearVelocity = Vector3.zero;
                rb.angularVelocity = Vector3.zero;

                //lock the socket so other blocks cant enter
                currentlySnappedBlock = rb;
            }
        }
    }

    // 3. If the player pulls the block OUT of the socket (using the tether), unlock it!
    void OnTriggerExit(Collider other)
    {
        if (currentlySnappedBlock != null && other.attachedRigidbody == currentlySnappedBlock)
        {
            Debug.Log("Socket is empty again!");
            currentlySnappedBlock = null; 
        }
    }
}