using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderReload : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform _slider;
    public Slider _actualSlider;
    public GameObject _canvas;
    public float reloadTime;
    public Image _dashIcon;
    public Image _stompIcon;
    public Image _sprite;

    public Sprite _pistol;
    public Sprite _shotGun;
    public Sprite _sword;
    public Sprite _katana;
    void Start()
    {
        _canvas = GameObject.Find("ReloadCanvas");
        _sprite = _canvas.transform.Find("WeaponIcon").gameObject.GetComponent<Image>();
        _dashIcon = _canvas.transform.Find("Dash").gameObject.GetComponent<Image>();
        _stompIcon = _canvas.transform.Find("Stomp").gameObject.GetComponent<Image>();
        _slider = _canvas.transform.Find("ReloadSlider");
        _actualSlider = _slider.GetComponent<Slider>();
        _slider.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerAttack.weaponChosen == 0)
        {
            _sprite.sprite = _pistol;
        }
        if (PlayerAttack.weaponChosen == 1)
        {
            _sprite.sprite = _sword;
        }
        if (PlayerAttack.weaponChosen == 2)
        {
            _sprite.sprite = _shotGun;
        }
        if (PlayerAttack.weaponChosen == 3)
        {
            _sprite.sprite = _katana;
        }
        if (PlayerMovement.canDash)
        {
            _dashIcon.color = new Color(1,1,1,1);
        }
        else
        {
            _dashIcon.color = new Color(1, 1, 1, 0.2f);
        }
        if(PlayerAttack.canStomp)
        {
            _stompIcon.color = new Color(1,1,1,1);
        }
        else
        {
            _stompIcon.color = new Color(1, 1, 1, 0.2f);
        }
        if (reloadTime > 0)
        {
            _actualSlider.value = reloadTime;
            reloadTime -= Time.deltaTime;
            if (reloadTime <= 0)
            {
                _slider.gameObject.SetActive(false);
                PlayerAttack._bulletsLeft = 5;
                PlayerAttack.canFire = true;
                reloadTime = 0;
            }
        }
        if (PlayerAttack._bulletsLeft == 0)
        {
            Debug.Log("yay");
            PlayerAttack.canFire = false;
            _slider.gameObject.SetActive(true);
            reloadTime = 2f;
            PlayerAttack._bulletsLeft = -1;
        }
    }
}
