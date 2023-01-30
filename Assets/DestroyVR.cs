using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class DestroyVR : NetworkBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if (!IsOwner)
        {
            this.GetComponent<VRPlayerController>().enabled = false;
        }
            


    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
