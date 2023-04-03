using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Serialization;

public class SpawnSystem : MonoBehaviour
{
    public static SpawnSystem Instance;

    public Action spawnItemAction;
    public Action up;
    public Action remove;

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
        spawnItemAction?.Invoke();
    }

    public void Up(Transform item)
    {
        item.DOScale(Vector3.zero, 0.5f);
        item.DOJump(MovementSystem.Instance.GetPlayer.position, 2f, 1, 0.5f).OnComplete(delegate
        {
            Destroy(item.gameObject);
        });
        up?.Invoke();
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
        Vector3 scale = item.lossyScale;
        item.DOScale(Vector3.zero, 0);
        item.DOScale(scale, 0.5f);
        item.DOJump(portalPoint.position, 2f, 1, 0.5f).OnComplete(delegate
        {
            item.DOScale(0, 0.3f).OnComplete(delegate { Destroy(item.gameObject); });
        });
        remove?.Invoke();
    }
}
