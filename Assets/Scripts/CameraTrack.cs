using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTrack : MonoBehaviour 
{
    public Transform trackTransform;
    public Transform lookTransform;
    public float kh;

    private void Update()
    {
        if (trackTransform != null & lookTransform != null)
        {
            this.transform.position += (trackTransform.position - this.transform.position) * kh * Time.deltaTime;
            this.transform.LookAt(lookTransform.transform);
        }
    }
}
