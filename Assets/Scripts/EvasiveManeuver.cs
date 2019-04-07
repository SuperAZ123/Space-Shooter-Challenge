using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvasiveManeuver : MonoBehaviour
{
    public Vector2 startwait;
    public float dodge;
    public float smoothing;
    public float tilt;
    public Vector2 maneuverstart;
    public Vector2 maneuverwait;
    public Boundary boundary;

    private float targetmaneuver;
    private float currentSpeed;
    private Rigidbody RB;
 
    void Start()
    {
        RB = GetComponent<Rigidbody>();
        StartCoroutine(Evade());
        currentSpeed = RB.velocity.z;
        
    }

    IEnumerator Evade ()
    {
        yield return new WaitForSeconds(Random.Range(startwait.x, startwait.y));

        while (true)
        {
            targetmaneuver = Random.Range(1, dodge) * -Mathf.Sign(transform.position.x);
            yield return new WaitForSeconds(Random.Range(maneuverstart.x, maneuverstart.y));
            targetmaneuver = 0;
            yield return new WaitForSeconds(Random.Range(maneuverwait.x, maneuverwait.y));
        }
    }


    void FixedUpdate()
    {
        float newManeuver = Mathf.MoveTowards(RB.velocity.x, targetmaneuver, Time.deltaTime * smoothing);
        RB.velocity = new Vector3(newManeuver, 0.0f, currentSpeed);
        RB.position = new Vector3
        (
            Mathf.Clamp(RB.position.x, boundary.xMin, boundary.xMax),
            0.0f,
            Mathf.Clamp(RB.position.z, boundary.zMin, boundary.zMax)
        );

        RB.rotation = Quaternion.Euler(0.0f, 0.0f, RB.velocity.x * tilt);
    }

}
