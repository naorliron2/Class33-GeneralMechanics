using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    bool open;

    [SerializeField] Vector3 openAmount;
    public void Toggle()
    {
        //In most cases this would play an animation

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
    }
}
