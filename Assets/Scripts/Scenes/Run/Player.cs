using UnityEngine;
using System.Collections;

public enum PlayerStatus {
    RUNNING = 0,
    JUMPING,
    COLLIDED,
}

public class Player : MonoBehaviour
{
    public PlayerStatus state = 0;
    private tk2dAnimatedSprite animation = null;
	private int lives = 2;
	
	private int maxLives = 2;
	private tk2dSprite[] hearts = new tk2dSprite[1];
	private UILabel peachLabel = null;
	private int peaches = 0;
	
    void Awake ()
    {
		peaches = 0;
        animation = GetComponent<tk2dAnimatedSprite> ();
		for (int i = 0; i < maxLives/2; i++)
		{
			hearts[i] = GameObject.Find("Heart"+i).GetComponent<tk2dSprite>();
		}
		
		peachLabel = GameObject.Find("PeachLabel").GetComponent<UILabel>();
    }

    public void Jump ()
    {
        //rigidbody.velocity = Vector3.up * 7.0f;
        rigidbody.AddForce (Vector3.up * 7000, ForceMode.Impulse);
        state = PlayerStatus.JUMPING;
        animation.Play ("Jump");
    }

    void OnCollisionEnter (Collision collision)
    {
        if (state != PlayerStatus.RUNNING && collision.collider.name == "Plane") {
            Debug.Log ("RUN");
            animation.Play ("Run");
            state = PlayerStatus.RUNNING;
        }
			
		if (collision.collider.material.name.StartsWith("Obstacle"))
		{
			UpdateLife(-1);
			GameObject.Destroy(collision.collider.gameObject);
		}
		
		if (collision.collider.material.name.StartsWith("Life"))
		{
			peaches++;
			peachLabel.text = string.Format("PEACHES {0}", peaches);
			UpdateLife(1);
			GameObject.Destroy(collision.collider.gameObject);
		}
    }

    void OnCollisionStay (Collision collision)
    {
    }
	
	void UpdateLife(int change)
	{
		lives += change;
		
		if (lives > maxLives) lives = maxLives;
		
		if (lives < 1)
		{
			Application.LoadLevel("Title");	
			return;
		}
		
		for (int i = 0; i < maxLives/2; i++)
		{
			if (lives >= 2*i+1)
			{
				hearts[i].gameObject.SetActive(true);
				hearts[i].spriteId = hearts[i].GetSpriteIdByName(lives > 2*i+1 ? "heart-full" : "heart-half");
			}
			else
			{
				hearts[i].gameObject.SetActive(false);
			}
		}

	}
}

