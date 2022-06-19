using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayLight : MonoBehaviour
{
    [SerializeField] MeshRenderer renderer;
    [SerializeField] Material onMat;
    [SerializeField] Material offMat;
    [SerializeField] bool phase;

    public void TurnOn()
    {
        if (phase) return;

        renderer.material = onMat;
        phase = true;
    }
    public void TurnOff()
    {
        if (!phase) return;

        renderer.material = offMat;
        phase = false;
    }

}
