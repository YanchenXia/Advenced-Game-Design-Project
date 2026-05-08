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

    //unlock if taken out of socket
    void OnTriggerExit(Collider other)
    {
        if (currentlySnappedBlock != null && other.attachedRigidbody == currentlySnappedBlock)
        {
            currentlySnappedBlock = null; 
        }
    }
}