using UnityEngine;

public class ExitGame : MonoBehaviour
{
    public void QuitGame()
    {
        Debug.Log("Oyun kapanýyor...");
        Application.Quit();
    }
}
