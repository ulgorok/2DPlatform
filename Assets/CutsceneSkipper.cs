using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class CutsceneSkipper : MonoBehaviour
{
    public VideoPlayer videoPlayer; // IntroCutscene videosu
    public string sceneToLoad = "1st_Level";

    void Start()
    {
        // Video tamamlandığında sahneye geç
        videoPlayer.loopPointReached += OnVideoFinished;
    }

    void Update()
    {
        // ESC tuşuna basıldığında sahneye geç
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SkipCutscene();
        }
    }

    public void SkipCutscene()
    {
        // Video'yu durdur ve sahneye geç
        videoPlayer.Stop();
        SceneManager.LoadScene(sceneToLoad);
    }

    void OnVideoFinished(VideoPlayer vp)
    {
        SceneManager.LoadScene(sceneToLoad);
    }
}
