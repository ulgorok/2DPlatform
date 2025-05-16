using UnityEngine;

public class PlayerRespawn : MonoBehaviour
{
    private Player_Health playerHealth;

    private void Awake()
    {
        playerHealth = GetComponent<Player_Health>();
    }

    void ResetAllEnemies()
    {
        void ResetAllEnemies()
        {
            EnemyRespawnHandler[] enemies = FindObjectsOfType<EnemyRespawnHandler>();
            Debug.Log("Enemies found: " + enemies.Length);
            foreach (var enemy in enemies)
            {
                Debug.Log("Resetting enemy: " + enemy.name);
                enemy.ResetEnemy();
            }
        }

    }

    public void Respawn()
    {
        Debug.Log("Player Respawn called");
        playerHealth.Respawn(); //Restore player health and reset animation
        ResetAllEnemies();      // <-- EKLENDİ
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Checkpoint")
        {
            collision.GetComponent<Collider2D>().enabled = false;
            collision.GetComponent<Animator>().SetTrigger("activate");
        }
    }
}