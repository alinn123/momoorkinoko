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

    void Awake ()
    {
        animation = GetComponent<tk2dAnimatedSprite> ();
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
    }

    void OnCollisionStay (Collision collision)
    {
    }
}

