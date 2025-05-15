using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoUp : MonoBehaviour
{
    public Rigidbody2D _rig;
    public float delay;

    // Start is called before the first frame update
    void Awake()
    {
        _rig = GetComponent<Rigidbody2D>();
        _rig.AddForce(new Vector2(0, 3f), ForceMode2D.Impulse);
    }

    private void Start()
    {
        Invoke(nameof(EnableCollision), delay);
    }

    private void EnableCollision()
    {
        GetComponent<Collider2D>().enabled = true;
    }

}
