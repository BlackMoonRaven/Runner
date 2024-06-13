using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chunk : MonoBehaviour
{
    private Spawner _spawner;
    private bool _spawnNextChunk = false;

    private GameController _gameController;

    private void Awake()
    {
        _spawner = transform.parent.GetComponent<Spawner>();
        _gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
    }

    private void Start()
    {
        GameObject _applesContainer = transform.Find("Apples").gameObject;

        if (Random.Range(0, 100) < _gameController._applesRandomChance)
        {
            _applesContainer.SetActive(true);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player") && _spawnNextChunk == false)
        {
            _spawner.ChunkSpawn(transform.position + Vector3.forward * 30);
            _spawnNextChunk = true;
        }
    }
}
