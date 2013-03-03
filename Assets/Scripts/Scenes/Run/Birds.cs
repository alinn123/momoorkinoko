using UnityEngine;
using System.Collections;

public class Birds : MonoBehaviour {
	
	private tk2dAnimatedSprite animation;
	
	void Awake()
	{
		transform.localScale = new Vector3 (0.01f, 0.01f, 0.01f);
		animation = GetComponent<tk2dAnimatedSprite>();

		int bird = Random.value < 0.5f ? 1 : 2;
		animation.Play("Bird" + bird);
		gameObject.name = "Bird-" + bird; 
	}
	
	void Start()
	{
		int path = Random.Range(1, 4);
		if (path > 3) path = 3;
		Hashtable args = new Hashtable();
		args["time"] = 10.0f;
		var p = iTweenPath.GetPath("Fly-" + path);
		if (p == null)
			Debug.Log ("Cannot find path Fly-" + path);
		args["path"] = iTweenPath.GetPath("Fly-" + path);
		args["oncomplete"] = "OnFlyComplete";
		args["name"] = "Path" + path;
		iTween.MoveTo(gameObject, args);
		Debug.Log ("Path" + path);
	}
	
	void OnFlyComplete()
	{
		Debug.Log("DONE");
		Destroy(gameObject);
	}
}
