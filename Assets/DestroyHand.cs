using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using UnityEngine.XR.Interaction.Toolkit.Samples.StarterAssets;
using UnityEngine.XR.Interaction.Toolkit;

public class DestroyHand : NetworkBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if (!IsOwner)
        {
            this.GetComponent<ActionBasedControllerManager>().enabled = false;
            this.GetComponent<ActionBasedController>().enabled = false;
            this.transform.GetChild(0).GetComponent<XRBaseControllerInteractor>().enabled = false;
            this.transform.GetChild(2).GetComponent<XRBaseControllerInteractor>().enabled = false;
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
