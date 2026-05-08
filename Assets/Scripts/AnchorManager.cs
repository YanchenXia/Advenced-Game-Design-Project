using UnityEngine;

public class AnchorManager : MonoBehaviour
{
    //anchorable blocks start anchored
    private bool isAnchored = true;

    //start level
    void Start()
    {
        //find all anchorable objects
        GameObject[] puzzleBlocks = GameObject.FindGameObjectsWithTag("AnchorBlock");

        foreach (GameObject block in puzzleBlocks)
        {
            Rigidbody rb = block.GetComponent<Rigidbody>();
            if (rb != null)
            {
                //freeze anchorable blocks
                rb.isKinematic = true;
            }
        }
    }

    //update runs every frame for keyboard input
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            //unfreeze after F is pressed
            isAnchored = !isAnchored;

            GameObject[] puzzleBlocks = GameObject.FindGameObjectsWithTag("AnchorBlock");

            foreach (GameObject block in puzzleBlocks)
            {
                Rigidbody rb = block.GetComponent<Rigidbody>();
                if (rb != null)
                {
                    rb.isKinematic = isAnchored;
                }
            }
        }
    }
}