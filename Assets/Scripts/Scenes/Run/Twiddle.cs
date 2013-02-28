using UnityEngine;
using System.Collections;

public class Twiddle : MonoBehaviour
{
    public int paramId = 0;
    public int plusMinus = -1;
    public float changeValue = 0.1f;

    private GameObject platform = null;
    // Use this for initialization
    void Awake ()
    {
        platform = GameObject.Find("Platform(Clone)");
    }

    void OnClick()
    {
        platform.SendMessage("ChangeParam" + (paramId == 0 ? "A" : "B"), plusMinus * changeValue);
    }
}

