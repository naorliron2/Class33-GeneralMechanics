using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Key : MonoBehaviour
{
    public KeyType keyType;
    public Sprite keyImage;
    [SerializeField] GameObject graphics;
    [SerializeField] Rigidbody rb;
    public void OnPickup()
    {
        rb.isKinematic = true;
        graphics.SetActive(false);
    }
}

public enum KeyType {YellowKey,GreenKey,RedKey };
