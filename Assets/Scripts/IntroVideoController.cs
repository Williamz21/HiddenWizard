using System.Collections;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class ReproductorVideo : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    public string escenaAlFinalizar;

    void Start()
    {
        videoPlayer.loopPointReached += CambiarEscenaAlFinalizar;
    }

    void CambiarEscenaAlFinalizar(VideoPlayer vp)
    {
        SceneManager.LoadScene(escenaAlFinalizar);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !videoPlayer.isPlaying)
        {
            videoPlayer.Play();
        }
    }
}