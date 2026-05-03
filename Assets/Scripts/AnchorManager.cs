using UnityEngine;

public class AnchorManager : MonoBehaviour
{
    //Anchorable blocks start anchored
    private bool isAnchored = true;

    //Start levl
    void Start()
    {
        //Find all anchorable blocks
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
            //unfreeze after F is clicked
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