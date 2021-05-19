using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEnding : MonoBehaviour
{
    public float fadeDuration=1f;
    public float DisplayImageDuration=1f;
    private bool isPlayerAtExit;
    public GameObject player;
    public CanvasGroup exitBackgroundImageCanvasGroup;
    private float timer;
    private void OnTriggerEnter(Collider other) {
        if (other.gameObject==player)
        {
            isPlayerAtExit=true;
        }
    }
    private void Update() {
        if (isPlayerAtExit)
        {
            timer += Time.deltaTime;
            exitBackgroundImageCanvasGroup.alpha=timer/fadeDuration;
            if (timer > Mathf.Clamp(DisplayImageDuration + DisplayImageDuration,0,1))
            {
                EndLevel();
            }
        }
    }
    /// <summary>
    /// Desvanece el canvas group y finaliza el juego
    /// </summary>
    void EndLevel()
    {
        Debug.Log("Fin de la Partida");
        //SceneManager.LoadScene("");
        Application.Quit();
    }


}
