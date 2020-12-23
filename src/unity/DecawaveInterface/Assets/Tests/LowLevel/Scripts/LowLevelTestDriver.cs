using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;
using System.Linq;
using System.Threading;

public class LowLevelTestDriver : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Decawave.LowLevel.Interface.Initialize();

        for(int i = 0; i < 100; i++)
        {
            Decawave.Common.AnchorData[] anchors = Decawave.LowLevel.Interface.anchors;
            foreach (Decawave.Common.AnchorData a in anchors)
            {
                Debug.Log("ID: " + a.id);
                Debug.Log("Distance: " + a.distance);
            }
            //Thread.Sleep(100); // TODO: Get rid of this and see what happens
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
