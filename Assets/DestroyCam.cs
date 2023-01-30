using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.InputSystem.XR;

public class DestroyCam : NetworkBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if (!IsOwner)
        {
            this.GetComponent<Camera>().enabled = false;
            this.GetComponent<AudioListener>().enabled = false;
            this.GetComponent<TrackedPoseDriver>().enabled = false;


        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
