using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnedItem : MonoBehaviour
{
    [SerializeField] private TypeItem _type;

    private void Start()
    {
        StartCoroutine(Enter());
    }

    public IEnumerator Enter()
    {
        yield return new WaitForSeconds(1f);
        SpawnSystem.Instance.Up(transform);
        InventorySystem.Instance.AddInInventory(_type);
        StopAllCoroutines();
    }
    
    public enum TypeItem
    {
        Sphere,
        Cube,
        Floor,
        Crystall,
        Cake
    }
}
