using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIAnimation : MonoBehaviour
{
    public List<Sprite> Sprites;
    private int _animationIndex;
    private Image _image;

    private void OnEnable()
    {
        _image = GetComponent<Image>();
        _animationIndex = 0;
        StartCoroutine(Animation());
    }

    private void OnDisable()
    {
        StopCoroutine(Animation());
    }

    private IEnumerator Animation()
    {
        while (true)
        {
            _image.sprite = Sprites[_animationIndex];
            yield return new WaitForSeconds(0.5f);
            _animationIndex++;
            if (_animationIndex >= Sprites.Count)
            {
                _animationIndex = 0;
            }
        }
    }
}