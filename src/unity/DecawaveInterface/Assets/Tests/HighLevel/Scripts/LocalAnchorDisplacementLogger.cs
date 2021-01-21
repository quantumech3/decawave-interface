using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class LocalAnchorDisplacementLogger : MonoBehaviour
{
    public ARRaycastManager raycastManager;
    public ARAnchorManager anchorManager;
    private ARAnchor anchor;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(anchor)
        {
            Debug.LogError("ANCHOR_POS: " + (transform.position - anchor.transform.position));
        }
        else if (Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Began)
        {
            List<ARRaycastHit> raycastHits = new List<ARRaycastHit>();
            raycastManager.Raycast(new Vector2(Screen.width / 2, Screen.height / 2), raycastHits);

            anchor = anchorManager.AddAnchor(raycastHits[0].pose);
        }
    }
}
