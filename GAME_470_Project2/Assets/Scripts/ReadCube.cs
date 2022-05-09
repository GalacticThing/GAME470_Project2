using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReadCube : MonoBehaviour
{
    public GameObject[] pieces;
    public Transform[] piecesStartPosition;
    public GameObject ResetButton;

    public Transform tUp;
    public Transform tDown;
    public Transform tLeft;
    public Transform tRight;
    public Transform tFront;
    public Transform tBack;

    private List<GameObject> frontRays = new List<GameObject>();
    private List<GameObject> backRays = new List<GameObject>();
    private List<GameObject> upRays = new List<GameObject>();
    private List<GameObject> downRays = new List<GameObject>();
    private List<GameObject> leftRays = new List<GameObject>();
    private List<GameObject> rightRays = new List<GameObject>();

    private int layerMask = 1 << 8; // This is the layer mask for the faces of the cube
    CubeState cubeState;
    CubeMap cubeMap;
    AutomaticMove automaticMove;
    public GameObject emptyGO;


    // Start is called before the first frame update
    void Start()
    {
        SetRayTransforms();

        cubeState = FindObjectOfType<CubeState>();
        cubeMap = FindObjectOfType<CubeMap>();
        automaticMove = FindObjectOfType<AutomaticMove>();
        ReadState();
        CubeState.started = true;
        
    }

    // Update is called once per frame
    void Update()
    {
        if (CubeState.started == true && AutomaticMove.moveList.Count == 0 && automaticMove.ResetAvailable)
        {
            for (int i = 0; i < pieces.Length; i++)
            {
                if (pieces[i].transform.position == piecesStartPosition[i].transform.position
                && pieces[i].transform.rotation == piecesStartPosition[i].transform.rotation)
                {
                    print("The cube is a match");
                }
            }
        }
    }

    public void ReadState()
    {
        cubeState = FindObjectOfType<CubeState>();
        cubeMap = FindObjectOfType<CubeMap>();

        //set state of each position in the list if sides so we know
        // what color is in what position

        cubeState.up = ReadFace(upRays, tUp);
        cubeState.down = ReadFace(downRays, tDown);
        cubeState.left = ReadFace(leftRays, tLeft);
        cubeState.right = ReadFace(rightRays, tRight);
        cubeState.front = ReadFace(frontRays, tFront);
        cubeState.back = ReadFace(backRays, tBack);

        // update the map with the found positions
        cubeMap.Set();
        
    }

    void SetRayTransforms()
    {
        // populate the ray lists with raycast eminating from the transform, angled toward the cube
        upRays = BuildRays(tUp, new Vector3(90, 90, 0));
        downRays = BuildRays(tDown, new Vector3(270, 90, 0));
        leftRays = BuildRays(tLeft, new Vector3(0, 180, 0));
        rightRays = BuildRays(tRight, new Vector3(0, 0, 0));
        frontRays = BuildRays(tFront, new Vector3(0, 90, 0));
        backRays = BuildRays(tBack, new Vector3(0, 270, 0));
    }

    List<GameObject> BuildRays(Transform rayTransform, Vector3 direction)
    {
        // The ray count is used to name the rays so we can be sure they are in the right order

        int rayCount = 0;
        List<GameObject> rays = new List<GameObject> ();
        // Creates 9 rays in the shape of the side of the cube , with ray 0 at top left and ray 8 at bottom right

        for (int y = 1; y > -2; y--)
        {
            for (int x = -1; x < 2; x++)
            {
                Vector3 startPos = new Vector3(rayTransform.localPosition.x + x,
                                                rayTransform.localPosition.y + y,
                                                    rayTransform.localPosition.z);
                GameObject rayStart = Instantiate(emptyGO, startPos, Quaternion.identity, rayTransform);
                rayStart.name = rayCount.ToString();
                rays.Add(rayStart);
                rayCount++;
            }
        }
        rayTransform.localRotation = Quaternion.Euler(direction);
        return rays;
    }

    public List<GameObject> ReadFace(List<GameObject> rayStarts, Transform rayTransform)
    {
        
        List<GameObject> facesHit = new List<GameObject>();

        foreach (GameObject rayStart in rayStarts)
        {
            Vector3 ray = rayStart.transform.position;
            RaycastHit hit;

            // Does the ray intersect any objects in the layer mask
            if (Physics.Raycast(ray, rayTransform.forward, out hit, Mathf.Infinity, layerMask)) // Mathf.Infinity
            {
                Debug.DrawRay(ray, rayTransform.forward * hit.distance, Color.yellow);
                facesHit.Add(hit.collider.gameObject);
                //print(hit.collider.gameObject.name);
            }
            else
            {
                Debug.DrawRay(ray, rayTransform.forward * 1000, Color.green);
            }
        }
        //StartCoroutine(CheckIfSolved());
        return facesHit;   
    }

    IEnumerator CheckIfSolved()
    {
        if (CubeState.started == true && AutomaticMove.moveList.Count == 0 && automaticMove.ResetAvailable)
        {
            for (int i = 0; i < pieces.Length; i++)
            {
                if (pieces[i].transform.position == piecesStartPosition[i].transform.position
                && pieces[i].transform.rotation == piecesStartPosition[i].transform.rotation)
                {
                    print("The cube is a match");
                }
            }
        }
        //print("Check has been called");
        yield return null;
    }
}
