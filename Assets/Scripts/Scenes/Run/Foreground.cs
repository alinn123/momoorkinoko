using UnityEngine;
using System.Collections;

public class Foreground : MonoBehaviour
{
    private float theta = 0.0f;
    private Platform platform = null;
    private float offset = 2.0f;
	private float speedRatio = 1.5f;

    void Awake ()
    {
        platform = GameObject.Find ("Platform(Clone)").GetComponent<Platform> ();
        var pos = new Vector3(0, 0, -5);
		transform.position = platform.Center + pos;
        transform.localScale = new Vector3 (0.1f, 0.1f, 0.1f);
    }

    public void Update()
    {
        theta += Time.deltaTime * platform.Speed*speedRatio;
        var nextPoint = platform.GenerateXY(theta);

        if (theta > platform.cutoff) {
            transform.up = Vector3.up;
            transform.localScale = Vector3.one;
        } else {
            transform.up = platform.Center - nextPoint;
            var scale = (theta / platform.cutoff) * (theta / platform.cutoff) * (theta / platform.cutoff);
            transform.localScale = new Vector3(scale, scale, scale);
        }
		
		var z = transform.position.z;
		nextPoint.z = z;
        transform.position = nextPoint;

        if (theta > 50)
            GameObject.Destroy (gameObject);
    }
}

