using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Linq;
using UnityEngine;

public class InventorySystem : MonoBehaviour
{
    [SerializeField] private InventoryData _data;
    private Dictionary<SpawnedItem.TypeItem, int> _inventoryContainer;
    public static InventorySystem Instance;
    
    public Action<SpawnedItem.TypeItem, Transform> giveToAction;
    public Action<SpawnedItem.TypeItem, int> updateData;
    public Action<SpawnedItem.TypeItem, Sprite> addData;

    private void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(this);
        _inventoryContainer = new Dictionary<SpawnedItem.TypeItem, int>();
    }

    private void Start()
    {
        LoadResources();
    }

    public void AddInInventory(SpawnedItem.TypeItem type)
    {
        if (_inventoryContainer.ContainsKey(type))
        {
            _inventoryContainer[type] += 1;
            updateData?.Invoke(type, _inventoryContainer[type]);
        }
        else
        {
            _inventoryContainer.Add(type, 1);
            addData?.Invoke(type, _data.GetIconByType(type));
        }
    }

    public bool RemoveToInventory(SpawnedItem.TypeItem type, Transform portalPoint)
    {
        if (_inventoryContainer.ContainsKey(type) && _inventoryContainer[type]>0)
        {
            GameObject item = _data.GetDataOfType(type);
            if (item != null)
            {
                _inventoryContainer[type]--;
                updateData?.Invoke(type, _inventoryContainer[type]);
                if (_inventoryContainer[type].Equals(0))
                {
                    _inventoryContainer.Remove(type);
                }
                GameObject createdItem =
                    Instantiate(item, MovementSystem.Instance.GetPlayer.transform.position, Quaternion.identity);
                SpawnSystem.Instance.RemoveFromInventory(createdItem.transform, portalPoint);
                giveToAction?.Invoke(type, portalPoint);
                return true;
            }
        }

        return false;
    }

    private void SaveResources()
    {
        XElement root = new XElement("root");
        foreach (var item in _inventoryContainer)
        {
            XElement saveItem = new XElement("item");
            XAttribute type = new XAttribute("Type", item.Key.GetHashCode());
            XAttribute count = new XAttribute("Count", item.Value);
            saveItem.Add(type);
            saveItem.Add(count);
            root.Add(saveItem);
        }
        
        File.Delete(Application.dataPath+"/Inventory.xml");
        XDocument doc = new XDocument();
        doc.Add(root);
        doc.Save(Application.dataPath+"/Inventory.xml");
        
    }

    private void LoadResources()
    {
        if (File.Exists(Application.dataPath + "/Inventory.xml"))
        {
            var doc = XDocument.Load(Application.dataPath + "/Inventory.xml");
            foreach (var elem in doc.Element("root").Elements())
            {
                int count = Int32.Parse(elem.Attribute("Count").Value);
                SpawnedItem.TypeItem type = (SpawnedItem.TypeItem)Enum.
                    ToObject(typeof(SpawnedItem.TypeItem), Int32.Parse(elem.Attribute("Type").Value));
                _inventoryContainer.Add(type, count);
                addData?.Invoke(type, _data.GetIconByType(type));
            }
        }
    }

    private void OnApplicationQuit()
    {
        SaveResources();
    }
}
