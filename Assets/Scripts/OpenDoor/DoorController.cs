using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    [SerializeField] private GameObject gate;
    [SerializeField] private GameObject gateFront;
    private Vector3 firstPositionGate;
    void Start()
    {
        firstPositionGate = gateFront.transform.position;
    }

    void Update()
    {
        if(Vector3.Distance(firstPositionGate, gateFront.transform.position) >= 0.01)
        {
            gate.gameObject.SetActive(true);
        }
        else
        {
            gate.gameObject.SetActive(false);
        }
    }
    private void CheckPositionGateFront()
    {

    }
}
