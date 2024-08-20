using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIControl : MonoBehaviour
{
    public IntroVideoManager videoManager;
    public GameObject uiContainer;
    public AudioSource bgmSource;

    public void StartBtn()
    {
        SceneManager.LoadSceneAsync(1);
    }
    public void EasyBtn()
    {
        playVideo(2);
        HideUI();
        StopBGM();
    }
    public void ModerateBtn()
    {
        // playVideo(3);
        // HideUI();
        // StopBGM();
    }
    public void ImpossibleBtn()
    {
        // playVideo(4);
        // HideUI();
        // StopBGM();
    }
    private void playVideo(int sceneIndex)
    {
        videoManager.StartVideo(sceneIndex);
    }
    private void StopBGM()
    {
        if (bgmSource != null)
        {
            bgmSource.Pause();  // Pause the BGM
        }
    }
    private void HideUI()
    {
        if (uiContainer != null)
        {
            uiContainer.SetActive(false);  // Disable the UI container
        }
    }
    public void Restart()
    {
        SceneManager.LoadSceneAsync(2);
    }

    public void MainMenu()
    {
        SceneManager.LoadSceneAsync(0);
    }
}
