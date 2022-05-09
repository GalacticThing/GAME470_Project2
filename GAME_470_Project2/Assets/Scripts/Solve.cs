using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Kociemba;

public class Solve : MonoBehaviour
{
    public ReadCube readCube;
    public CubeState cubeState;
    public AudioSource confirm; // attached to the ground object

    private bool doOnce = true;

    // Start is called before the first frame update
    void Start()
    {
        readCube = FindObjectOfType<ReadCube>();
        cubeState = FindObjectOfType<CubeState>();
    }

    // Update is called once per frame
    void Update()
    {
        if (CubeState.started && doOnce)
        {
            doOnce = false;
            Solver();
        }
    }

    public void Solver()
    {
        confirm.Play();
        readCube.ReadState();

        //get the state of the cube as a string
        string moveString = cubeState.GetStateString();
        print(moveString);

        // solve cube
        string info = "";

        // first time
        //string solution = SearchRunTime.solution(moveString, out info, buildTables: true);

        //every other time
        string solution = Search.solution(moveString, out info);

        // convert the solved moves from a string to a list
        List<string> solutionList = StringToList(solution);

        //Automate the list
        AutomaticMove.moveList = solutionList;

        print(info);
    }

    List<string> StringToList(string solution)
    {
        List<string> solutionList = new List<string>(solution.Split(new string[] { " " }, System.StringSplitOptions.RemoveEmptyEntries));
        return solutionList;
    }
}
