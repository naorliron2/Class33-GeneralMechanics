using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public static partial class DebugDrawExtension
{
    private static int numberOfPointsInCircle = 20;
    //PreGenerated points
    private static Vector3[] pointsOnYCircle = FindPointsOnCircle(true);
    private static Vector3[] pointsOnZCircle = FindPointsOnCircle(false);
    private static Vector3[] halfCircleYPoints = GetHalfCirclePoints(true);
    private static Vector3[] halfCircleZPoints = GetHalfCirclePoints(false);

    //Empty arrays for the drawing to fill in
    private static Vector3[] pointsOnCircle = new Vector3[numberOfPointsInCircle];
    private static Vector3[] halfCirclePoints1 = new Vector3[halfCircleYPoints.Length];
    private static Vector3[] halfCirclePoints2 = new Vector3[halfCircleYPoints.Length];

    #region Circles
    private static Vector3[] FindPointsOnCircle(bool yCircle)
    {
        float hypotenus = 1;
        float currentDegree = 0;
        float degreeDelta = 360 / numberOfPointsInCircle;
        Vector3[] pointsOnCircle = new Vector3[numberOfPointsInCircle];

        for (int i = 0; i < numberOfPointsInCircle; i++)
        {
            float x = Mathf.Cos(Mathf.Deg2Rad * currentDegree) * hypotenus;
            float y = Mathf.Sin(Mathf.Deg2Rad * currentDegree) * hypotenus;
            currentDegree += degreeDelta;
            Vector3 point;
            if (yCircle)
            {
                point = new Vector3(x, y, 0);
            }
            else
            {
                point = new Vector3(x, 0, y);
            }
            pointsOnCircle[i] = point;
        }
        return pointsOnCircle;
    }

    private static Vector3[] GetHalfCirclePoints(bool yCircle)
    {
        float hypotenus = 1;
        float currentDegree = 0;
        float degreeDelta = 180 / (numberOfPointsInCircle / 2);
        Vector3[] halfPoints = new Vector3[(numberOfPointsInCircle / 2) + 1];

        for (int i = 0; i < halfPoints.Length; i++)
        {
            float x = Mathf.Cos(Mathf.Deg2Rad * currentDegree) * hypotenus;
            float y = Mathf.Sin(Mathf.Deg2Rad * currentDegree) * hypotenus;
            currentDegree += degreeDelta;
            Vector3 point;
            if (yCircle)
            {
                point = new Vector3(x, y, 0);
            }
            else
            {
                point = new Vector3(x, 0, y);
            }
            halfPoints[i] = point;
        }

        return halfPoints;
    }

    private static void Draw2DYCircle(Vector3 center, Quaternion rotation, float radius, Color color)
    {
        for (int i = 0; i < numberOfPointsInCircle; i++)
        {
            pointsOnCircle[i] = rotation * (pointsOnYCircle[i] * radius);
        }
        DrawCircle(center, pointsOnCircle, true, color);
    }

    private static void Draw2DZCircle(Vector3 center, Quaternion rotation, float radius, Color color)
    {
        for (int i = 0; i < numberOfPointsInCircle; i++)
        {
            pointsOnCircle[i] = rotation * (pointsOnZCircle[i] * radius);
        }
        DrawCircle(center, pointsOnCircle, true, color);
    }

    private static void Draw2DYHalfCircle(Vector3 center, Quaternion rotation, float radius, ref Vector3[] halfCirclePoints)
    {
        for (int i = 0; i < halfCircleYPoints.Length; i++)
        {
            halfCirclePoints[i] = rotation * (halfCircleYPoints[i] * radius);

        }
        DrawCircle(center, halfCirclePoints, false);
    }

    private static void DrawZHalfCircle(Vector3 center, Quaternion rotation, float radius, ref Vector3[] halfCirclePoints, Color col)
    {
        for (int i = 0; i < halfCircleZPoints.Length; i++)
        {
            halfCirclePoints[i] = rotation * (halfCircleZPoints[i] * radius);

        }
        DrawCircle(center, halfCirclePoints, false, col);
    }

    private static void DrawCircle(Vector3 center, Vector3[] points, bool closedCircle)
    {
        DrawCircle(center, points, closedCircle, Color.white);
    }

    private static void DrawCircle(Vector3 center, Vector3[] points, bool closeCircle, Color col)
    {
        for (int i = 0; i < points.Length; i++)
        {
            if (i + 1 == points.Length)
            {
                if (closeCircle)
                {
                    Debug.DrawLine(center + points[i], center + points[0], col);
                }
                continue;

            }
            Debug.DrawLine(center + points[i], center + points[i + 1], col);
        }
    }

    #endregion

    #region Sphere
    /// <summary>
    /// If maxdistance isn't used the default value would be 999
    /// if no upRelativeToDirection is given the lines connecting the sphere might be weirdly rotated at certain rotations
    /// </summary>
    /// <param name="center"></param>
    /// <param name="radius"></param>
    /// <param name="direction"></param>
    public static void DrawCastSphere(Vector3 center, float radius, Vector3 direction)
    {
        DrawCastSphere(center, radius, direction, 999f);
    }

    /// <summary>
    /// if no upRelativeToDirection is given the lines connecting the sphere might be weirdly rotated at certain rotations
    /// </summary>
    /// <param name="center"></param>
    /// <param name="radius"></param>
    /// <param name="direction"></param>
    public static void DrawCastSphere(Vector3 center, float radius, Vector3 direction, float maxDistance)
    {
        DrawCastSphere(center, radius, direction, Vector3.up, maxDistance);
    }

    /// <summary>
    /// if no upRelativeToDirection is given the lines connecting the sphere might be weirdly rotated at certain rotations
    /// </summary>
    /// <param name="center"></param>
    /// <param name="radius"></param>
    /// <param name="direction"></param>
    public static void DrawCastSphere(Vector3 center, float radius, Vector3 direction, float maxDistance, Color color)
    {
        DrawCastSphere(center, radius, direction, Vector3.up, maxDistance, color);
    }

    public static void DrawCastSphere(Vector3 center, float radius, Vector3 direction,Vector3 upRelativeToDirection)
    {
        DrawCastSphere(center, radius, direction, upRelativeToDirection, 999f);
    }

    public static void DrawCastSphere(Vector3 center, float radius, Vector3 direction,Vector3 upRelativeToDirection, float maxDistance)
    {
        DrawCastSphere(center, radius, direction, upRelativeToDirection, maxDistance, Color.green);
    }

    public static void DrawCastSphere(Vector3 center, float radius, Vector3 direction,Vector3 upRelativeToDirection, float maxDistance, Color color)
    {
        Quaternion rotation = Quaternion.LookRotation(direction, upRelativeToDirection);
        direction = direction.normalized;
        Vector3 endCenter = center + direction * maxDistance;
        DrawSphere(center, radius,rotation, color);
        DrawSphere(endCenter, radius,rotation, color);

        upRelativeToDirection = upRelativeToDirection.normalized;
        Vector3 right = Vector3.Cross(direction, upRelativeToDirection);
        right = right.normalized;
        right *= radius;
        upRelativeToDirection *= radius;
        Debug.DrawLine(center + right, endCenter + right, color);
        Debug.DrawLine(center - right, endCenter - right, color);
        Debug.DrawLine(center + upRelativeToDirection, endCenter + upRelativeToDirection, color);
        Debug.DrawLine(center - upRelativeToDirection, endCenter - upRelativeToDirection, color);

    }

    public static void DrawOverlapSphere(Vector3 center, float radius)
    {
        DrawOverlapSphere(center, radius, Color.green);
    }

    public static void DrawOverlapSphere(Vector3 center, float radius, Color color)
    {
        DrawSphere(center, radius, Quaternion.identity, color);
    }

    private static void DrawSphere(Vector3 center, float radius,Quaternion rotation, Color color)
    {
        Draw2DZCircle(center, rotation, radius, color);
        Draw2DZCircle(center, rotation * Quaternion.Euler(0, 0, 90), radius, color);
        Draw2DYCircle(center, rotation, radius, color);
    }
    #endregion

    #region Capsule
    public static void DrawOverlapCapsule(Vector3 point1, Vector3 point2, float radius)
    {
        DrawOverlapCapsule(point1, point2, radius, Color.green);
    }

    public static void DrawOverlapCapsule(Vector3 point1, Vector3 point2, float radius, Color color)
    {
        CapsuleHeights capsHeight = new CapsuleHeights(point1, point2, radius);
        CapsuleRotationAndDirections capsDirRot = new CapsuleRotationAndDirections(point1, point2);
        CapsuleCenters center = new CapsuleCenters(point1, capsHeight, capsDirRot);

        DrawCapsule(capsHeight, capsDirRot, center, color);
    }

    private static void DrawCapsule(CapsuleHeights capsHeight, CapsuleRotationAndDirections capsRotDir, CapsuleCenters centers, Color color)
    {
        Draw2DYCircle(centers.top, capsRotDir.rotation, capsHeight.radius, color);
        Draw2DYCircle(centers.bot, capsRotDir.rotation, capsHeight.radius, color);

        //These two lines draw the two half circle that compose the top cap of the capsule
        //it also saves the point data in their respective vector arrays (ex : halfCirclePoints1)
        DrawZHalfCircle(centers.top, capsRotDir.reverseRot, capsHeight.radius, ref halfCirclePoints1, color);
        DrawZHalfCircle(centers.top, capsRotDir.reverseRot * Quaternion.Euler(new Vector3(0, 0, 90)), capsHeight.radius, ref halfCirclePoints2, color);


        if (capsHeight.midHeight > 0)
        {
            //Here we draw the lines that strech for the mid part of the capsule.
            //We use the information from the halfCircle arrays (the first and last point of the half circle)
            //to place the starting position of our draw line, and simply add the midheight capsule value multiplied by the rotation to get the end position

            Vector3 firstStartPoint = centers.top + halfCirclePoints1[0];
            Vector3 secondStartPoint = centers.top + halfCirclePoints1[halfCirclePoints1.Length - 1];
            Vector3 thirdStartPoint = centers.top + halfCirclePoints2[0];
            Vector3 fourthStartPoint = centers.top + halfCirclePoints2[halfCirclePoints2.Length - 1];
            Vector3 addedDistance = capsRotDir.dirStartToEnd * capsHeight.midHeight;

            Debug.DrawLine(firstStartPoint, firstStartPoint + addedDistance, color);
            Debug.DrawLine(secondStartPoint, secondStartPoint + addedDistance, color);
            Debug.DrawLine(thirdStartPoint, thirdStartPoint + addedDistance, color);
            Debug.DrawLine(fourthStartPoint, fourthStartPoint + addedDistance, color);
        }
        //We draw the bottom cap of the capsule
        DrawZHalfCircle(centers.bot, capsRotDir.rotation, capsHeight.radius, ref halfCirclePoints1, color);
        DrawZHalfCircle(centers.bot, capsRotDir.rotation * Quaternion.Euler(new Vector3(0, 0, 90)), capsHeight.radius, ref halfCirclePoints2, color);
    }


    public static void DrawCastCapsule(Vector3 point1, Vector3 point2, float radius, Vector3 direction)
    {
        DrawCastCapsule(point1, point2, radius, direction, 999f);
    }

    public static void DrawCastCapsule(Vector3 point1, Vector3 point2, float radius, Vector3 direction, float maxDistance)
    {
        DrawCastCapsule(point1, point2, radius, direction, maxDistance, Color.green);
    }

    public static void DrawCastCapsule(Vector3 point1, Vector3 point2, float radius, Vector3 direction, float maxDistance, Color color)
    {

        CapsuleHeights capsHeight = new CapsuleHeights(point1, point2, radius);
        CapsuleRotationAndDirections capsDirRot = new CapsuleRotationAndDirections(point1, point2);
        direction = direction.normalized;
        Vector3 dirFromStartToCastEnd = direction * maxDistance;

        CapsuleCenters startingCenter = new CapsuleCenters(point1 + capsDirRot.dirStartToEnd * capsHeight.capsuleCapHeight,
            point1 + capsDirRot.dirStartToEnd * (capsHeight.capsuleCapHeight + capsHeight.midHeight));
        CapsuleCenters destinationCenter = new CapsuleCenters(startingCenter.top + dirFromStartToCastEnd, startingCenter.bot + dirFromStartToCastEnd);

        //draw capsule at starting position
        DrawCapsule(capsHeight, capsDirRot, startingCenter, color);
        //draw capsule at ending position
        DrawCapsule(capsHeight, capsDirRot, destinationCenter, color);

        //draw 2 lines between the top and bot points of the 2 capsules
        Debug.DrawLine(point1, point1 + dirFromStartToCastEnd, color);
        Debug.DrawLine(point2, point2 + dirFromStartToCastEnd, color);

        Vector3 rightDirOffset = (Vector3.Cross(direction, capsDirRot.dirStartToEnd)) * radius;
        //draw 4 lines two from each circle sides(the circle sitting on the border between the cap and the mid section of the capsule)
        Debug.DrawLine(startingCenter.top + rightDirOffset, destinationCenter.top + rightDirOffset, color);
        Debug.DrawLine(startingCenter.top - rightDirOffset, destinationCenter.top - rightDirOffset, color);
        Debug.DrawLine(startingCenter.bot + rightDirOffset, destinationCenter.bot + rightDirOffset, color);
        Debug.DrawLine(startingCenter.bot - rightDirOffset, destinationCenter.bot - rightDirOffset, color);
    }

    //A capsule is splitted into 3 parts top mid and bot
    //  _
    // / \ top 
    // | | mid - The mid part height is calculated by the height of the capsule divivded by two minus the radius and then doubled ((height/2)-radius)*2 
    // \_/ bot - The bot and top parts are even
    //  and they are half of the remaining height of the total height minus the mid part (height-midHeight)/2      

    private struct CapsuleHeights
    {
        public float height;
        public float midHeight;
        public float capsuleCapHeight;
        public float radius;

        public CapsuleHeights(Vector3 point1, Vector3 point2, float radius)
        {
            height = Vector3.Distance(point1, point2);
            midHeight = (((height / 2) - radius) * 2);
            midHeight = midHeight > 0 ? midHeight : 0;
            capsuleCapHeight = (height - midHeight) / 2;
            this.radius = radius;
        }
    }

    private struct CapsuleRotationAndDirections
    {
        public Vector3 dirStartToEnd;
        public Vector3 dirEndToStart;
        public Quaternion rotation;
        public Quaternion reverseRot;

        public CapsuleRotationAndDirections(Vector3 point1, Vector3 point2)
        {
            dirStartToEnd = (point2 - point1).normalized;
            dirEndToStart = -dirStartToEnd;
            rotation = Quaternion.LookRotation(dirStartToEnd);
            reverseRot = Quaternion.LookRotation(dirEndToStart);
        }
    }

    private struct CapsuleCenters
    {
        public Vector3 top;
        public Vector3 bot;

        public CapsuleCenters(Vector3 topCenter, Vector3 botCenter)
        {
            this.top = topCenter;
            this.bot = botCenter;
        }
        public CapsuleCenters(Vector3 point1, CapsuleHeights capHeight, CapsuleRotationAndDirections capsRotDir)
        {
            top = point1 + capsRotDir.dirStartToEnd * capHeight.capsuleCapHeight;
            bot = point1 + capsRotDir.dirStartToEnd * (capHeight.capsuleCapHeight + capHeight.midHeight);
        }
    }
    #endregion

}
