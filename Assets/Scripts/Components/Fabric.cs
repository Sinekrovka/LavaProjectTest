using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fabric : MonoBehaviour, IInteractable
{
    [SerializeField] private DataFabric[] _dataFiles;
    private int _minCounts;
    private Transform _spawnPoint;
    private List<Transform> _createObjectsEndPoints;
    private int[] _minCountsData;
   
    private void Awake()
    {
        _minCounts = 0;
        _minCountsData = new int[_dataFiles.Length];

        for (int i = 0; i < _minCountsData.Length; ++i)
        {
            _minCountsData[i] = 0;
        }
        
        _spawnPoint = transform.Find("SpawnPoint");
        _createObjectsEndPoints = new List<Transform>();

        Transform pointContainer = transform.Find("PointContainer");

        if (pointContainer != null)
        {
            for (int i = 0; i < pointContainer.childCount; ++i)
            {
                _createObjectsEndPoints.Add(pointContainer.GetChild(i));
            }
        }
    }

    public void Enter()
    {
        bool spawn = true;
        for (int i = 0; i < _dataFiles.Length; ++i)
        {
            if (InventorySystem.Instance.RemoveToInventory(_dataFiles[i].GivedItem, _spawnPoint))
            {
                _minCountsData[i]++;
            }

            spawn = spawn & _minCountsData[i] >= _dataFiles[i].CountForCreated;
        }
        
        if (spawn)
        {
            for (int i = 0; i < _dataFiles.Length; ++i)
            {
                _minCountsData[i] -= _dataFiles[i].CountForCreated;
                if (_dataFiles[i].CreatedItem != null)
                {
                    GameObject createdItem = Instantiate(_dataFiles[i].CreatedItem, _spawnPoint.position, Quaternion.identity, null);
                    SpawnSystem.Instance.Spawn(createdItem.transform, _createObjectsEndPoints[Random.Range(0, _createObjectsEndPoints.Count)].position, createdItem.transform.localScale.x);
                }
            }
        }
    }

    public float GetDataTime()
    {
        return 0f;
    }

}
