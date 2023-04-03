using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnSource : MonoBehaviour, IInteractable
{
    [SerializeField] private DataSpawner _dataFile;
    [SerializeField] private float _minDistance;
    [SerializeField] private float _maxDistance;

    private int _currentSpawn;
    private void Start()
    {
        _currentSpawn = _dataFile.CountedObject;
    }

    public void Enter()
    {
        if (_currentSpawn > 0)
        {
            _currentSpawn--;
            Spawn();
        }
        else
        {
            StartCoroutine(WaitRest());
        }
    }

    private IEnumerator WaitRest()
    {
        yield return new WaitForSeconds(_dataFile.TimeRest);
        _currentSpawn = _dataFile.CountedObject;
        /*Где то здесь шкала обратного отсчета*/
    }

    private void Spawn()
    {
        bool negative = Random.Range(0, 1).Equals(0);
        float x = 0f;
        float y = 0f;
        float z = 1f;
        
        if (negative)
        {
            z = -1f;
        }
        
        x = Random.Range(_minDistance, _maxDistance) * z;
        y = _maxDistance - Mathf.Abs(x);
        negative = Random.Range(0, 1).Equals(0);
        
        if (negative)
        {
            y = y * -1f;
        }
        
        Vector3 movsPosition = new Vector3(transform.position.x + x, _dataFile.SpawnItems.transform.localScale.y/2, transform.position.z +y);

        GameObject spawnPoint = Instantiate(_dataFile.SpawnItems, transform.position, Quaternion.identity);
        SpawnSystem.Instance.Spawn(spawnPoint.transform, movsPosition, 0.3f);
        SpawnSystem.Instance.ShakeSpawnSource(transform);
    }
}
