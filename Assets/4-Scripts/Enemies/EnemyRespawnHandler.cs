using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRespawnHandler : MonoBehaviour
{
    private Vector3 initialPosition;
    private Quaternion initialRotation;
    private LightEnemy_Health enemyScript;

    void Start()
    {
        initialPosition = transform.position; // Otomatik pozisyon kaydı
        initialRotation = transform.rotation;
        enemyScript = GetComponent<LightEnemy_Health>();
    }

    public void ResetEnemy()
    {
        Debug.Log("Enemy reset called on: " + gameObject.name);
        transform.position = initialPosition;
        transform.rotation = initialRotation;

        if (enemyScript != null)
        {
            enemyScript.ResetStats();
        }

        gameObject.SetActive(true);
    }

}
