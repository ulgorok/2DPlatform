using Unity.VisualScripting;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float speed;
    private float direction;
    private bool hit;
    private float lifetime;
    public Rigidbody2D _rig;
    public GameObject _player;
    public Transform _firePoint;
    public int normDir;
    public int normDirSet;

    //private Animator anim;
    private BoxCollider2D boxCollider;

    private void Awake()
    {
        _player = GameObject.Find("Player");
        _firePoint = _player.transform.Find("FirePoint");
        direction = _firePoint.transform.position.x - _player.transform.position.x;
        // anim = GetComponent<Animator>();
        _rig = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
        if(this.gameObject.name == "Bullet")
        {
            Destroy(this.gameObject, 2);
        }
        else
        {
            Destroy(this.gameObject, 0.1f);
        }
    }
    public void Update()
    {
        if (direction > 0)
        {
            normDir = 1;
        }
        else if (direction < 0)
        {
            normDir = -1;
        }
        _rig.velocity = new Vector2(normDir * speed, 0f);
    }
    public void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Enemy"))
        {
            Destroy(this.gameObject);
        }
    }
}
//    private void Update()
//    {
//        if (hit) return;
//        float movementSpeed = speed * Time.deltaTime * direction;
//        transform.Translate(movementSpeed, 0, 0);

//        lifetime += Time.deltaTime;
//        if (lifetime > 5) gameObject.SetActive(false);
//    }
//    private void OnTriggerEnter2D(Collider2D collision)
//    {
//        hit = true;
//        boxCollider.enabled = false;
//        //anim.SetTrigger("explode");
//    }
//    public void SetDirection(float _direction)
//    {
//        lifetime = 0;
//        direction = _direction;
//        gameObject.SetActive(true);
//        hit = false;
//        boxCollider.enabled = true;

//        //float localScaleX = transform.localScale.x;
//        //if (Mathf.Sign(localScaleX) != _direction)
//        //    localScaleX = -localScaleX;

//        //transform.localScale = new Vector3(localScaleX, transform.localScale.y, transform.localScale.z);
//    }
//    private void Deactivate()
//    {
//        gameObject.SetActive(false);
//    }
//}