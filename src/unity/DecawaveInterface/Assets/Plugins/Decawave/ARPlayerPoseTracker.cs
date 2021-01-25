using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.Networking;

public class ARPlayerPoseTracker : NetworkBehaviour
{
    public GameObject vioOrigin; // Set at Start or Update
    public string rfOriginName; // Used to find RF origin in scene when player loads into the game
    public GameObject rfOrigin; // Set at Start
    public GameObject rfGhostPrefab;
    public GameObject vioGhostPrefab;

    [SyncVar]
    public Vector3 rfPosition;

    [SyncVar]
    public Vector3 vioPosition;

    private GameObject rfGhost;

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
        Decawave.LowLevel.Interface.Initialize(); // TODO: Delete this
        rfOrigin = GameObject.Find(rfOriginName);
        if (isLocalPlayer && isClient) // TODO: Delete this
        {
            rfGhost = Instantiate(rfGhostPrefab);
            rfGhost.GetComponent<RFGhostBehavior>().rfOrigin = rfOrigin;
        }
    }

    // Update is called once per frame
    void Update()
    {
        // TODO: Implement this based off the "Update" part of the flowchart in the written portion of design
        this.transform.position = rfGhost.GetComponent<RFGhostBehavior>().GetTransform().position;
        this.transform.rotation = rfGhost.GetComponent<RFGhostBehavior>().GetTransform().rotation;
    }
}
