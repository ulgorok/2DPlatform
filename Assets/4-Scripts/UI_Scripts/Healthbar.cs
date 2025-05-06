
using UnityEngine;
using UnityEngine.UI;

public class Healthbar : MonoBehaviour
{
    [SerializeField] private Health characterHealth;
    [SerializeField] private Image totalhealthBar;
    [SerializeField] private Image currenthealthBar;

    private void Start()
    {
        totalhealthBar.fillAmount = 3;
    }

    private void Update()
    {
        currenthealthBar.fillAmount = characterHealth.currentHealth/3;
    }

}
