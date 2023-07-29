using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class MainMenuUI : MonoBehaviour
{
    [SerializeField] private Button playBtn;
    [SerializeField] private Button quitBtn;

    private void Awake()
    {
        playBtn.onClick.AddListener(() => { Loader.Load(Loader.Scene.MainScene); });

        quitBtn.onClick.AddListener(() => { Application.Quit(); });
        Time.timeScale = 1f;
    }

    private void Start()
    {
        playBtn.Select();
    }
}