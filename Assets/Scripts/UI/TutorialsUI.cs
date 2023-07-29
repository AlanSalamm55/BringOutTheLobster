using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TutorialsUI : MonoBehaviour
{
    [SerializeField] private GameInput gameInput;

    [SerializeField] private TextMeshProUGUI keyMoveUpText;
    [SerializeField] private TextMeshProUGUI keyMoveDownText;
    [SerializeField] private TextMeshProUGUI keyMoveLeftText;
    [SerializeField] private TextMeshProUGUI keyMoveRightText;
    [SerializeField] private TextMeshProUGUI keyMoveInteractText;
    [SerializeField] private TextMeshProUGUI keyMoveInteractAlternateText;
    [SerializeField] private TextMeshProUGUI keyMovePauseText;

    private void Start()
    {
        UpdateVisual();
        GameManager.Instance.OnStateChanged += GameManager_OnStateChanged;
        Show();
    }

    private void UpdateVisual()
    {
        keyMoveUpText.text = gameInput.GetBindingText(GameInput.Binding.Move_UP);
        keyMoveDownText.text = gameInput.GetBindingText(GameInput.Binding.Move_Down);
        keyMoveRightText.text = gameInput.GetBindingText(GameInput.Binding.Move_Right);
        keyMoveLeftText.text = gameInput.GetBindingText(GameInput.Binding.Move_Left);
        keyMoveInteractText.text = gameInput.GetBindingText(GameInput.Binding.Interact);
        keyMoveInteractAlternateText.text = gameInput.GetBindingText(GameInput.Binding.Alt_Interact);
        keyMovePauseText.text = gameInput.GetBindingText(GameInput.Binding.Pause);
    }

    private void GameManager_OnStateChanged(object sender, EventArgs e)
    {
        if (GameManager.Instance.IsCountDownToStartActive())
        {
            Hide();
        }
    }

    private void Show()
    {
        gameObject.SetActive(true);
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }
}