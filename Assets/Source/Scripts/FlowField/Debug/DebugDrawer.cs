using UnityEngine;

public class DebugDrawer : MonoBehaviour
{
    Node[,] _nodes;
    bool draw = false;
    internal bool DrawDir = false;

    public void SetNodes(Node[,] nodes)
    {
        _nodes = nodes;
        draw = true;
    }

    private void OnDrawGizmos()
    {
        if (draw)
        {
            var xSize = _nodes.GetLength(0);
            var zSize = _nodes.GetLength(1);
            for (int z = 0; z < zSize; z++)
            {
                for (int x = 0; x < xSize; x++)
                {
                    Gizmos.color = Color.white;
                    if (_nodes[x, z].Weight > 0)
                        Gizmos.color = new Color((float)_nodes[x, z].Weight / 100, 1f - (float)_nodes[x, z].Weight / 100, 1f - (float)_nodes[x, z].Weight / 100);

                    Gizmos.DrawCube(_nodes[x, z].Position, new Vector3(9f, 0.1f, 9f));
                }
            }
            if (DrawDir)
            {
                Gizmos.color = Color.black;
                for (int z = 0; z < zSize; z++)
                {
                    for (int x = 0; x < xSize; x++)
                    {
                        if (_nodes[x, z].Direction == Vector3.zero) continue;
                        Gizmos.DrawRay(_nodes[x, z].Position, _nodes[x, z].Direction*2);
                    }
                }
            }
        }
    }
}
