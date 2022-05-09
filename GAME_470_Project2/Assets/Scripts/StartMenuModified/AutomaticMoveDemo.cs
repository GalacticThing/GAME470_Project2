using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutomaticMoveDemo : MonoBehaviour
{
    public static List<string> moveList = new List<string>() { "U", "U", "D", "D"};
    private readonly List<string> allMoves = new List<string>()
    { "U", "D", "L", "R", "F", "B",
     "U'", "D'", "L'", "R'", "F'", "B'" // omits 180 degree turns so every rotation has only one click
    /*,"U2", "D2", "L2", "R2", "F2", "B2"*/
    };

    private CubeState cubeState;
    private ReadCube readCube;
    private PivotRotation pivotRotation;


    private bool boopIsPlaying;

    int shuffletime = 0;

    // Start is called before the first frame update
    void Start()
    {
        cubeState = FindObjectOfType<CubeState>();
        readCube = FindObjectOfType<ReadCube>();
        pivotRotation = FindObjectOfType<PivotRotation>();
        Shuffle();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
       if (moveList.Count > 0  && !CubeState.autoRotating && CubeState.started )
        {
            // Do the move at the first index;
            DoMove(moveList[0]);

            // remove the move at the first index
            moveList.Remove(moveList[0]);
        }

        
        if (shuffletime >= 10)
        {
            shuffletime = 0;
            StartCoroutine(RestartShuffle());
        }

    }

    public IEnumerator RestartShuffle()
    {
        Shuffle();
        yield return null;
    }
    public void Shuffle()
    {
        List<string> moves = new List<string>();
        int shuffleLength = 10;
        shuffletime = 0;

        for (int i = 0; i < shuffleLength; i++)
        {
            
            int randomMove = Random.Range(0, allMoves.Count);
            moves.Add(allMoves[randomMove]);
            shuffletime++;
            
            
        }
        moveList = moves;
    }

    void DoMove(string move) // contains the instructions for every autorotate command as left turn, right turn, and 180 turn
    {
        
        readCube.ReadState();
        //readStartCube.ReadState();
        CubeState.autoRotating = true;
        pivotRotation.speed = 100;

        if (move == "U")
        {
            RotateSide(cubeState.up, -90);
        }
        if (move == "U'")
        {
            RotateSide(cubeState.up, 90);
        }
        if (move == "U2")
        {
            RotateSide(cubeState.up, -180);
        }
        if (move == "D")
        {
            RotateSide(cubeState.down, -90);
        }
        if (move == "D'")
        {
            RotateSide(cubeState.down, 90);
        }
        if (move == "D2")
        {
            RotateSide(cubeState.down, -180);
        }
        if (move == "L")
        {
            RotateSide(cubeState.left, -90);
        }
        if (move == "L'")
        {
            RotateSide(cubeState.left, 90);
        }
        if (move == "L2")
        {
            RotateSide(cubeState.left, -180);
        }
        if (move == "R")
        {
            RotateSide(cubeState.right, -90);
        }
        if (move == "R'")
        {
            RotateSide(cubeState.right, 90);
        }
        if (move == "R2")
        {
            RotateSide(cubeState.right, -180);
        }
        if (move == "F")
        {
            RotateSide(cubeState.front, -90);
        }
        if (move == "F'")
        {
            RotateSide(cubeState.front, 90);
        }
        if (move == "F2")
        {
            RotateSide(cubeState.front, -180);
        }
        if (move == "B")
        {
            RotateSide(cubeState.back, -90);
        }
        if (move == "B'")
        {
            RotateSide(cubeState.back, 90);
        }
        if (move == "B2")
        {
            RotateSide(cubeState.back, -180);
        }
    }

    void RotateSide(List<GameObject> side, float angle)
    {
        
        // automatically rotate the side by the angle
        PivotRotation pr = side[4].transform.parent.GetComponent<PivotRotation>();
        pr.StartAutoRotate(side, angle);
        
    }



}
