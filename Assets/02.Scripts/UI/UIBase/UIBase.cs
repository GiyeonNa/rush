using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;
using UnityEngine.U2D;
using DG.Tweening;

public class UIBase : MonoBehaviour
{
    //���� Ȯ��
    public virtual bool isOpen
    {
        get
        {
            if (gameObject == null)
                return false;

            return gameObject.activeSelf;
        }
    }

    //����
    public virtual void Open()
    {
        gameObject.SetActive(true);
    }

    //�ݱ�
    public virtual void Close()
    {
        gameObject.SetActive(false);

    }

    //update�� ����
    public virtual void UpdateLogic()
    {
    }

    #region Button
    //button�� �̺�Ʈ �Ҵ�
    public void SetBtn(Button _btn, UnityEngine.Events.UnityAction _click, bool _isSound = true)
    {
        if (_btn == null) return;

        _btn.onClick.RemoveAllListeners();
        _btn.onClick.AddListener(() =>
        {
            if (_isSound)
                SoundManager.Instance?.PlayButtonPopupSound();

            _btn.transform.DOScale(0.9f, 0.1f).SetEase(Ease.OutQuad).OnComplete(() =>
            {
                _btn.transform.DOScale(1f, 0.1f).SetEase(Ease.OutQuad);
                _click.Invoke();
            });
        });
    }

    //��ư ��ȣ�ۿ� ON,OFF
    public void SetBtnEnable(Button _btn, bool _enable)
    {
        if (null == _btn)
            return;

        _btn.interactable = _enable;
    }
    //��ư �� ����
    public void SetColor(Button _btn, Color _color)
    {
        if (null == _btn)
            return;

        _btn.image.color = _color;
    }
    #endregion

    #region Toggle
    //Toggle�� �̺�Ʈ �߰�
    public void SetToggle(Toggle _toggle, UnityEngine.Events.UnityAction<bool> _change)
    {
        if (null == _toggle)
            return;

        _toggle.onValueChanged.AddListener(_change);
    }

    //��� ��ȣ�ۿ� ON,OFF
    public void SetToggleEnable(Toggle _toggle, bool _enable)
    {
        if (null == _toggle)
            return;

        _toggle.interactable = _enable;
    }
    //��۰��� On,Off ���� ����
    public void SetToggleisOn(Toggle _toggle, bool _isOn)
    {
        if (null == _toggle)
            return;

        _toggle.isOn = _isOn;
    }
    #endregion

    #region Text
    //�ؽ�Ʈ ����
    public void SetText(Text _text, string _dest)
    {
        if (null == _text)
            return;

        if (string.IsNullOrEmpty(_dest) == true)
        {
            _text.gameObject.SetActive(false);
            return;
        }

        _text.gameObject.SetActive(true);
        _text.text = _dest;
    }

    //�ؽ�Ʈ����, TMP�� ����
    public void SetText(TextMeshProUGUI _text, string _dest)
    {
        if (null == _text)
            return;

        if (string.IsNullOrEmpty(_dest) == true)
        {
            _text.gameObject.SetActive(false);
            return;
        }

        _text.gameObject.SetActive(true);
        _text.text = _dest;
    }
    #endregion

    #region Slider
    //Slider ���� �� �̺�Ʈ �߰�
    public void SetSlider(Slider _slider, UnityEngine.Events.UnityAction<float> _value)
    {
        if (null == _slider)
            return;

        _slider.onValueChanged.AddListener(_value);
    }

    //Slider �� ����
    public void SetSliderValue(Slider _slider, float _value)
    {
        if (null == _slider)
            return;

        _slider.value = _value;
    }
    #endregion

    #region Image
    //�̹��� ����, ������ ��Ʋ�� �̹��� ��θ� �����ϴ� ����
    public void SetIcon(SpriteRenderer _icon, string _path)
    {
        if (null == _icon)
        {
            Debug.LogWarning($"{_icon} is Null");
            return;
        }

        if (_path == null || _path.Length <= 0)
        {
            _icon.gameObject.SetActive(false);
            return;
        }

        _icon.gameObject.SetActive(true);
        SpriteAtlas spriteAtlas = null;
        _icon.sprite = spriteAtlas.GetSprite(_path);
        //_icon.sprite = SpritePool.GetSprite(_path);
    }

