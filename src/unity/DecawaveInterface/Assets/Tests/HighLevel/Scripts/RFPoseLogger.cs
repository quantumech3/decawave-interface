using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Decawave.HighLevel;
using Decawave.Common;
using Decawave;

public class RFPoseLogger : MonoBehaviour
{
    public static Vector3[] ANCHOR_POSITIONS = new Vector3[]
    {
            new Vector3(0, 1.5f, 6f),
            new Vector3(3f, 1.5f, 6f),
            new Vector3(3f, 1.5f, 0),
            new Vector3(0, 0, 0)
    };

    public static Dictionary<int, Vector3> ANCHORS = new Dictionary<int, Vector3>();

    // Start is called before the first frame update
    void Start()
    {
        ANCHORS.Add(0xA92, new Vector3(3f, 1.5f, 6f));
        ANCHORS.Add(0x8B08, new Vector3(3f, 1.5f, 0));
        ANCHORS.Add(0xC70C, new Vector3(0, 1.5f, 6f));
        ANCHORS.Add(0xCC93, new Vector3(0, 0, 0));

        Decawave.LowLevel.Interface.Initialize();
    }

    // Update is called once per frame
    void Update()
    {
        AnchorData[] distances = Decawave.LowLevel.Interface.anchors;
        if (true) // distances.Length == 4)
        {
            Debug.LogError("RF POSITION: " + RF.Trilaterate(ANCHORS, distances));
        }
    }
}
