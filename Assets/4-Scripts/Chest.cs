using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{
    public bool IsOpened { get; private set; }
    public string ChestID { get; private set; }
    public GameObject itemPrefab; //Item that chest drops
    public SpriteRenderer _spriteRenderer;
    public Sprite openedSprite;

    public void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }
    public void OnTriggerStay2D(Collider2D collider)
    {
        if(collider.gameObject.CompareTag("Player"))
        {
            if(Input.GetKeyDown(KeyCode.E))
            {
                _spriteRenderer.sprite = openedSprite;
                OpenChest();
            }
        }
    }
    //Start is called before  the first frame update
    //void Start()
    //{
    //    ChestID ??= GlobalHelper.GenerateUniqueID(gameObject);
    //}

    //public bool CanInteract()
    //{
    //    return !IsOpened;
    //}

    //public void Interact()
    //{
    //    if (!CanInteract()) return;
    //    OpenChest();
    //}

    private void OpenChest()
    {
        //SetOpened(true);

        //DropItem
        if (itemPrefab)
        {
            GameObject droppedItem = Instantiate(itemPrefab, transform.position + Vector3.up * 0.10f, Quaternion.identity);
            droppedItem.GetComponent<BounceEffect>().StartBounce();
        }
    }
    //public void SetOpened(bool opened)
    //{
    //    if (IsOpened = opened)
    //    {
    //        GetComponent<SpriteRenderer>().sprite = openedSprite;
    //    }
    //}
}

internal class BounceEffect
{
    internal void StartBounce()
    {
        throw new NotImplementedException();
    }
}