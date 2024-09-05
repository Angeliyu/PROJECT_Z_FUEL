using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;
using System.Collections;
using UnityEngine.UI;

public class IntroVideoManager : MonoBehaviour
{
    public  VideoPlayer videoPlayer;
    // private string videoFileName = "/Intro_Animation.mp4";
    // public VideoClip videoClip;
    public GameObject loadingScreen; 
    private int targetSceneIndex;

    public void StartVideo(int sceneIndex)
    {
        StartCoroutine(PlayVideo());
        // string videoPath = Application.streamingAssetsPath + videoFileName;
        // videoPlayer.url = videoPath;
        // // videoPlayer.clip = videoClip;
        // videoPlayer.errorReceived += OnVideoError;
        targetSceneIndex = sceneIndex; 
        // videoPlayer.Prepare();
        // videoPlayer.prepareCompleted += OnPrepareCompleted;
    }

    IEnumerator PlayVideo()
    {
        string videoPath = System.IO.Path.Combine(Application.streamingAssetsPath, "Intro_Animation.mp4");

        #if UNITY_WEBGL && !UNITY_EDITOR
        videoPath = System.Uri.EscapeUriString(videoPath);
        #endif

        using (UnityWebRequest uwr = UnityWebRequest.Get(videoPath))
        {
            yield return uwr.SendWebRequest();

            if (uwr.isNetworkError || uwr.isHttpError)
            {
                Debug.LogError(uwr.error);
            }
            else
            {
                videoPlayer.url = videoPath;
                videoPlayer.Prepare();
                loadingScreen.SetActive(false);
                OnPrepareCompleted(videoPlayer);
            }
        }
    }

    // void Start()
    // {
    //     string videoPath = System.IO.Path.Combine(Application.streamingAssetsPath, videoFileName);
    //     videoPlayer.url = videoPath;
    //     videoPlayer.errorReceived += OnVideoError;
    //     videoPlayer.Prepare();
    //     videoPlayer.prepareCompleted += OnPrepareCompleted;
    // }

    void OnPrepareCompleted(VideoPlayer vp)
    {
        vp.Play();
        StartCoroutine(CheckBuffering());
        vp.loopPointReached += OnVideoEnd;
    }

    IEnumerator CheckBuffering()
    {
        while (videoPlayer.isPrepared && (ulong)videoPlayer.frame < videoPlayer.frameCount - 10)
        {
            if (videoPlayer.isPlaying && videoPlayer.frame > 0)
            {
                yield return null;
            }
            else
            {
                Debug.Log("Buffering...");
                videoPlayer.Pause();
                yield return new WaitForSeconds(0.5f);
                videoPlayer.Play();
            }
        }
    }

    void OnVideoError(VideoPlayer vp, string message)
    {
        Debug.LogError("VideoPlayer Error: " + message);
    }

    void OnVideoEnd(VideoPlayer vp)
    {
        // Load the next scene after the video ends
        SceneManager.LoadSceneAsync(targetSceneIndex);
    }

    public void skipIntro()
    {
        SceneManager.LoadSceneAsync(targetSceneIndex);
    }
}
