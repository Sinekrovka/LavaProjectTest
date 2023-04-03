using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(order = 52, fileName = "DataSpawner", menuName = "MyData/DataSpawner")]
public class DataSpawner : ScriptableObject
{
    [SerializeField] private GameObject _spawnItems;
    [SerializeField] private float _timeRest;
    [SerializeField] private int _countedObject;

    public GameObject SpawnItems => _spawnItems;

    public float TimeRest => _timeRest;

    public int CountedObject => _countedObject;
}
