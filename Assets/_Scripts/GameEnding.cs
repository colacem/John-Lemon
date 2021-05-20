using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameEnding : MonoBehaviour
{
    public float fadeDuration=1f;
    public float DisplayImageDuration=1f;
    private bool isPlayerAtExit,isPlayerCaught;
    public GameObject player;
    public CanvasGroup exitBackgroundImageCanvasGroup;
    public CanvasGroup caughtBackgroundImageCanvasGroup;
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
            EndLevel(exitBackgroundImageCanvasGroup,false);
        }
        else if (isPlayerCaught)
        {
            EndLevel(caughtBackgroundImageCanvasGroup,true);
        }
    }
    /// <summary>
    /// Desvanece el canvas group y finaliza el juego
    /// </summary>
    /// <param name="imageCanvasGroup"> imagen de finalizacion</param>
    void EndLevel(CanvasGroup imageCanvasGroup, bool doRestart)
    {   
        timer += Time.deltaTime;
            imageCanvasGroup.alpha=timer/fadeDuration;
            if (timer > Mathf.Clamp(DisplayImageDuration + DisplayImageDuration,0,1))
            {
                Debug.Log("Fin de la Partida");
                if (doRestart)
                {
                    SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                }
                else
                {
                    Application.Quit();
                }
                //SceneManager.LoadScene("");
                
            }
    }

    public void CatchPlayer()
    {
        isPlayerCaught=true;
    }

}
