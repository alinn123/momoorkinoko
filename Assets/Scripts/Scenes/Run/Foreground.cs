using UnityEngine;
using System.Collections;

public class Foreground : MonoBehaviour
{
    private float theta = 0.0f;
    private Platform platform = null;
    private float offset = 4.0f;
	private float scaleFactor = 0.0f;
	private float speedRatio = 1.3f;
	
	private tk2dSprite sprite = null;
	
    void Awake ()
    {
        platform = GameObject.Find ("Platform(Clone)").GetComponent<Platform> ();
        var pos = new Vector3(0, 0, -5);
		transform.position = platform.Center + pos;
		scaleFactor = Random.Range(0.8f, 1.2f);
        transform.localScale = new Vector3 (0.01f, 0.01f, 0.01f);
		
		sprite = gameObject.GetComponent<tk2dSprite>();
		
		var blade = Random.Range(1, 3);
		if (Random.value < 0.05f)
			blade = 4;
		sprite.spriteId = 20 + blade;
    }

    public void Update()
    {
        theta += Time.deltaTime * platform.Speed*speedRatio;
        var nextPoint = platform.GenerateXY(theta);

        if (theta > platform.cutoff) {
			nextPoint += Vector3.up*offset;
            transform.up = Vector3.up;
            transform.localScale = Vector3.one*scaleFactor;
        } else {
            transform.up = platform.Center - nextPoint;
            var scale = (theta / platform.cutoff) * (theta / platform.cutoff) * (theta / platform.cutoff) * scaleFactor;
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

