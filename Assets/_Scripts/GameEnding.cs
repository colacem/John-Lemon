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
    public AudioSource exitAudio, caughtAudio;
    bool hasAudioPlayed;
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
            EndLevel(exitBackgroundImageCanvasGroup,false,exitAudio);
        }
        else if (isPlayerCaught)
        {
            EndLevel(caughtBackgroundImageCanvasGroup,true,caughtAudio);
        }
    }
    /// <summary>
    /// Desvanece el canvas group y finaliza el juego
    /// </summary>
    /// <param name="imageCanvasGroup"> imagen de finalizacion</param>
    void EndLevel(CanvasGroup imageCanvasGroup, bool doRestart, AudioSource audioSource)
    {   
        if (hasAudioPlayed==false)
        {
            audioSource.Play();
            hasAudioPlayed=true;
        }
        timer += Time.deltaTime;
        imageCanvasGroup.alpha=timer/fadeDuration;
        if (timer > Mathf.Clamp(DisplayImageDuration + DisplayImageDuration,0,1))
        {
            Debug.Log("Fin de la Partida");
            if (doRestart)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                hasAudioPlayed=false;
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
