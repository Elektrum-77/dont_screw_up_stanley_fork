using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pause : MonoBehaviour
{
    [SerializeField] private Sprite pause, play;

    private Image image;

    private void Awake()
    {
        image = GetComponent<Image>();
    }

    public void TogglePause()
    {
        var city = FindObjectOfType<City>();
        city.isPause = !city.isPause;
        image.sprite = city.isPause ? play : pause;
    }
}