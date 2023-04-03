using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [SerializeField] private Button _talkingButton;
    [SerializeField] private GameObject _counterObject;
    [SerializeField] private RectTransform _dialogField;
    [SerializeField] private TextMeshProUGUI _dialogText;
    [SerializeField] private Button _button;
    [SerializeField] private GameObject _timer;
    [SerializeField] private Transform _timerField;

    private List<string> _currentText;
    private int _indexText;
    private Transform _talker;
    
    public static UIController Instance;
    public Action<Transform> finishPartOfTalking;

    private Dictionary<SpawnedItem.TypeItem, GameObject> _dictionaryItems;

    private void Awake()
    {
        Instance = this;
        _button.onClick.AddListener(delegate { NextTalking();});
        _talkingButton.gameObject.SetActive(false);
        _dialogField.DOAnchorPosY(-350, 0);
        _dictionaryItems = new Dictionary<SpawnedItem.TypeItem, GameObject>();
    }

    private void Start()
    {
        InventorySystem.Instance.updateData += UpdateItem;
        InventorySystem.Instance.addData += AddNewItem;
    }

    private void AddNewItem(SpawnedItem.TypeItem type, Sprite sprite)
    {
        _dictionaryItems.Add(type, Instantiate(_counterObject, _counterObject.transform.parent));
        Transform item = _dictionaryItems[type].transform;
        item.Find("Icon").GetComponent<Image>().sprite = sprite;
        item.Find("Count").GetComponent<TextMeshProUGUI>().text = 1.ToString();
        _dictionaryItems[type].SetActive(true);
    }

    private void UpdateItem(SpawnedItem.TypeItem type, int count)
    {
        if (_dictionaryItems.ContainsKey(type))
        {
            if (count.Equals(0))
            {
                GameObject destroyed = _dictionaryItems[type];
                _dictionaryItems.Remove(type);
                Destroy(destroyed);
            }
            else
            {
                _dictionaryItems[type].transform.Find("Count").GetComponent<TextMeshProUGUI>().text = count.ToString();
            }
        }
    }

    public void TalkingButton(GhostTalking talking)
    {
        _talkingButton.gameObject.SetActive(true);
        _talkingButton.onClick.RemoveAllListeners();
        _dialogText.text = "";
        _talkingButton.onClick.AddListener(delegate { talking.SpeakStuff(); });
    }

    public void StartTalking(List<string> text, Transform cuurentTalking)
    {
        _talkingButton.gameObject.SetActive(false);
        _talker = cuurentTalking;
        _indexText = 0;
        _currentText = text;
        _dialogField.DOAnchorPosY(30, 1f).SetEase(Ease.OutQuad);
        StartCoroutine(TalkingAnimationAction());
    }

    public void NextTalking()
    {
        StopAllCoroutines();
        _indexText++;
        if (_indexText.Equals(_currentText.Count))
        {
            _dialogField.DOAnchorPosY(-350, 1f).SetEase(Ease.OutQuad);
        }
        else
        {
            _dialogText.text = "";
            StartCoroutine(TalkingAnimationAction());
        }
        
    }

    private IEnumerator TalkingAnimationAction()
    {
        foreach (var character in _currentText[_indexText])
        {
            _dialogText.text += character;
            yield return new WaitForSeconds(0.05f);
        }
        finishPartOfTalking?.Invoke(_talker);
        _talker = null;
    }

    public void HideTalkingButton()
    {
        _talkingButton.gameObject.SetActive(false);
    }
}
