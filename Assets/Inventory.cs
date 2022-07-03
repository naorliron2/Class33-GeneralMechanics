using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Inventory : MonoBehaviour
{
    [SerializeField] int inventorySize = 6;
    [SerializeField] Key[] keys = new Key[6];
    [SerializeField] List<Image> InventorySlots = new List<Image>();
    int index = 0;
    public void AddKey(Key key)
    {
        if (index < inventorySize)
        {
            keys[index] = key;
            if (index < InventorySlots.Count)
            {
                InventorySlots[index].sprite = keys[index].keyImage;
            }

            index++;
        }
    }

    public void removeKey(int _index)
    {
        if (_index < inventorySize)
        {
            keys[index] = null;
            if (_index < InventorySlots.Count)
            {
                InventorySlots[_index].sprite = keys[_index].keyImage;
            }

            index++;
        }
    }

    public int ContainsKey(KeyType keyType)
    {
        for (int i = 0; i < keys.Length; i++)
        {
            if(keys[i].keyType == keyType)
            {
                return i;
            }
        }

        return -1;
    }
}
