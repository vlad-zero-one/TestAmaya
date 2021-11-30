using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Restarter : MonoBehaviour
{
    [SerializeField] private GameObject _restartButton;
    [SerializeField] private Image _blackImage;
    [SerializeField] private LevelController _levelController;

    public void ShowButton()
    {
        _restartButton.SetActive(true);
        _blackImage.enabled = true;
        _blackImage.DOFade(0.7f, 3);
    }

    public void Restart()
    {
        _blackImage.DOKill();
        _blackImage.DOFade(1, 1);

        _levelController.SceneInitAndLoaded += EndLoadingScreen;
        _levelController.Init();
    }

    public void EndLoadingScreen()
    {
        _blackImage.DOKill();
        _blackImage.DOFade(0, 0);
    }
}
