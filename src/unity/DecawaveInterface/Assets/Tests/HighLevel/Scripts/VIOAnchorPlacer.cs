using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VIOAnchorPlacer : MonoBehaviour
{
    int ctr = 0;
    Vector3 rf0 = Vector3.zero;
    ARPlayerPoseTracker tracker;

    // Start is called before the first frame update
    void Start()
    {
        tracker = GetComponent<ARPlayerPoseTracker>();
    }

    // Update is called once per frame
    void Update()
    {
        if(ctr < 50)
        {
            rf0 += tracker.rfPosition;
            ctr++;
        }
        else if(ctr == 50)
        {
            ctr++;
            rf0 /= ctr;

            ARPlayerPoseTracker.vioOrigin = Instantiate(new GameObject(), rf0, Quaternion.identity * tracker.rfGhost.GetComponent<RFGhostBehavior>().GetTransform().rotation);
        }
    }
}
