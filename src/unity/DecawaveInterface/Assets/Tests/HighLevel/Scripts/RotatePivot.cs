using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RotatePivot : MonoBehaviour
{
    public Slider xRot, yRot, zRot;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        this.transform.rotation = Quaternion.Euler(xRot.value, yRot.value, zRot.value); // Apply rotations in order z => y => x
    }
}
