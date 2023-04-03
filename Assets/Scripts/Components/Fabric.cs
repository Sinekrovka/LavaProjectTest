using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fabric : MonoBehaviour, IInteractable
{
    [SerializeField] private DataFabric _dataFile;
    
    private int _minCounts;
    private int _countCurrent;
    private Transform _spawnPoint;
    private List<Transform> _createObjectsEndPoints;
   
    private void Awake()
    {
        _minCounts = 0;
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
        if (InventorySystem.Instance.RemoveToInventory(_dataFile.GivedItem, _spawnPoint))
        {
            _minCounts++;
            if (_minCounts.Equals(_dataFile.CountForCreated))
            {
                _minCounts = 0;
                if (_dataFile.CreatedItem != null)
                {
                    GameObject createdItem = Instantiate(_dataFile.CreatedItem, _spawnPoint.position, Quaternion.identity, null);
                    SpawnSystem.Instance.Spawn(createdItem.transform, _createObjectsEndPoints[Random.Range(0, _createObjectsEndPoints.Count)].position, createdItem.transform.localScale.x);
                }
            }
        }
    }

    public SpawnedItem.TypeItem GetFabricType => _dataFile.GivedItem;
}
