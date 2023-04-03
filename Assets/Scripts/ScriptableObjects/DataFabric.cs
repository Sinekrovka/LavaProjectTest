using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(order = 51, fileName = "DataFabric", menuName = "MyData/DataFabric")]
public class DataFabric : ScriptableObject
{
    [SerializeField] private SpawnedItem.TypeItem _givedItem;
    [SerializeField] private GameObject _createdItem;
    [SerializeField] private float _timeCreated;
    [SerializeField] private int _countForCreated;

    public SpawnedItem.TypeItem GivedItem => _givedItem;

    public GameObject CreatedItem => _createdItem;

    public float TimeCreated => _timeCreated;

    public int CountForCreated => _countForCreated;
}
