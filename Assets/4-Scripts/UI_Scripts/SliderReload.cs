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
    void Start()
    {
        _canvas = GameObject.Find("ReloadCanvas");
        _slider = _canvas.transform.Find("ReloadSlider");
        _actualSlider = _slider.GetComponent<Slider>();
        _canvas.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (reloadTime > 0)
        {
            _actualSlider.value = reloadTime;
            reloadTime -= Time.deltaTime;
            if (reloadTime <= 0)
            {
                _canvas.gameObject.SetActive(false);
                PlayerAttack._bulletsLeft = 5;
                PlayerAttack.canFire = true;
                reloadTime = 0;
            }
        }
        if (PlayerAttack._bulletsLeft == 0)
        {
            Debug.Log("yay");
            PlayerAttack.canFire = false;
            _canvas.gameObject.SetActive(true);
            reloadTime = 2f;
            PlayerAttack._bulletsLeft = -1;
        }
    }
}
