
using UnityEngine;
using UnityEngine.UI;

public class Healthbar : MonoBehaviour
{
    [SerializeField] private Player_Health playerHealth;
    [SerializeField] private Image totalhealthBar;
    [SerializeField] private Image currenthealthBar;
    private object Player_Health;

    private void Start()
    {
        totalhealthBar.fillAmount = 3;
    }

    private void Update()
    {
        currenthealthBar.fillAmount = playerHealth.currentHealth / 3;
    }
}
