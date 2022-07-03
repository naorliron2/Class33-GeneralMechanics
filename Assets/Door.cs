using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    bool open;

    [SerializeField] KeyType key;
    [SerializeField] Inventory playerInventory;
    [SerializeField] Vector3 openAmount;
    public void Toggle()
    {
        //In most cases this would play an animation
        int keyindex = playerInventory.ContainsKey(key);
        if (keyindex != -1)
        {
            if (open)
            {
                transform.position -= openAmount;
                open = false;
            }
            else
            {
                transform.position += openAmount;
                open = true;

            }
            playerInventory.removeKey(keyindex);
        }
    }
}
