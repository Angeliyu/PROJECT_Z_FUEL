using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class IntroVideoManager : MonoBehaviour
{
    public  VideoPlayer videoPlayer;
    private string videoFileName = "intro_animation.mov";
    private int targetSceneIndex;

    public void StartVideo(int sceneIndex)
    {
        string videoPath = System.IO.Path.Combine(Application.streamingAssetsPath, videoFileName);
        videoPlayer.url = videoPath;
        videoPlayer.errorReceived += OnVideoError;
        targetSceneIndex = sceneIndex; 
        videoPlayer.Prepare();
        videoPlayer.prepareCompleted += OnPrepareCompleted;
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
        vp.loopPointReached += OnVideoEnd;
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
