using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
public class HideObject : NetworkBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if(IsOwner)
        {
            foreach(Transform child in transform)
            {
                child.gameObject.layer = 6;
            }

        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
