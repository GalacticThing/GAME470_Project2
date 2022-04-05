using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetCube : MonoBehaviour
{
    
    // Build the array for the individual pieces
    public GameObject[] pieces;
    public Transform[] piecesStartPosition;

    public

    // Start is called before the first frame update
    void Start()
    {
        

       
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        { 
            Reset();
        }

    }

    private void Reset() // Restes all pieces to their initial position and rotations ( those of the ResetCube )
    {
        for (int i = 0; i < pieces.Length; i++)
        {
            pieces[i].transform.position = piecesStartPosition[i].transform.position;
            pieces[i].transform.rotation = piecesStartPosition[i].transform.rotation;
            pieces[i].GetComponent<Rigidbody>().isKinematic = true;

        }
    }


}
