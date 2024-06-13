using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject[] _chunks;
    [Range(0, 100)][SerializeField] private int _clearChunkChance;

    void Start()
    {
        Vector3 _spawnPosition = new Vector3(1.5f, transform.position.y, transform.position.z);

        for (int i = 0; i < 11; i++)
        {
            GameObject _spawnChunk;

            if (_clearChunkChance < Random.Range(0, 100))
            {
                int _randomValue = Random.Range(1, _chunks.Length);

                _spawnChunk = _chunks[_randomValue];
            }
            else
            {
                _spawnChunk = _chunks[0];
            }

            if (i == 0 || i == 1)
            {
                _spawnChunk = _chunks[0];
            }

            Instantiate(_spawnChunk, _spawnPosition, Quaternion.identity, transform);
            _spawnPosition.z += 3;
        }
    }

    public void ChunkSpawn(Vector3 _position)
    {
        Instantiate(_chunks[Random.Range(0, _chunks.Length)], _position, Quaternion.identity, transform);
    }
}
