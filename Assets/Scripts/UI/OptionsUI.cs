using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OptionsUI : MonoBehaviour
{
    [SerializeField] private GameInput gameInput;
    private Action onCloseButtonAction;
    [SerializeField] private Button soundEffectBtn;
    [SerializeField] private Button musicBtn;
    [SerializeField] private Button closeBtn;

    [SerializeField] private Button moveUpBtn;
    [SerializeField] private Button moveDownBtn;
    [SerializeField] private Button moveLeftBtn;
    [SerializeField] private Button moveRightBtn;
    [SerializeField] private Button interactBtn;
    [SerializeField] private Button interactAltBtn;
    [SerializeField] private Button pauseBtn;

    [SerializeField] private TextMeshProUGUI soundEffectText;
    [SerializeField] private TextMeshProUGUI musicText;

    [SerializeField] private TextMeshProUGUI moveUpText;
    [SerializeField] private TextMeshProUGUI moveDownText;
    [SerializeField] private TextMeshProUGUI moveLeftText;
    [SerializeField] private TextMeshProUGUI moveRightText;
    [SerializeField] private TextMeshProUGUI interactText;
    [SerializeField] private TextMeshProUGUI interactAltText;
    [SerializeField] private TextMeshProUGUI pauseText;
    [SerializeField] private Transform pressToRebindPopUpTransform;


    private void Awake()
    {
        soundEffectBtn.onClick.AddListener(() =>
        {
            SoundManager.Instance.ChangeVolume();
            UpdateVisual();
        });
        musicBtn.onClick.AddListener(() =>
        {
            MusicManager.Instance.ChangeVolume();
            UpdateVisual();
        });

        closeBtn.onClick.AddListener(() =>
        {
            Hide();
            onCloseButtonAction();
        });

        moveUpBtn.onClick.AddListener(() => { RebindBinding(GameInput.Binding.Move_UP); });
        moveDownBtn.onClick.AddListener(() => { RebindBinding(GameInput.Binding.Move_Down); });
        moveRightBtn.onClick.AddListener(() => { RebindBinding(GameInput.Binding.Move_Right); });
        moveLeftBtn.onClick.AddListener(() => { RebindBinding(GameInput.Binding.Move_Left); });
        interactBtn.onClick.AddListener(() => { RebindBinding(GameInput.Binding.Interact); });
        interactAltBtn.onClick.AddListener(() => { RebindBinding(GameInput.Binding.Alt_Interact); });
        pauseBtn.onClick.AddListener(() => { RebindBinding(GameInput.Binding.Pause); });
    }

    private void Start()
    {
        GameManager.Instance.OnGameUnPause += GameManager_OnGameUnPause;
        UpdateVisual();

        Hide();
        HidePressToRebindKeyPopup();
    }

    private void GameManager_OnGameUnPause(object sender, EventArgs e)
    {
        Hide();
    }


    private void UpdateVisual()
    {
        soundEffectText.text = "Sound Effects:" + Mathf.Round(SoundManager.Instance.GetVolume() * 10f);
        musicText.text = "Music:" + Mathf.Round(MusicManager.Instance.GetVolume() * 10f);

        moveUpText.text = gameInput.GetBindingText(GameInput.Binding.Move_UP);
        moveDownText.text = gameInput.GetBindingText(GameInput.Binding.Move_Down);
        moveRightText.text = gameInput.GetBindingText(GameInput.Binding.Move_Right);
        moveLeftText.text = gameInput.GetBindingText(GameInput.Binding.Move_Left);
        interactText.text = gameInput.GetBindingText(GameInput.Binding.Interact);
        interactAltText.text = gameInput.GetBindingText(GameInput.Binding.Alt_Interact);
        pauseText.text = gameInput.GetBindingText(GameInput.Binding.Pause);
    }

    public void Show(Action onCloseButtonAction)
    {
        this.onCloseButtonAction = onCloseButtonAction;

        gameObject.SetActive(true);
        soundEffectBtn.Select();
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }

    private void ShowPressToRebindKeyPopup()
    {
        pressToRebindPopUpTransform.gameObject.SetActive(true);
    }

    private void HidePressToRebindKeyPopup()
    {
        pressToRebindPopUpTransform.gameObject.SetActive(false);
    }

    private void RebindBinding(GameInput.Binding binding)
    {
        ShowPressToRebindKeyPopup();
        gameInput.RebindBinding(binding, () =>
        {
            HidePressToRebindKeyPopup();
            UpdateVisual();
        });
    }
}