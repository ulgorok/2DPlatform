using System.Collections.Generic;
using UnityEngine;

public class PlayerRespawn : MonoBehaviour
{
    private Player_Health playerHealth;

    public static List<EnemyRespawnHandler> deadEnemies;
    public TutorialIconManager tutorialIconManager; // Inspector'da atayacaksın

    AudioManager audioManager; //

    private void Awake()
    {
        playerHealth = GetComponent<Player_Health>();
        deadEnemies = new();

        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>(); //
    }

    void ResetAllEnemies()
    {
        //EnemyRespawnHandler[] enemies = FindObjectsOfType<EnemyRespawnHandler>();
        //Debug.Log("Enemies found: " + enemies.Length);
        foreach (var enemy in deadEnemies)
        {
            Debug.Log("Resetting enemy: " + enemy.name);
            enemy.ResetEnemy();
        }

    }
    public void Respawn()
    {
        Debug.Log("Player Respawn called");

        audioManager.PlaySFX(audioManager.respawn); //

        ResetAllEnemies();
        ResetAllChests();     // <-- Sandıkları sıfırla

        if (PlayerPrefs.GetInt("TutorialShown", 0) == 0)
        {
            tutorialIconManager.HideIcons();
        }
    }
    //public void Respawn()
    //{
    //    Debug.Log("Player Respawn called");
    //    ResetAllEnemies();      // <-- EKLENDİ

    //    // İlk respawn sonrası tutorial ikonları kapat
    //    if (PlayerPrefs.GetInt("TutorialShown", 0) == 0)
    //    {
    //        tutorialIconManager.HideIcons();
    //    }
    //}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Checkpoint")
        {
            collision.GetComponent<Collider2D>().enabled = false;
            collision.GetComponent<Animator>().SetTrigger("activate");
        }
    }

    void ResetAllChests()
    {
        foreach (var chest in Chest.allChests)
        {
            chest.ResetChest();
        }
    }


}