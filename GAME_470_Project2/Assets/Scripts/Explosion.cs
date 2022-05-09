using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    public float explodeForce, radius;
    public AudioSource explode;
   

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("q"))
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