    public void SetIcon(Image _icon, Sprite _sprite)
    {
        if (null == _icon)
            return;
        _icon.gameObject.SetActive(true);
        _icon.sprite = _sprite;
    }

    public void SetIcon(Image _icon, string _path)
    {
        if (null == _icon)
            return;

        if (_path == null || _path.Length <= 0)
        {
            _icon.gameObject.SetActive(false);
            return;
        }

        _icon.gameObject.SetActive(true);
        //_icon.sprite = SpritePool.GetSprite(_path);
    }

    //ä�� ����
    public void SetFillAmount(Image _img, float _amount)
    {
        if (null == _img)
            return;

        _img.gameObject.SetActive(true);
        _img.fillAmount = _amount;
    }

    //���� ����
    public void SetColor(MaskableGraphic _img, Color _color)
    {
        if (null == _img)
            return;

        _img.color = _color;
    }

    //���� ����
    public void SetAlpha(MaskableGraphic _img, float _alpha)
    {
        if (null == _img)
            return;

        Color _color = _img.color;
        _color.a = _alpha;
        _img.color = _color;
    }

    #endregion

    #region ON,OFF
    public void SetActive(GameObject _img, bool _isActive)
    {
        if (null == _img)
            return;

        _img.SetActive(_isActive);
    }

    public void SetActive(Slider _slider, bool _isActive)
    {
        if (null == _slider)
            return;

        SetActive(_slider.gameObject, _isActive);
    }

    public void SetActive(GameObject[] _img, bool _isActive)
    {
        if (null == _img)
            return;

        for (int i = 0; i < _img.Length; ++i)
        {
            SetActive(_img[i], _isActive);
        }
    }

    public void SetActive(Toggle _toggle, bool _isActive)
    {
        if (null == _toggle)
            return;

        _toggle.gameObject.SetActive(_isActive);
    }

    public void SetActive(Image _img, bool _isActive)
    {
        if (null == _img)
            return;

        _img.gameObject.SetActive(_isActive);
    }

    public void SetActive(Text _text, bool _isActive)
    {
        if (null == _text)
            return;

        _text.gameObject.SetActive(_isActive);
    }

    public void SetActive(TextMeshProUGUI _text, bool _isActive)
    {
        if (null == _text)
            return;

        _text.gameObject.SetActive(_isActive);
    }


    public void SetActive(Button _btn, bool _isActive)
    {
        if (null == _btn)
            return;

        _btn.gameObject.SetActive(_isActive);
    }

    public void SetActive(RectTransform _rt, bool _isActive)
    {
        if (null == _rt)
            return;

        _rt.gameObject.SetActive(_isActive);
    }
    #endregion

    public virtual void ResetData()
    {

    }

    public virtual void Hide()
    {
        transform.position = new Vector3(-2000f, 0, 0);
    }

    public virtual void Show()
    {
        transform.position = Vector3.zero;
    }

    //��α�� ����
    public T Create<T>(string path, Transform parent = null) where T : Component
    {
        GameObject _res = Resources.Load<GameObject>(path);
        if (_res == null)
            return null;

        GameObject _obj = Instantiate<GameObject>(_res);
        SetAttach(_obj.transform, parent);
        T _comp = _obj.GetComponent<T>();
        return _comp;

    }

    //������Ʈ�� �޾Ƽ� ����
    public T Create<T>(GameObject _res, Transform parent = null) where T : Component
    {
        if (_res == null)
            return null;

        GameObject _obj = Instantiate<GameObject>(_res);
        SetAttach(_obj.transform, parent);
        T _comp = _obj.GetComponent<T>();
        return _comp;

    }

    //��ġ ����
    public void SetAttach(Transform _self, Transform _parent)
    {
        if (null == _parent)
            return;

        _self.transform.SetParent(_parent);
        _self.transform.localPosition = Vector3.zero;
        _self.transform.localRotation = Quaternion.identity;
        _self.transform.localScale = Vector3.one;
    }
}
