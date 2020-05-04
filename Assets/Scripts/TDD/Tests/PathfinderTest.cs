using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathfinderTest : Test
{
    private Pathfinder pathfinder;

    public override bool DoTests()
    {
        pathfinder = testController.pathfinder;

        bool upTest = UpTest();
        bool downTest = DownTest();
        bool leftTest = LeftTest();
        bool rightTest = RightTest();
        bool checkPositionTest = CheckPositionTest();

        Debug.Log("PATHFINDER - UP TEST: " + upTest.ToString());
        Debug.Log("PATHFINDER - DOWN TEST: " + downTest.ToString());
        Debug.Log("PATHFINDER - LEFT TEST: " + leftTest.ToString());
        Debug.Log("PATHFINDER - RIGHT TEST: " + rightTest.ToString());
        Debug.Log("PATHFINDER - CHECK POSITION TEST: " + checkPositionTest.ToString());

        return upTest && downTest && leftTest && rightTest && checkPositionTest;
    }

    public bool UpTest()
    {
        Vector2Int position = new Vector2Int(5, 5);
        Vector2Int upPosition = pathfinder.Up(position);

        return upPosition == new Vector2Int(5, 6) ? true : false;
    }

    public bool DownTest()
    {
        Vector2Int position = new Vector2Int(5, 5);
        Vector2Int downPosition = pathfinder.Down(position);

        return downPosition == new Vector2Int(5, 4) ? true : false;
    }

    public bool LeftTest()
    {
        Vector2Int position = new Vector2Int(5, 5);
        Vector2Int leftPosition = pathfinder.Left(position);

        return leftPosition == new Vector2Int(4, 5) ? true : false;
    }

    public bool RightTest()
    {
        Vector2Int position = new Vector2Int(5, 5);
        Vector2Int rightPosition = pathfinder.Right(position);

        return rightPosition == new Vector2Int(6, 5) ? true : false;
    }

    public bool CheckPositionTest()
    {
        List<Pathfinder.Node> nodesToRead = new List<Pathfinder.Node>();
        nodesToRead.Add(new Pathfinder.Node(new Vector2Int(4, 5), null, 0, 0));
        nodesToRead.Add(new Pathfinder.Node(new Vector2Int(6, 5), null, 0, 0));
        nodesToRead.Add(new Pathfinder.Node(new Vector2Int(5, 4), null, 0, 0));
        nodesToRead.Add(new Pathfinder.Node(new Vector2Int(5, 6), null, 0, 0));

        List<Pathfinder.Node> nodesRead = new List<Pathfinder.Node>();
        nodesRead.Add(new Pathfinder.Node(new Vector2Int(5, 5), null, 0, 0));

        Vector2Int position = new Vector2Int(3, 3);

        return pathfinder.CheckPosition(position, nodesToRead, nodesRead) ? false : true;
    }
}
