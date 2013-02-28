using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RunController : MonoBehaviour
{
    private Platform platform = null;
    private Player player = null;
    private bool jumping = false;

    private tk2dCameraAnchor cameraAnchor = null;

    void Awake ()
    {
        cameraAnchor = GameObject.Find("2DAnchor").GetComponent<tk2dCameraAnchor>();

        var platformGO = (GameObject)GameObject.Instantiate (Resources.Load ("Prefabs/Run/Platform"));
        platform = platformGO.GetComponent<Platform> ();

        var playerGO = (GameObject)GameObject.Instantiate (Resources.Load ("Prefabs/Run/Player"));
        //playerGO.transform.parent = cameraAnchor.transform;
        player = playerGO.GetComponent<Player> ();

    }

    void Update ()
    {
        bool mouseDown = Input.GetMouseButtonDown(0);
        if (player.state == PlayerStatus.RUNNING && mouseDown && !jumping) {
            player.Jump();
            Debug.Log("Jump");
        }
        if (jumping && !Input.GetMouseButton (0)) {
            jumping = false;
        }
    }
}
