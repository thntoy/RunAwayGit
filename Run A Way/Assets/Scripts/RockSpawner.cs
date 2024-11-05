using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockSpawner : MonoBehaviour
{
    [SerializeField] private GameObject _rockPrefab;
    [SerializeField] private Transform _spawnPoint;

    public void SpawnRock()
    {
        Instantiate(_rockPrefab, _spawnPoint.position, Quaternion.identity);
    }

}
