using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public Vector2Int position;
    private Vector3[] path = null;

    private Pathfinder pathfinder;
    private LineRenderer line;

    private void Start()
    {
        pathfinder = FindObjectOfType<Pathfinder>();
        line = GetComponent<LineRenderer>();

        position = (Vector2Int)pathfinder.tilemap.WorldToCell(transform.position);
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 mousePosition = Input.mousePosition;
            mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
            Vector2Int selectedCell = (Vector2Int)pathfinder.tilemap.WorldToCell(mousePosition);

            path = pathfinder.Find(position, selectedCell);
            if (path != null)
            {
                transform.position = path[0];
                position = (Vector2Int)pathfinder.tilemap.WorldToCell(transform.position);
                line.positionCount = path.Length;
                line.SetPositions(path);
            }
        }
    }

    //private void OnDrawGizmos()
    //{
    //    if (path != null)
    //    {
    //        Gizmos.color = Color.red;
    //        for (int i = 0; i < path.Length - 1; i++)
    //        {
    //            Gizmos.DrawLine(path[i], path[i + 1]);
    //        }
    //    }
    //}
}
