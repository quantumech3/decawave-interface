﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class AnchorPlacementHandler : MonoBehaviour
{
    public ARRaycastManager raycastManager;
    public ARAnchorManager anchorManager;
    private ARAnchor pseudoCloudAnchor; // Will use a local anchor placed at the origin beacon instead of cloud anchors because the game state flow is functionally the same
    // Camera acts as player object for the sake of testing

    bool UserPressedScreen()
    {
        return Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Began;
    }

    // Start is called before the first frame update
    void Start()
    {
        raycastManager = GameObject.Find("AR Session Origin").GetComponent<ARRaycastManager>();
        anchorManager = GameObject.Find("AR Session Origin").GetComponent<ARAnchorManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!pseudoCloudAnchor && UserPressedScreen())
        {
            List<ARRaycastHit> raycastHits = new List<ARRaycastHit>();
            raycastManager.Raycast(new Vector2(Screen.width / 2, Screen.height / 2), raycastHits);

            pseudoCloudAnchor = anchorManager.AddAnchor(raycastHits[0].pose);

            ARPlayerPoseTracker.vioOrigin = pseudoCloudAnchor.gameObject;
        }

        
        
    }
}
