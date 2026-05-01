using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    public int width;
    public int height;
    public float cellSize = 1f;

    public CubeActive[,] grid;

    public CubeActive cubePrefabs;

    private void Start()
    {
        grid = new CubeActive[width, height];

        // Spawn beberapa cube contoh
        SpawnCube(new Vector2Int(0, 0));
        SpawnCube(new Vector2Int(1, 0));
        SpawnCube(new Vector2Int(2, 2));
    }

    public CubeActive SpawnCube(Vector2Int gridPos)
    {
        // Cek valid
        if (!IsInsideGrid(gridPos))
        {
            Debug.LogWarning("Posisi di luar grid!");
            return null;
        }

        // Cek apakah sudah ada cube
        if (grid[gridPos.x, gridPos.y] != null)
        {
            Debug.LogWarning("Cell sudah terisi!");
            return null;
        }

        // Ambil posisi world dari grid
        Vector3 worldPos = GridToWorld(gridPos);

        // Spawn object
        CubeActive cube = Instantiate(cubePrefabs, worldPos, Quaternion.identity, transform);

        // Set data ke cube
        cube.gridPosition = gridPos;

        // Masukkan ke array grid
        grid[gridPos.x, gridPos.y] = cube;

        return cube;
    }

    public List<CubeActive> GetNeighbors(CubeActive cube)
    {
        List<CubeActive> neighbors = new List<CubeActive>();

        Vector2Int[] directions = new Vector2Int[]
        {
            Vector2Int.up,
            Vector2Int.down,
            Vector2Int.left,
            Vector2Int.right
        };

        foreach (var dir in directions)
        {
            Vector2Int newPos = cube.gridPosition + dir;

            if (IsInsideGrid(newPos))
            {
                CubeActive neighbor = grid[newPos.x, newPos.y];
                if (neighbor != null)
                    neighbors.Add(neighbor);
            }
        }

        return neighbors;
    }

    bool IsInsideGrid(Vector2Int pos)
    {
        return pos.x >= 0 && pos.x < width &&
               pos.y >= 0 && pos.y < height;
    }

    public Vector3 GridToWorld(Vector2Int gridPos)
    {
        Vector3 offset = new Vector3(width, 0, height) * 0.5f;

        Vector3 localPos = new Vector3(
            gridPos.x * cellSize,
            0,
            gridPos.y * cellSize
        ) - offset;

        return transform.TransformPoint(localPos);
    }

    public Vector2Int WorldToGrid(Vector3 worldPos)
    {
        Vector3 offset = new Vector3(width, 0, height) * 0.5f;

        Vector3 localPos = transform.InverseTransformPoint(worldPos);

        int x = Mathf.RoundToInt((localPos.x + offset.x) / cellSize);
        int y = Mathf.RoundToInt((localPos.z + offset.z) / cellSize);

        return new Vector2Int(x, y);
    }

    void OnDrawGizmos()
    {
        if (width <= 0 || height <= 0) return;

        float size = cellSize;
        Vector3 offset = new Vector3(width, 0, height) * 0.5f;

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                Vector3 localPos = new Vector3(x * size, 0, y * size) - offset;
                Vector3 worldPos = transform.TransformPoint(localPos);

                if (grid != null && x < width && y < height && grid[x, y] != null)
                    Gizmos.color = Color.yellow;
                else
                    Gizmos.color = Color.red;

                Gizmos.DrawWireCube(worldPos, new Vector3(size, 0.1f, size));
            }
        }
    }
}
