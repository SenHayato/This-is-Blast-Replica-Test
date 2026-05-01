using UnityEngine;

public class CubeManager : MonoBehaviour
{
    [Header("Cube List")]
    [SerializeField] GameObject[] cubeBlocks;

    [Header("Cube Component")]
    [SerializeField] float cubeMoveSpeed;

    private void Awake()
    {
        cubeBlocks = GameObject.FindGameObjectsWithTag("Cube");
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    void CubeMoving()
    {
        foreach (var cubes in cubeBlocks)
        {
            cubes.transform.localPosition += new Vector3(0f, 0f, -cubeMoveSpeed * Time.deltaTime);
        }
    }

    // Update is called once per frame
    void Update()
    {
        CubeMoving();
    }
}
