using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard : MonoBehaviour
{
    public Camera _Camera;
    private void LateUpdate()
    {
        //transform.forward = _Camera.transform.forward;
    }
}
