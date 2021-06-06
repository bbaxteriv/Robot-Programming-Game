using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayButton : MonoBehaviour
{
  public void PlayGame() {
    Debug.Log("clicked");
    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
  }

  void Start() {
      Debug.Log("started");
  }
}
