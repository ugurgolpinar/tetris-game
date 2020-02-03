using UnityEngine;
public class Spawner : MonoBehaviour
{
    public GameObject[] blocks;
    void Start()
    {
        SpawnBlock();
    }

    public void SpawnBlock()
    {
        int index = Random.Range(0, blocks.Length);
        Instantiate(blocks[index], transform.position, Quaternion.identity);
    }
}
