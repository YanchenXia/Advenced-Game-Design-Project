using UnityEngine;

public class CheckpointPad : MonoBehaviour
{
    [Header("Checkpoint Settings")]
    public Transform spawnLocation;
    
    [Header("Visuals")]
    public Color inactiveColor = Color.red;
    public Color activeColor = Color.green;

    private bool isActivated = false;
    private Renderer padRenderer;

    void Start()
    {
        padRenderer = GetComponent<Renderer>();
        
        //start with inactive color
        if (padRenderer != null)
        {
            padRenderer.material.color = inactiveColor;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        //trigger if player + not activated yet
        if (!isActivated && other.CompareTag("Player"))
        {
            isActivated = true;

            //change color
            if (padRenderer != null)
            {
                padRenderer.material.color = activeColor;
            }

            //respawn at new location
            PlayerRespawn respawnScript = other.GetComponent<PlayerRespawn>();
            if (respawnScript != null)
            {
                respawnScript.SetRespawnPoint(spawnLocation);
            }
        }
    }
}