using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class SpawnSystem : MonoBehaviour
{
    public static SpawnSystem Instance;

    private void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(this);
    }

    public void Spawn(Transform spawnItem, Vector3 newPos, float scaleFactor)
    {
        Collider collider = spawnItem.GetComponent<Collider>();
        collider.enabled = false;
        spawnItem.DOScale(Vector3.zero, 0);
        spawnItem.DOScale(Vector3.one * scaleFactor, 0.5f);
        spawnItem.DOJump(newPos, 1f, 1, 0.5f).OnComplete(delegate
        {
            collider.enabled = true;
        });
    }

    public void Up(Transform item)
    {
        item.DOScale(Vector3.zero, 0.5f);
        item.DOJump(MovementSystem.Instance.GetPlayer.position, 2f, 1, 0.5f).OnComplete(delegate
        {
            Destroy(item.gameObject);
        });
    }

    public void ShakeSpawnSource(Transform spawnShaked)
    {
        spawnShaked.DOShakePosition(0.1f, 0.1f, 10, 90f, false);
    }

    public void RemoveFromInventory(Transform item, Transform portalPoint)
    {
        SpawnedItem spawnedItem = item.GetComponent<SpawnedItem>();
        spawnedItem.StopAllCoroutines();
        spawnedItem.enabled = false;
        item.DOScale(Vector3.zero, 0);
        item.DOScale(Vector3.one * 0.3f, 0.5f);
        item.DOJump(portalPoint.position, 2f, 1, 0.5f).OnComplete(delegate
        {
            item.DOScale(0, 0.3f).OnComplete(delegate { Destroy(item.gameObject); });
        });
    }
}
