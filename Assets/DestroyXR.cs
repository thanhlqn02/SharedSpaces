using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using UnityEngine.XR.Interaction.Toolkit.Samples.StarterAssets;
using Unity.XR.CoreUtils;

public class DestroyXR : NetworkBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if (!IsOwner)
        {
            this.GetComponent<XROrigin>().enabled = false;
            this.GetComponent<DynamicMoveProvider>().enabled = false;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
