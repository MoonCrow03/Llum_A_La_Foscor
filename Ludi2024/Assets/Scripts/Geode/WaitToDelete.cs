using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaitToDelete : MonoBehaviour
{
    public void Destroy()
    {
        Destroy(gameObject);
    }
}
