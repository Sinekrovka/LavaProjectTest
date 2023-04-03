using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class TutorialController : MonoBehaviour
{
    [SerializeField] private Transform _showSources;
    [SerializeField] private Transform _showFabric;
    [SerializeField] private Transform _follow;
    [SerializeField] private GameObject _ghost;

    private Transform _player;
    private bool _tutorialComplete;

    // Start is called before the first frame update
    void Start()
    {
        _player = MovementSystem.Instance.GetPlayer;
        _tutorialComplete = false;
        if (PlayerPrefs.HasKey("Tutorial"))
        {
            SpawnSystem.Instance.spawnItemAction += ShowFabric;
            SpawnSystem.Instance.remove += HideAll;
            StartTutorial();
        }
        if (PlayerPrefs.HasKey("Finish"))
        {
            _ghost.SetActive(false);
            JokeController.Instance.gameObject.SetActive(false);
        }
    }

    private void StartTutorial()
    {
        _follow.transform.position = _player.position;
        StartCoroutine(PlayAnimation(_showSources));
    }

    private void ShowFabric()
    {
        if (!_tutorialComplete)
        {
            StopAllCoroutines();
            _follow.gameObject.SetActive(false);
            _follow.transform.position = _player.position;
            _follow.gameObject.SetActive(true);
            StartCoroutine(PlayAnimation(_showFabric));
        }
    }

    private void HideAll()
    {
        if (!_tutorialComplete)
        {
            _tutorialComplete = true;
            _follow.gameObject.SetActive(false);
            StopAllCoroutines();
        }
    }

    private IEnumerator PlayAnimation(Transform _endPoint)
    {
        yield return new WaitForSeconds(0.5f);
        _follow.DOMove(_endPoint.position, Vector3.Distance(_follow.position, _endPoint.position) / 5f).OnComplete(
            delegate
            {
                _follow.gameObject.SetActive(false);
                _follow.transform.position = _player.transform.position;
                _follow.gameObject.SetActive(true);
                StartCoroutine(PlayAnimation(_endPoint));
            });
    }
}
