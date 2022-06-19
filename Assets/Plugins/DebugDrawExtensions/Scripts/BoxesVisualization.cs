using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static partial class DebugDrawExtension
{
    static Vector3[] startingBox = new Vector3[8];
    static Vector3[] endingBox = new Vector3[8];

    public static void DrawOverlapBox(Vector3 center, Vector3 halfExtents)
    {
        DrawOverlapBox(center, halfExtents, Quaternion.identity, Color.red);
    }

    public static void DrawOverlapBox(Vector3 center, Vector3 halfExtents, Color color)
    {
        DrawOverlapBox(center, halfExtents, Quaternion.identity, color);
    }

    public static void DrawOverlapBox(Vector3 center, Vector3 halfExtents, Quaternion orientation)
    {
        DrawOverlapBox(center, halfExtents, orientation, Color.red);
    }

    public static void DrawOverlapBox(Vector3 center, Vector3 halfExtents, Quaternion orientation, Color color)
    {
        BuildBox(ref startingBox , center, halfExtents, orientation);
        DrawBox(startingBox, color);
    }

    private static void BuildBox(ref Vector3[] boxArray, Vector3 center, Vector3 halfExtents, Quaternion orientation)
    {
        Vector3 forwardDir = orientation * Vector3.forward;
        Vector3 upDir = orientation * Vector3.up;
        Vector3 rightdir = orientation * Vector3.right;


        Vector3 right = (rightdir * halfExtents.x);
        Vector3 top = (upDir * halfExtents.y);
        Vector3 forward = (forwardDir * halfExtents.z);

        Vector3 topRightFront = center + ((top + right + forward));
        Vector3 topLeftFront = center + (top - right + forward);
        Vector3 topRightBack = center + (top + right - forward);
        Vector3 topLeftBack = center + (top - right - forward);
        Vector3 botRightFront = center + (-top + right + forward);
        Vector3 botLeftFront = center + (-top - right + forward);
        Vector3 botRightBack = center + (-top + right - forward);
        Vector3 botLeftBack = center + (-top - right - forward);

        boxArray[0] = topRightFront;
        boxArray[1] = topLeftFront;
        boxArray[2] = topRightBack;
        boxArray[3] = topLeftBack;
        boxArray[4] = botRightFront;
        boxArray[5] = botLeftFront;
        boxArray[6] = botRightBack;
        boxArray[7] = botLeftBack;
    }

    private static void DrawBox(Vector3[] boxVectors, Color color)
    {
        Debug.DrawLine(boxVectors[0], boxVectors[2], color);
        Debug.DrawLine(boxVectors[0], boxVectors[1], color);
        Debug.DrawLine(boxVectors[2], boxVectors[3], color);
        Debug.DrawLine(boxVectors[1], boxVectors[3], color);

        Debug.DrawLine(boxVectors[4], boxVectors[6], color);
        Debug.DrawLine(boxVectors[4], boxVectors[5], color);
        Debug.DrawLine(boxVectors[6], boxVectors[7], color);
        Debug.DrawLine(boxVectors[5], boxVectors[7], color);

        Debug.DrawLine(boxVectors[0], boxVectors[4], color);
        Debug.DrawLine(boxVectors[2], boxVectors[6], color);
        Debug.DrawLine(boxVectors[1], boxVectors[5], color);
        Debug.DrawLine(boxVectors[3], boxVectors[7], color);
    }



    /// <summary>
    /// when not giving a max distance the box draw uses the value of 999 the unity boxcast uses infinity
    /// </summary>
    /// <param name="center"></param>
    /// <param name="halfExtents"></param>
    /// <param name="direction"></param>
    /// <param name="orientation"></param>
    /// <param name="color"></param>
    public static void DrawCastBox(Vector3 center, Vector3 halfExtents, Vector3 direction)
    {
        DrawCastBox(center, halfExtents, direction, Quaternion.identity, Color.red);
    }
    /// <summary>
    /// when not giving a max distance the box draw uses the value of 999 the unity boxcast uses infinity
    /// </summary>
    /// <param name="center"></param>
    /// <param name="halfExtents"></param>
    /// <param name="direction"></param>
    /// <param name="orientation"></param>
    /// <param name="color"></param>
    public static void DrawCastBox(Vector3 center, Vector3 halfExtents, Vector3 direction, Color color)
    {
        DrawCastBox(center, halfExtents, direction, Quaternion.identity, color);
    }
    public static void DrawCastBox(Vector3 center, Vector3 halfExtents, Vector3 direction, float maxDistance, Color color)
    {
        DrawCastBox(center, halfExtents, direction, Quaternion.identity, maxDistance, color);
    }
    /// <summary>
    /// when not giving a max distance the box draw uses the value of 999 the unity boxcast uses infinity
    /// </summary>
    /// <param name="center"></param>
    /// <param name="halfExtents"></param>
    /// <param name="direction"></param>
    /// <param name="orientation"></param>
    /// <param name="color"></param>
    public static void DrawCastBox(Vector3 center, Vector3 halfExtents, Vector3 direction, Quaternion orientation)
    {
        DrawCastBox(center, halfExtents, direction, orientation, 999, Color.red);
    }
    /// <summary>
    /// when not giving a max distance the box draw uses the value of 999 the unity boxcast uses infinity
    /// </summary>
    /// <param name="center"></param>
    /// <param name="halfExtents"></param>
    /// <param name="direction"></param>
    /// <param name="orientation"></param>
    /// <param name="color"></param>
    public static void DrawCastBox(Vector3 center, Vector3 halfExtents, Vector3 direction, Quaternion orientation, Color color)
    {
        DrawCastBox(center, halfExtents, direction, orientation, 999, color);
    }

    public static void DrawCastBox(Vector3 center, Vector3 halfExtents, Vector3 direction, Quaternion orientation, float maxDistance)
    {
        DrawCastBox(center, halfExtents, direction, orientation, maxDistance, Color.red);
    }

    public static void DrawCastBox(Vector3 center, Vector3 halfExtents, Vector3 direction, Quaternion orientation, float maxDistance, Color color)
    {
        direction.Normalize();
       BuildBox(ref startingBox,center, halfExtents, orientation);
        Vector3 castPosition = (direction * maxDistance);
       BuildBox(ref endingBox,center + castPosition, halfExtents, orientation);

        DrawBox(startingBox, color);
        color /= 1.5f;
        DrawBox(endingBox, color);


        for (int i = 0; i < 8; i++)
        {
            Debug.DrawLine(startingBox[i], endingBox[i], color);
        }
    }
}
