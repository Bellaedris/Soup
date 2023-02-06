using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buoyancy : MonoBehaviour
{

    private Rigidbody rb;
    public Transform soupPosition;
    [Tooltip("density of the fluid")]
    public float depthBeforeSubmerged = 1f;
    public float displacementAmount = 3f;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        // F_buoyancy = rho * g * V
        // rho - density of the mediaum you are in
        // g - gravity
        // V - volume of fluid directly above the curved surface 

        // V = z * S * n 
        // z - distance to surface
        // S - surface area
        // n - normal to the surface
        float waveOffset = WaveManager.instance.GetWaveLength(transform.position.x, transform.position.z);
        if (transform.position.y < soupPosition.position.y + waveOffset)
        {
            float displacementMultiplier = Mathf.Clamp01((soupPosition.position.y + waveOffset - transform.position.y) / depthBeforeSubmerged) * displacementAmount; 
            Vector3 force = -Physics.gravity * displacementMultiplier;
            rb.AddForce(force, ForceMode.Acceleration);
        }
    }
}
