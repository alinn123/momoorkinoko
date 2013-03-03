using UnityEngine;
using System.Collections;

public class Platform : MonoBehaviour
{
    private const float e = 2.71828f;

    public const float a = 1.0f;
    public const float b = 0.2f;
    private float N = 25.5f;

    public Vector3 Center { get; set; }
    public float Speed { get; set; }
    private float lastShroom = 0.0f;
	private float lastTree   = 0.0f;
	
	private float lastPeach = 0.0f;
	
    public float cutoff = 23.5f;
    public float ground_y = 90f;
	private int birds = 0;
	private float nextBird = 0.0f;
	private float fromBird = 0.0f;
	private float lastBird = 0.0f;

    void Awake ()
    {
        Speed = 4f;
        Center = new Vector3 (525f, 440f, 0f);

        Regenerate ();
    }

    public Vector3 GenerateXY (float theta)
    {
        float r = a * Mathf.Pow (e, b * theta);
        Vector3 nextPoint = 3.2f*new Vector3 (-r * Mathf.Cos (theta), r * Mathf.Sin (theta), 0) + Center;

        if (theta > cutoff)
        {
            nextPoint = new Vector3(Center.x - (theta - cutoff) / 2f * Center.x, ground_y, 0);
        }

        return nextPoint;
    }

    private void Regenerate()
    {
        Vector3 point = new Vector3(Center.x, Center.y, 0);
        for (float i = 0; i < N; i+=0.5f)
        {
            float theta = i;// * Mathf.Deg2Rad;
            Vector3 nextPoint = GenerateXY(theta);

            Debug.DrawLine(point, nextPoint, Color.red, 100.0f);
            point = nextPoint;
        }
    }

    private void GenerateShroom()
    {
        var shroom = GameObject.Instantiate(Resources.Load("Prefabs/Run/Mushroom"));
    }
	
	private void GenerateTree()
    {
        var shroom = GameObject.Instantiate(Resources.Load("Prefabs/Run/Tree"));
    }

	private void GenerateBird()
    {
        var shroom = GameObject.Instantiate(Resources.Load("Prefabs/Run/Birds"));
    }
	
	private void GeneratePeach()
    {
        var shroom = GameObject.Instantiate(Resources.Load("Prefabs/Run/Peach"));
    }

	
    void Update ()
    {
        if (Time.time - lastShroom > 2.7 && Random.value < 0.1f) {
            GenerateShroom ();
            lastShroom = Time.time;
        }

	    /*
	    if (Time.time - lastTree > 1.5 && Random.value < 0.1f) {
            GenerateTree ();
            lastTree = Time.time;
        }
        */
		
		if (Time.time > lastPeach + 10.0f && Random.value < 0.01f)
		{
			lastPeach = Time.time;
			GeneratePeach();
		}
		
		if (Random.value < 0.01f)
		{
			birds += Random.Range(1, 15);
			nextBird = Random.Range(0.1f, 1.0f);
		}
		
		fromBird += Time.deltaTime;
		if (birds > 0 && fromBird > nextBird)
		{
			GenerateBird();
			fromBird = 0.0f;
			nextBird = Random.Range(0.1f, 1.0f);
		}

	}
}

