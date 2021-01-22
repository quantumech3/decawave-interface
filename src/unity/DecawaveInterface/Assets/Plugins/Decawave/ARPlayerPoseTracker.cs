using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.Networking;

public class ARPlayerPoseTracker : NetworkBehaviour
{
    public GameObject vioOrigin; // Set at compile time or runtime
    public GameObject rfOrigin; // Set at compile time
    public GameObject rfGhostPrefab;
    public GameObject vioGhostPrefab;

    [SyncVar]
    public Vector3 rfPosition;

    [SyncVar]
    public Vector3 vioPosition;

    [Command]
    public void CmdSetVIOPosition(Vector3 vioPosition)
    {
        // TODO
    }

    [Command]
    public void CmdSetRFPosition(Vector3 rfPosition)
    {
        // TODO
    }

    // Start is called before the first frame update
    void Start()
    {
        // TODO: Implement this based off the "Start" part of the flowchart in the written portion of design
    }

    // Update is called once per frame
    void Update()
    {
        // TODO: Implement this based off the "Update" part of the flowchart in the written portion of design
    }
}
