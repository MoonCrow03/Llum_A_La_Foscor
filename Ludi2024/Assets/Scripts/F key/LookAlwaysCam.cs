using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAlwaysCam : MonoBehaviour
{
    //make the object always look at the camera
    [SerializeField] private Transform m_Camera;
    
    void Update()
    {
        transform.LookAt(m_Camera);
    }
}
