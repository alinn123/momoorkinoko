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
	private int lives = 8;
	
	private tk2dSprite[] hearts = new tk2dSprite[4];
	
    void Awake ()
    {
        animation = GetComponent<tk2dAnimatedSprite> ();
		for (int i = 0; i < 4; i++)
		{
			hearts[i] = GameObject.Find("Heart"+i).GetComponent<tk2dSprite>();
		}
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
		
		if (lives > 8) lives = 8;
		
		if (lives < 1)
		{
			Application.LoadLevel("Title");	
			return;
		}
		
		for (int i = 0; i < 4; i++)
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

