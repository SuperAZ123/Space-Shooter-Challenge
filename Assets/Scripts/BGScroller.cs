using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGScroller : MonoBehaviour
{
    public float scrollspeed;
    public float timesizeZ;
    private Vector3 startposition;

    // Start is called before the first frame update
    void Start()
    {
        startposition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        float newPosition = Mathf.Repeat(Time.time * scrollspeed, timesizeZ);
        transform.position = startposition + Vector3.forward * newPosition;
    }
}
