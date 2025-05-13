using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShieldHealthBar : MonoBehaviour
{
    [SerializeField] private Player_Health playerShieldHealth;
    [SerializeField] private Image totalShieldhealthBar;
    [SerializeField] private Image currentShieldhealthBar;
    private object Player_Health;

    private void Start()
    {
        totalShieldhealthBar.fillAmount = 3;
    }

    private void Update()
    {
        currentShieldhealthBar.fillAmount = playerShieldHealth.currentShieldHealth / 3;

        if (playerShieldHealth.currentShieldHealth == 0)
        {
            this.gameObject.SetActive(false);
        }
    }
}
