using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostTalking : MonoBehaviour, ITalking
{
    [SerializeField] private QuestSpeakersData _questSpeakers;
    [SerializeField] private GameObject indicator;
    [SerializeField] private SpawnedItem.TypeItem _questType;
    private bool _setQuest;
    private Transform _point;
    private bool _questCompletely;
    private bool _questFinished;
    private void Awake()
    {
        _setQuest = false;
        _questCompletely = false;
        _questFinished = false;
        _point = transform.Find("SpawnPoint");
    }

    private void Start()
    {
        InventorySystem.Instance.giveToAction += CheckQuest;
        UIController.Instance.finishPartOfTalking += FinishQuest;
    }

    public void Talk()
    {
        UIController.Instance.TalkingButton(this);
    }

    public void HideTalk()
    {
        UIController.Instance.HideTalkingButton();
    }

    public void SpeakStuff()
    {
        if (!_questFinished)
        {
            if (_setQuest)
            {
                if (_questCompletely)
                {
                    indicator.SetActive(false);
                    UIController.Instance.StartTalking(_questSpeakers._questCompleteTalk, transform);
                    _questFinished = true;
                    PlayerPrefs.SetInt("Finish", 0);
                    PlayerPrefs.Save();
                }
                else
                {
                    UIController.Instance.StartTalking(_questSpeakers._defaultTalk, transform);
                }
            }
            else
            {
                UIController.Instance.StartTalking(_questSpeakers._firstTalk, transform);
                indicator.SetActive(false);
                _setQuest = true;
            }
        }
    }

    private void CheckQuest(SpawnedItem.TypeItem type, Transform checkPoint)
    {
        if (type.Equals(_questType) && checkPoint.Equals(_point))
        {
            indicator.SetActive(true);
            _questCompletely = true;
        }
    }

    private void FinishQuest(Transform current)
    {
        if (_setQuest && _questCompletely && current.Equals(transform) && _questFinished)
        {
            JokeController.Instance.StartJoke();
        }
    }
}
