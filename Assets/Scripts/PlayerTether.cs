using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class PlayerTether : MonoBehaviour
{
    [Header("Tether Settings")]
    public float maxAimDistance = 15f; 
    public float pullForce = 50f;
    public KeyCode tetherKey = KeyCode.E;
    public Transform playerCamera; 

    private Rigidbody tetheredBlock;
    private float ropeLength;
    private LineRenderer laserLine;

    void Start()
    {
        laserLine = GetComponent<LineRenderer>();
        laserLine.startWidth = 0.05f;
        laserLine.endWidth = 0.05f;
        laserLine.enabled = false;
        
        //find camera
        if (playerCamera == null)
        {
            playerCamera = Camera.main.transform;
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(tetherKey))
        {
            ShootTether();
        }

        if (Input.GetKeyUp(tetherKey))
        {
            BreakTether();
        }

        if (tetheredBlock != null)
        {
            //draw line below camera
            laserLine.SetPosition(0, playerCamera.position - new Vector3(0, 0.5f, 0)); 
            laserLine.SetPosition(1, tetheredBlock.position);
        }
    }

    void FixedUpdate()
    {
        if (tetheredBlock != null)
        {
            float currentDistance = Vector3.Distance(transform.position, tetheredBlock.position);

            //drag block
            if (currentDistance > ropeLength)
            {
                Vector3 pullDirection = (transform.position - tetheredBlock.position).normalized;
                tetheredBlock.AddForce(pullDirection * pullForce, ForceMode.Acceleration);
            }
        }
    }

    void ShootTether()
    {
        RaycastHit hit;
        
        //shoot laser where camera is looking
        if (Physics.SphereCast(playerCamera.position, 0.5f, playerCamera.forward, out hit, maxAimDistance))
        {
            Rigidbody hitRb = hit.collider.GetComponent<Rigidbody>();

            if (hitRb != null && !hit.collider.CompareTag("Player"))
            {
                tetheredBlock = hitRb;
                ropeLength = Vector3.Distance(transform.position, tetheredBlock.position);
                laserLine.enabled = true; 
            }
        }
    }

    void BreakTether()
    {
        tetheredBlock = null;
        laserLine.enabled = false; 
    }
}