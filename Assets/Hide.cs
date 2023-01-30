using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hide : MonoBehaviour
{
    public bool checkFireEx = false;
    public bool checkCampFire = false;
    public bool checkSafetyPin = false;

    
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (!checkFireEx)
            ControllGameObjectsByTag("FireExtinguisher", false, 1, out checkFireEx);
        if (!checkCampFire)
            ControllGameObjectsByTag("Campfire", false, 1, out checkCampFire);
        if (!checkSafetyPin)
            ControllGameObjectsByTag("SafetyPin", false, 1, out checkSafetyPin);

    }

    GameObject FindInActiveObjectByTag(string tag)
    {

        Transform[] objs = Resources.FindObjectsOfTypeAll<Transform>() as Transform[];
        for (int i = 0; i < objs.Length; i++)
        {
            if (objs[i].hideFlags == HideFlags.None)
            {
                if (objs[i].CompareTag(tag))
                {
                    return objs[i].gameObject;
                }
            }
        }
        return null;
    }

    private void ControllGameObjectsByTag(string tag, bool status, int length, out bool check)
    {
        check = false;
        GameObject[] objs = GameObject.FindGameObjectsWithTag(tag);
        if (objs.Length.Equals(length))
        {
            check = true;
            foreach (GameObject obj in objs)
            {
                if(tag == "Campfire")
                {
                    obj.GetComponent<MeshCollider>().enabled = status;
                    obj.GetComponent<SphereCollider>().enabled = status;
                    obj.GetComponent<AudioSource>().enabled = status;
                }
                else
                    obj.GetComponent<Collider>().enabled = status;

                foreach (MeshRenderer mesh in obj.GetComponentsInChildren<MeshRenderer>())
                {
                    mesh.enabled = status;
                }
                foreach (var rigid in obj.GetComponentsInChildren<Rigidbody>())
                {
                    if (rigid != null)
                    {
                        rigid.isKinematic = !status;
                    }
                }
                foreach (var ps in obj.GetComponentsInChildren<ParticleSystem>())
                {
                    if (ps != null)
                    {
                        ps.Stop();
                    }
                }
            }
        }
    }
}
