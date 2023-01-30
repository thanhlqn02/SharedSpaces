using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyS : MonoBehaviour
{
    public bool check = true; 
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(check)
        {
            if (GameObject.Find("EventSystem") != null || GameObject.Find("XR Interaction Manager") != null)
            {
/*                Debug.Log(GameObject.Find("EventSystem"));
                Debug.Log(GameObject.Find("XR Interaction Manager"));*/
                Destroy(GameObject.Find("XR Interaction Manager"));
                Destroy(GameObject.Find("EventSystem"));
                if (GameObject.Find("EventSystem") == null && GameObject.Find("XR Interaction Manager") == null)
                    check = false;
            }
        }

    }
}
