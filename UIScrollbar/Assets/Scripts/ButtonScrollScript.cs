using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonScrollScript : MonoBehaviour
{
    [SerializeField]
    private Button _rightButton;
    [SerializeField]
    private Button _leftButton;
    [SerializeField]
    private ScrollRect _productScroll;
    [SerializeField]
    private float _oneSnapsize = 0.0625f;
    [SerializeField]
    private float _snapTime = 0.3f;
    private void OnEnable() {
        _rightButton.onClick.AddListener(RightScroll);
        _leftButton.onClick.AddListener(LeftScroll);

    }

    private void RightScroll() {
        StartCoroutine(Scroll(true));
    }

    private void LeftScroll() {
        StartCoroutine(Scroll(false));
    }

    IEnumerator Scroll(bool isRight) {
        float change = 0.0f;
        var newHorizontalNormPos = _productScroll.horizontalNormalizedPosition + _oneSnapsize * (isRight == true ? 1 : -1);
        while (change <= _snapTime) {
            yield return null;
            change += Time.deltaTime;
            _productScroll.horizontalNormalizedPosition = Mathf.Lerp(_productScroll.horizontalNormalizedPosition, newHorizontalNormPos, change / _snapTime);
        }
    }
}
