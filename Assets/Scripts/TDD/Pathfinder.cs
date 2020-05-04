using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Pathfinder : MonoBehaviour
{
    public class Node
    {
        public Vector2Int position;
        public Node back;
        public int walkingWeight;
        public float distanceWeight;

        public Node(Vector2Int position, Node back, int walkingWeight, float distanceWeight)
        {
            this.position = position;
            this.back = back;
            this.walkingWeight = walkingWeight;
            this.distanceWeight = distanceWeight;
        }

        public float weight
        {
            get
            {
                return walkingWeight + distanceWeight;
            }
        }
    }

    public int width;
    public int height;

    [HideInInspector]
    public Tilemap tilemap;

    private void Start()
    {
        tilemap = GetComponentInChildren<Tilemap>();
    }

    public Vector3[] Find(Vector2Int startPosition, Vector2Int endPosition)
    {
        List<Node> nodesToRead = new List<Node>();
        List<Node> nodesRead = new List<Node>();
        Node endNode = null;

        nodesToRead.Add(new Node(startPosition, null, 0, Vector2Int.Distance(startPosition, endPosition)));

        while (nodesToRead.Count > 0)
        {
            Node node = GetLowestWeight(nodesToRead);
            nodesToRead.Remove(node);
            nodesRead.Add(node);

            // END
            if (node.position == endPosition)
            {
                endNode = node;
                break;
            }

            // UP
            Vector2Int upPosition = Up(node.position);
            if (upPosition != -Vector2Int.one &&
                !CheckPosition(upPosition, nodesToRead, nodesRead))
            {
                WeightTile tile = tilemap.GetTile((Vector3Int)upPosition) as WeightTile;
                if (tile.weight != 0)
                {
                    Node up = new Node(upPosition, node, node.walkingWeight + tile.weight, Vector2Int.Distance(upPosition, endPosition));
                    nodesToRead.Add(up);
                }
            }

            // DOWN
            Vector2Int downPosition = Down(node.position);
            if (downPosition != -Vector2Int.one &&
                !CheckPosition(downPosition, nodesToRead, nodesRead))
            {
                WeightTile tile = tilemap.GetTile((Vector3Int)downPosition) as WeightTile;
                if (tile.weight != 0)
                {
                    Node down = new Node(downPosition, node, node.walkingWeight + tile.weight, Vector2Int.Distance(downPosition, endPosition));
                    nodesToRead.Add(down);
                }
            }

            // LEFT
            Vector2Int leftPosition = Left(node.position);
            if (leftPosition != -Vector2Int.one &&
                !CheckPosition(leftPosition, nodesToRead, nodesRead))
            {
                WeightTile tile = tilemap.GetTile((Vector3Int)leftPosition) as WeightTile;
                if (tile.weight != 0)
                {
                    Node left = new Node(leftPosition, node, node.walkingWeight + tile.weight, Vector2Int.Distance(leftPosition, endPosition));
                    nodesToRead.Add(left);
                }
            }

            // RIGHT
            Vector2Int rightPosition = Right(node.position);
            if (rightPosition != -Vector2Int.one &&
                !CheckPosition(rightPosition, nodesToRead, nodesRead))
            {
                WeightTile tile = tilemap.GetTile((Vector3Int)rightPosition) as WeightTile;
                if (tile.weight != 0)
                {
                    Node right = new Node(rightPosition, node, node.walkingWeight + tile.weight, Vector2Int.Distance(rightPosition, endPosition));
                    nodesToRead.Add(right);
                }
            }
        }

        if (endNode != null)
        {
            Debug.Log("FOUND!");

            List<Vector3> path = new List<Vector3>();

            Node node = endNode;
            while (node.back != null)
            {
                path.Add(tilemap.GetCellCenterWorld((Vector3Int)node.position + new Vector3Int(0, 0, -1)));
                node = node.back;
            }
            path.Add(tilemap.GetCellCenterWorld((Vector3Int)startPosition + new Vector3Int(0, 0, -1)));

            return path.ToArray();
        }
        return null;
    }

    public Node GetLowestWeight(List<Node> nodes)
    {
        Node lowestWeightNode = null;
        float lowestWeight = Mathf.Infinity;

        foreach (Node n in nodes)
        {
            if (n.weight < lowestWeight)
            {
                lowestWeightNode = n;
                lowestWeight = n.weight;
            }
        }

        return lowestWeightNode;
    }

    public bool CheckPosition(Vector2Int position, List<Node> nodesToRead, List<Node> nodesRead)
    {
        foreach (Node n in nodesToRead)
        {
            if (n.position == position)
            {
                return true;
            }
        }

        foreach (Node n in nodesRead)
        {
            if (n.position == position)
            {
                return true;
            }
        }

        return false;
    }

    public Vector2Int Up(Vector2Int position)
    {
        Vector2Int upPosition = position + Vector2Int.up;
        return upPosition.y < height ? upPosition : -Vector2Int.one;
    }

    public Vector2Int Down(Vector2Int position)
    {
        Vector2Int downPosition = position + Vector2Int.down;
        return downPosition.y >= 0 ? downPosition : -Vector2Int.one;
    }

    public Vector2Int Left(Vector2Int position)
    {
        Vector2Int leftPosition = position + Vector2Int.left;
        return leftPosition.x >= 0 ? leftPosition : -Vector2Int.one;
    }

    public Vector2Int Right(Vector2Int position)
    {
        Vector2Int rightPosition = position + Vector2Int.right;
        return rightPosition.x < width ? rightPosition : -Vector2Int.one;
    }
}
