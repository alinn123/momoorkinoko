using UnityEngine;

public class Startup : MonoBehaviour
{
  private GameObject game = null;

  public void Awake()
  {
    Debug.Log("Game is starting");
    game = GameObject.Find("Game");
    if (game == null)
    {
      if (Application.loadedLevelName == "Title")
      {
        game = new GameObject("Game");
        GameObject.DontDestroyOnLoad(game);
      }
      else
      {
        Application.LoadLevel("Title");
      }
    }
  }
}
