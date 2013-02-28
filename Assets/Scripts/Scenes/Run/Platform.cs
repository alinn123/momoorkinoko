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
    private float lastCrate = 0.0f;

    public float cutoff = 23.5f;
    public float ground_y = 90f;

    void Awake ()
    {
        Speed = 3f;
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

    private void Regenerate ()
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

    private void GenerateCrate()
    {
        var crate = GameObject.Instantiate(Resources.Load("Prefabs/Run/Crate"));
    }

    void Update ()
    {
        if (Time.time - lastCrate > 2.7 && Random.value < 0.1f) {
            GenerateCrate ();
            lastCrate = Time.time;
        }
    }
}

