using UnityEngine;
using System.Collections;

public class Crate : MonoBehaviour
{
    private float theta = 0.0f;
    private Platform platform = null;
    private TrailRenderer trailRenderer = null;
    private float offset = 2.0f;

    void Awake ()
    {
        platform = GameObject.Find ("Platform(Clone)").GetComponent<Platform> ();
        transform.position = platform.Center;
        transform.localScale = new Vector3 (0.1f, 0.1f, 0.1f);

        trailRenderer = GetComponent<TrailRenderer>();
    }

    public void Update()
    {
        theta += Time.deltaTime * platform.Speed;
        var nextPoint = platform.GenerateXY (theta);

        if (theta > platform.cutoff) {
            transform.up = Vector3.up;
            transform.localScale = Vector3.one;
        } else {
            transform.up = platform.Center - nextPoint;
            var scale = (theta / platform.cutoff) * (theta / platform.cutoff);
            transform.localScale = new Vector3(scale, scale, scale);
        }

        transform.position = nextPoint;
        trailRenderer.transform.position = transform.position;// + offset*(transform.position - platform.Center);

        if (theta > 50)
            GameObject.Destroy (gameObject);
    }
}

