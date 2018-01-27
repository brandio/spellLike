using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class LakeDrawer
{
    float radius;
    Vector2 center;
    int[,] lakeGrid;
    const float samplesPerUnit = 10;
    const float lilyPadsPerArea = .06f;
    const float reedsPerCirc = .15f;
    public LakeDrawer(float lakeRadius, Vector2 lakeCenter)
    {
        radius = lakeRadius;
        center = lakeCenter;
        int samples = (int)Mathf.Ceil((radius * 2) * samplesPerUnit);
        lakeGrid = new int[samples, samples];
        DrawLake();
    }

    void DrawReeds()
    {
        float circum = Mathf.PI * radius * 2;
        for(int i = 0; i < circum * reedsPerCirc; i++)
        {
            Vector2 position = UnityEngine.Random.insideUnitCircle.normalized;
            position = center + position * radius * 1.1f;

            GameObject lilyPad = GameObject.Instantiate(Resources.Load("RoomStuff" + System.IO.Path.DirectorySeparatorChar.ToString() + "Reeds" + System.IO.Path.DirectorySeparatorChar.ToString() + "ReedSpawner"),
                                                        position, Quaternion.identity) as GameObject;
        }
    }

    void DrawLilyPads()
    {
        float area = Mathf.PI * radius * radius;
        float ourRadius = radius / 1.4f;
        List<Vector2> lilyPositions = new List<Vector2>();
        for (int i = 0; i < lilyPadsPerArea * area; i++)
        {
            bool positionGood = false;
            int tries = 20;
            int ii = 0;
            while (!positionGood)
            {
                ii++;
                Vector2 myPosition = UnityEngine.Random.insideUnitCircle * ourRadius;
                positionGood = true;
                foreach (Vector2 position in lilyPositions)
                {
                    if(ii > tries)
                    {
                        return;
                    }
                    
                    if(Vector2.Distance(myPosition, position) < 1)
                    {
                        positionGood = false;
                        break;
                    }
                }
                if(positionGood)
                {
                    lilyPositions.Add(myPosition);
                    GameObject lilyPad = GameObject.Instantiate(Resources.Load("RoomStuff" + System.IO.Path.DirectorySeparatorChar.ToString() + "LilyPad"), Vector2.zero, Quaternion.identity) as GameObject;
                    lilyPad.transform.position = center + myPosition;
               
                }
            }

        }
    }

    void DrawLake()
    {
        // Create lake object
        GameObject mygameobject = new GameObject();
        mygameobject.transform.position = center;
        mygameobject.layer = LayerMask.NameToLayer("Water");
        MeshRenderer renderer = mygameobject.AddComponent<MeshRenderer>() as MeshRenderer;

        // Put water effect on top of it
        /*
        GameObject waterEffect = GameObject.Instantiate(Resources.Load("Water" + System.IO.Path.DirectorySeparatorChar.ToString() + "grad") as GameObject, Vector2.zero, Quaternion.identity) as GameObject;
        waterEffect.transform.eulerAngles = new Vector3(0, 0, UnityEngine.Random.Range(0, 360));
        float scale = radius / 10;
        waterEffect.transform.localScale = new Vector3(scale,scale,scale);
        waterEffect.transform.parent = mygameobject.transform;
        waterEffect.transform.localPosition = Vector2.zero;
        */
        // Create collider
        float seed = UnityEngine.Random.Range(0, 1000);

        float waterBias = 1;
        float size = .3f;
        float distanceWeight = .05f;
        float distanceFromEdgeWeight = 5000;
        for (int x = 0; x < lakeGrid.GetLength(0); x++)
        {
            for (int y = 0; y < lakeGrid.GetLength(1); y++)
            {
                float xPos = GetPosition(x, true);
                float yPos = GetPosition(y, false);
                float height = Mathf.PerlinNoise((xPos + seed) * size, (yPos + seed) * size) * .1f;
                float distance = Mathf.Sqrt(Mathf.Pow(center.x - xPos, 2) + Mathf.Pow(center.y - yPos, 2));
                float relativeDistance = distance / radius;

                if (relativeDistance  + height < waterBias)
                {
                    lakeGrid[x, y] = 1;
                }

            }
        }

        List<Vector2> points = new List<Vector2>();
        Vector2 orginTemp = new Vector2(0, 0);
        for (int x = 0; x < lakeGrid.GetLength(0); x++)
        {
            for (int y = 0; y < lakeGrid.GetLength(1); y++)
            {
                if (lakeGrid[x, y] == 1)
                {
                    if (!IsSurroundedByLake(x, y))
                    {
                        float xPos = GetPosition(x, true) - center.x;
                        float yPos = GetPosition(y, false) - center.y;

                        orginTemp.x += xPos;
                        orginTemp.y += yPos;
                        points.Add(new Vector2(xPos, yPos));
                    }
                }
            }
        }
        Vector2 orgin = new Vector2(orginTemp.x / points.Count, orginTemp.y / points.Count);
        Debug.Log(orgin + "orgin");
        Debug.DrawLine(orgin, orgin + new Vector2(300, 300), Color.blue);
        Vector2[] sortedArray = points.ToArray();
        Array.Sort(sortedArray, new ClockwiseComparer(orgin));

        PolygonCollider2D collider = mygameobject.AddComponent<PolygonCollider2D>();
        collider.SetPath(0, sortedArray);

        // Create mesh
        mygameobject.AddComponent<MeshFilter>();
        ColliderToMesh colliderToMesh = mygameobject.AddComponent<ColliderToMesh>() as ColliderToMesh;
        colliderToMesh.init(collider, mygameobject, orgin);
        renderer.material = Resources.Load("WaterSurface") as Material;

        DrawLilyPads();
        DrawReeds();
    }

    float GetPosition(float cord, bool xPos)
    {
        if (xPos)
            return (center.x + (cord / samplesPerUnit - radius));

        return (center.y + (cord / samplesPerUnit - radius));
    }

    bool IsSurroundedByLake(int x, int y)
    {
        if (x == 0 || y == 0 || x == lakeGrid.GetLength(1) - 1 || y == lakeGrid.GetLength(1) - 1 || x == lakeGrid.GetLength(0) - 1)
        {
            Debug.LogError("Lake out of bounds");
            return false;
        }

        if (lakeGrid[x + 1, y] == 0)
        {
            return false;
        }
        if (lakeGrid[x - 1, y] == 0)
        {
            return false;
        }
        if (lakeGrid[x, y + 1] == 0)
        {
            return false;
        }
        if (lakeGrid[x, y - 1] == 0)
        {
            return false;
        }

        return true;
    }
}
