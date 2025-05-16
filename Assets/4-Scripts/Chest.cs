using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{
    public bool IsOpened { get; private set; }
    public string ChestID { get; private set; }
    public GameObject itemPrefab; // Item that chest drops
    public SpriteRenderer _spriteRenderer;
    public Sprite openedSprite;
    public CameraShake cameraShake;

    public void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();

        // Kamera shake component'ini otomatik bul (sahnedeki)
        if (cameraShake == null)
        {
            cameraShake = FindObjectOfType<CameraShake>();
            if (cameraShake == null)
                Debug.LogWarning("CameraShake bulunamadı! Sahnede CameraShake component'li bir obje olmalı.");
        }
    }

    public void OnTriggerStay2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Player") && !IsOpened)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                _spriteRenderer.sprite = openedSprite;
                OpenChest();
            }
        }
    }

    private void OpenChest()
    {
        if (itemPrefab)
        {
            GameObject droppedItem = Instantiate(itemPrefab, transform.position + Vector3.up * 0.10f, Quaternion.identity);

            // Eğer prefab'ta BounceEffect varsa çalıştır
            //var bounce = droppedItem.GetComponent<BounceEffect>();
            //if (bounce != null)
            //    bounce.StartBounce();
        }

        // Kamera sarsıntısı tetikleniyor
        if (cameraShake != null)
            cameraShake.ShakerCamera();

        IsOpened = true; // Tekrar açılmasın
    }
}
