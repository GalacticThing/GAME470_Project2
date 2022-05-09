using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    public float explodeForce, radius;
    public AudioSource explode;
    public Solve solve;
    public CubeState cubeState;

    private void Start()
    {
        solve = FindObjectOfType<Solve>();
        cubeState = FindObjectOfType<CubeState>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("q") && !CubeState.autoRotating )
        {
            Explode();
        }
    }

    public void Explode()
    {
        explode.Play();
        
        Collider[] colliders = Physics.OverlapSphere(transform.position, radius);

        foreach (Collider nearby in colliders)
        {
            Rigidbody rigg = nearby.GetComponent<Rigidbody>();
            if (rigg != null)
            {
                rigg.isKinematic = false;
                rigg.AddExplosionForce(explodeForce, transform.position, radius);
            }
        }
    }
}
