using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PausedGameUI : MonoBehaviour
{
    [SerializeField] private Button resumeBtn;
    [SerializeField] private Button mainMenuBtn;
    [SerializeField] private Button optionsBn;
    [SerializeField] private OptionsUI optionsUI;


    private void Start()
    {
        Hide();

        GameManager.Instance.OnGamePause += GameManager_OnGamePause;
        GameManager.Instance.OnGameUnPause += GameManager_OnGameUnPause;

        resumeBtn.onClick.AddListener(() => { GameManager.Instance.TogglePauseGame(); });
        mainMenuBtn.onClick.AddListener(() => { Loader.Load(Loader.Scene.MainMenuScene); });
        optionsBn.onClick.AddListener(() =>
        {
            Hide();
            optionsUI.Show(Show);
        });
    }


    private void GameManager_OnGamePause(object sender, System.EventArgs e)
    {
        Show();
    }

    private void GameManager_OnGameUnPause(object sender, System.EventArgs e)
    {
        Hide();
    }

    private void Show()
    {
        gameObject.SetActive(true);
        resumeBtn.Select();
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }
}