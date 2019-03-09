using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GramDrawer : MonoBehaviour
{

    public float offset;
    public float radius;
    public int numPoints;
    public Color unusedColor;
    public Color usedColor;
    public List<GameObject> unusedObjs = new List<GameObject>();
    List<GameObject> pointsInUse = new List<GameObject>();
    public List<LineRenderer> lineRenders = new List<LineRenderer>();
    public bool useLocal = false;
    public List<AudioSource> redrawSounds = new List<AudioSource>();
    public bool drawStart = true;
    public void DrawOverTime(float time)
    {
        Vector3[] pos = new Vector3[0];

        ReturnLines();
        ReturnPoints();
        float twoPi = Mathf.PI * 2;
        float increment = twoPi / numPoints;
        for (int i = 0; i < numPoints; i++)
        {
            float theta = (-1 * i * increment) + offset;
            GameObject point = GetPoint();
            if (useLocal)
                point.transform.localPosition = PolarToCart(theta);
            else
                point.transform.position = PolarToCart(theta);

        }
        StartCoroutine("DrawLinesOverTime", time);
    }

    IEnumerator DrawLinesOverTime(float time)
    {
        foreach (LineRenderer render in lineRenders)
        {
            render.SetColors(new Color(.7f, .7f, .7f, 0.75f), new Color(.7f, .7f, .7f, 0.75f));
        }
        int steps = 0;
        if (numPoints < 5)
        {
            steps = 1;
        }
        else
        {
            steps = numPoints % 2 == 0 ? numPoints : numPoints + 1;
            steps = (numPoints - 6) / 2 + 2;
        }

        if(numPoints >= 3)
        {
            for (int i = 0; i < numPoints; i++)
            {

                // Get our line renders
                lineRenders[i].transform.gameObject.SetActive(true);
                lineRenders[i + numPoints].transform.gameObject.SetActive(true);

                // Get start and end point
                int index = i + steps < numPoints ? i + steps : i + steps - numPoints;
                Vector3 start;
                Vector3 end;
                if (useLocal)
                {
                    start = pointsInUse[i].transform.localPosition;
                    end = pointsInUse[index].transform.localPosition;
                }
                else
                {
                    start = pointsInUse[i].transform.position;
                    end = pointsInUse[index].transform.position;
                }
                int index2 = i + 1 < numPoints ? i + 1 : 0;
                Vector3 end2;
                if (useLocal)
                    end2 = pointsInUse[index2].transform.localPosition;
                else
                    end2 = pointsInUse[index2].transform.position;


                // Determine time


                float timeForThisLine = (time - Time.time) / (numPoints - i);
                float timeToComplete = Time.time + timeForThisLine;
                Debug.Log("timeForThisLine : " + timeForThisLine);

                int numLoops = (int)Mathf.Ceil(timeForThisLine / Time.smoothDeltaTime);
                float indexIncrement = 1.05f / numLoops;
                float ii = 0;
                for (int index3 = 0; index3 < numLoops; index3++)
                {

                    Vector3[] positions = new Vector3[] { start, Vector3.Lerp(start, end, ii) };
                    lineRenders[i].SetPositions(positions);

                    Vector3[] positions2 = new Vector3[] { start, Vector3.Lerp(start, end2, ii) };
                    lineRenders[i + numPoints].SetPositions(positions2);

                    yield return null;
                    ii += indexIncrement;

                    if (Time.time >= timeToComplete)
                        break;
                }
                Vector3[] positions3 = new Vector3[] { start, Vector3.Lerp(start, end, 1) };
                lineRenders[i].SetPositions(positions3);

                Vector3[] positions4 = new Vector3[] { start, Vector3.Lerp(start, end2, 1) };
                lineRenders[i + numPoints].SetPositions(positions4);


                if (i == 0 || i % ((numPoints / 10) + 1) == 0)
                    redrawSounds[Random.Range(0, redrawSounds.Count)].Play();
            }

            // After shape is drawn 
            foreach (LineRenderer render in lineRenders)
            {
                render.SetColors(new Color(.6f, .2f, .19f), new Color(.6f, .2f, .2f));
            }
            yield return new WaitForSeconds(.3f);
        }
   
        
        gameObject.SetActive(false);
    }
    public void SetPoint(int index)
    {
        if (pointsInUse[index].GetComponent<SpriteRenderer>() != null)
            pointsInUse[index].GetComponent<SpriteRenderer>().color = usedColor;
    }

    public void SetPointsInUse(int num)
    {
        for(int i = 0; i < num; i++)
        {
            SetPoint(i);
        }
    }
    void DrawLine()
    {
        int steps = 0;
        if (numPoints < 5)
        {
            steps = 1;
        }
        else
        {
            steps = numPoints % 2 == 0 ? numPoints : numPoints + 1;
            steps = (numPoints - 6) / 2 + 2;
        }

        for (int i = 0; i < numPoints; i++)
        {

            lineRenders[i].transform.gameObject.SetActive(true);
            Vector3 start;
            Vector3 end;
            int index = i + steps < numPoints ? i + steps : i + steps - numPoints;
            if (useLocal)
            {
                start = pointsInUse[i].transform.localPosition;
                end = pointsInUse[index].transform.localPosition;
            }
            else
            {
                start = pointsInUse[i].transform.position;
                end = pointsInUse[index].transform.position;
            }
            Vector3[] positions = new Vector3[] { start, end };
            lineRenders[i].SetPositions(positions);

            lineRenders[i + numPoints].transform.gameObject.SetActive(true);

            int index2 = i + 1 < numPoints ? i + 1 : 0;

            Vector3 end2;
            if (useLocal)
                end2 = pointsInUse[index2].transform.localPosition;
            else
            {
                end2 = pointsInUse[index2].transform.position;
            }
            Vector3[] positions2 = new Vector3[] { start, end2 };
            lineRenders[i + numPoints].SetPositions(positions2);

        }

    }
    void ReturnLines()
    {
        foreach (LineRenderer lr in lineRenders)
        {
            lr.gameObject.SetActive(false);
        }
    }   
    void ReturnPoints()
    {
        foreach (GameObject obj in pointsInUse)
        {
            obj.SetActive(false);
            if(obj.GetComponent<SpriteRenderer>() != null)
                obj.GetComponent<SpriteRenderer>().color = unusedColor;
            unusedObjs.Add(obj);
        }
        pointsInUse.Clear();
    }

    GameObject GetPoint()
    {
        GameObject obj = unusedObjs[unusedObjs.Count - 1];
        obj.SetActive(true);
        pointsInUse.Add(obj);
        unusedObjs.RemoveAt(unusedObjs.Count - 1);
        return obj;
    }

    struct polarCord
    {
        public float radius;
        public float theta; // in rad
    }

    polarCord CartToPolar(float x, float y)
    {
        polarCord pc = new polarCord();
        pc.radius = Mathf.Sqrt(x * x + y * y);
        pc.theta = Mathf.Tan(y / x);
        return pc;
    }

    Vector3 PolarToCart(float theta)
    {
        float x = radius * Mathf.Cos(theta);
        float y = radius * Mathf.Sin(theta);
        return new Vector3(x, y, transform.position.z + 1);
    }

    public void ReDrawGram()
    {
        ReturnLines();
        ReturnPoints();
        float twoPi = Mathf.PI * 2;
        float increment = twoPi / numPoints;
        for (int i = 0; i < numPoints; i++)
        {
            float theta = (-1 * i * increment) + offset;
            GameObject point = GetPoint();
            if(useLocal)
                point.transform.localPosition = PolarToCart(theta);
            else
                point.transform.position = PolarToCart(theta);

        }
        DrawLine();
    }

    // Use this for initialization
    void Start()
    {
        lastTime = Time.time;
        foreach (Transform trans in transform.GetChild(0))
        {
            lineRenders.Add(trans.gameObject.GetComponent<LineRenderer>() as LineRenderer);
            trans.gameObject.SetActive(false);

        }
        foreach (Transform trans in transform.GetChild(1))
        {
            unusedObjs.Add(trans.gameObject);
            trans.gameObject.SetActive(false);

        }
        offset = Mathf.PI / 2 * 1;
        if(drawStart)
            ReDrawGram();

    }

    float lastTime = 0;
    // Update is called once per frame
    void Update()
    {
        //if (done)
        //{
        //    done = false;
        //    lastTime = Time.time;
        //    numPoints++;
        //    ReturnPoints();
        //    DrawGram();
        //    StartCoroutine("DrawLinesOverTime");


        //}

        ////offset += 0.011f;
    }
}
