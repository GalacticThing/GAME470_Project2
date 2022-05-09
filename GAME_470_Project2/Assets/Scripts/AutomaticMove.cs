using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutomaticMove : MonoBehaviour
{
    public static List<string> moveList = new List<string>() {};
    private readonly List<string> allMoves = new List<string>()
    { "U", "D", "L", "R", "F", "B",
     "U'", "D'", "L'", "R'", "F'", "B'" // omits 180 degree turns so every rotation has only one click
    /*,"U2", "D2", "L2", "R2", "F2", "B2"*/
    };

    private CubeState cubeState;
    private ReadCube readCube;
    private PivotRotation pivotRotation;

    public AudioSource boop;
    public AudioSource click;

    public GameObject ResetButton;

    private bool boopIsPlaying;

    // Start is called before the first frame update
    void Start()
    {
        cubeState = FindObjectOfType<CubeState>();
        readCube = FindObjectOfType<ReadCube>();
        pivotRotation = FindObjectOfType<PivotRotation>();
    }

    // Update is called once per frame
    void Update()
    {
       if (moveList.Count > 0  && !CubeState.autoRotating && CubeState.started )
        {
            // Do the move at the first index;
            DoMove(moveList[0]);

            // remove the move at the first index
            moveList.Remove(moveList[0]);

            ResetButton.SetActive(false);
        }
       if(moveList.Count == 0 && CubeState.autoRotating && !boopIsPlaying)
        {
            StartCoroutine(PlayeEndBoop());
            
        }  
       
      
    }

    // Shuffle functions with difficulty settings //_________________________________________________
    public void ShuffleLv1()
    {
        List<string> moves = new List<string>();
        int shuffleLength = Random.Range(2, 5);

        for (int i = 0; i < shuffleLength; i++)
        {
            int randomMove = Random.Range(0, allMoves.Count);
            moves.Add(allMoves[randomMove]);
            
        }
        moveList = moves;
        
    }

    public void ShuffleLv2()
    {
        List<string> moves = new List<string>();
        int shuffleLength = Random.Range(6, 9);

        for (int i = 0; i < shuffleLength; i++)
        {
            int randomMove = Random.Range(0, allMoves.Count);
            moves.Add(allMoves[randomMove]);
            
        }
        moveList = moves;
    }

    public void ShuffleLv3()
    {
        List<string> moves = new List<string>();
        int shuffleLength = Random.Range(10,15);

        for (int i = 0; i < shuffleLength; i++)
        {
            int randomMove = Random.Range(0, allMoves.Count);
            moves.Add(allMoves[randomMove]);
            
        }
        moveList = moves;
    }

    public void ShuffleLv4()
    {
        List<string> moves = new List<string>();
        int shuffleLength = Random.Range(16,20);

        for (int i = 0; i < shuffleLength; i++)
        {
            
            int randomMove = Random.Range(0, allMoves.Count);
            moves.Add(allMoves[randomMove]);
            
        }
        moveList = moves;
    }

    void DoMove(string move) // contains the instructions for every autorotate command as left turn, right turn, and 180 turn
    {
        
        readCube.ReadState();
        CubeState.autoRotating = true;
        click.Play();
        pivotRotation.speed += 5f;

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

    IEnumerator PlayeEndBoop()
    {
        boop.Play();
        boopIsPlaying = true;
        yield return new WaitForSeconds(1f);
        boopIsPlaying = false;
        pivotRotation.speed = 200f;
        yield return new WaitForSeconds(0.1f);
        ResetButton.SetActive(true);
    }

}
