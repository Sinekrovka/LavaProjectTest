using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(order = 53, fileName = "DataInventory", menuName = "MyData/DataInventory")]
public class InventoryData : ScriptableObject
{
    [SerializeField] private List<DataInventory> _dataInventories;

    [Serializable]
    public struct DataInventory
    {
        public SpawnedItem.TypeItem type;
        public GameObject prefab;
        public Sprite icon;
    }

    public GameObject GetDataOfType(SpawnedItem.TypeItem type)
    {
        foreach (var item in _dataInventories)
        {
            if (item.type.Equals(type))
            {
                return item.prefab;
            }
        }

        return null;
    }

    public Sprite GetIconByType(SpawnedItem.TypeItem type)
    {
        foreach (var item in _dataInventories)
        {
            if (item.type.Equals(type))
            {
                return item.icon;
            }
        }

        return null;
    }
}
