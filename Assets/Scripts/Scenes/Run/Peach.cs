using UnityEngine;
using System.Collections;

public class Peach : MonoBehaviour
{
    private float theta = 0.0f;
    private Platform platform = null;
    private float offset = 120.0f;

    void Awake ()
    {
        platform = GameObject.Find ("Platform(Clone)").GetComponent<Platform> ();
        var pos = new Vector3(0, 0, -2);
		transform.position = platform.Center + pos;
        transform.localScale = new Vector3 (0.01f, 0.01f, 0.01f);
    }

    public void Update()
    {
        theta += Time.deltaTime * platform.Speed;
        var nextPoint = platform.GenerateXY(theta);
		nextPoint.z = -10f;

        if (theta > platform.cutoff) {
			nextPoint += Vector3.up*offset;
            transform.up = Vector3.up;
            transform.localScale = Vector3.one;
        } else {
            transform.up = platform.Center - nextPoint;
            var scale = (theta / platform.cutoff) * (theta / platform.cutoff) * (theta / platform.cutoff);
            transform.localScale = new Vector3(scale, scale, scale);
			nextPoint += (platform.Center - transform.position).normalized*offset*scale;
        }
		
		var z = transform.position.z;
		nextPoint.z = z;
        transform.position = nextPoint;

        if (theta > 50)
            GameObject.Destroy (gameObject);
    }
}