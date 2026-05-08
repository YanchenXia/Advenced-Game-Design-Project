using System;
using NUnit.Framework;
using UnityEngine;


public class Grappling : MonoBehaviour
{
    //Call backs
    private PlayerMotor pm;
    public Transform cam;
    public Transform gunTip;
    public LayerMask whatIsGrappleable;
    public LineRenderer lr;

    //Grappling
    public float maxGrappleDistance = 100f;
    public float grappleSpeed = 25f;
    public float grappleDelayTime = 0.1f;
    public float overshootYAxis = 2f;

    private Vector3 grapplePoint;

    //Cooldown system
    public float grapplingCd = 1f;
    private float grapplingCdTimer;

    private bool grappling;

    void Start()
    {
        pm = GetComponent<PlayerMotor>();
        lr.enabled = false;
    }

    void Update()
    {
        // Cooldown system
        if (grapplingCdTimer > 0f)
        {
            grapplingCdTimer -= Time.deltaTime;
        }
    }

    void LateUpdate()
    {
        if (!grappling) return;

        lr.SetPosition(0, gunTip.position);
        lr.SetPosition(1, grapplePoint);
    }

    public void StartGrapple()
    {
        if (grapplingCdTimer > 0) return;

        grappling = true;

        RaycastHit hit;
        if (Physics.Raycast(cam.position, cam.forward, out hit, maxGrappleDistance, whatIsGrappleable))
        {
            grapplePoint = hit.point;
            Invoke(nameof(ExecuteGrapple), grappleDelayTime);
        }
        else
        {
            grapplePoint = cam.position + cam.forward * maxGrappleDistance;
            Invoke(nameof(StopGrapple), grappleDelayTime);
        }

        lr.enabled = true;
        lr.positionCount = 2;
    }

    private void ExecuteGrapple()
    {
        Vector3 lowestPoint = transform.position - Vector3.up;

        float yOffset = grapplePoint.y - lowestPoint.y;
        float arcHeight = yOffset + overshootYAxis;

        if (yOffset < 0)
            arcHeight = overshootYAxis;

        pm.JumpToPosition(grapplePoint, grappleSpeed);
    }

    public void StopGrapple()
    {
        if (!grappling) return;

        grappling = false;

        pm.StopGrappleMovement();

        grapplingCdTimer = grapplingCd;

        lr.enabled = false;

        CancelInvoke(nameof(ExecuteGrapple));
    }

}