using UnityEngine;

public class TutorialIconManager : MonoBehaviour
{
    public GameObject[] tutorialIcons; // Sahnedeki tüm tutorial icon'larını buraya at

    private const string tutorialKey = "TutorialShown";

    private void Start()
    {
        if (PlayerPrefs.GetInt(tutorialKey, 0) == 1)
        {
            HideIcons();
        }
        else
        {
            ShowIcons();
        }
    }

    public void HideIcons()
    {
        foreach (var icon in tutorialIcons)
        {
            icon.SetActive(false);
        }

        PlayerPrefs.SetInt(tutorialKey, 1);
        PlayerPrefs.Save();
    }

    public void ShowIcons()
    {
        foreach (var icon in tutorialIcons)
        {
            icon.SetActive(true);
        }
    }
}

