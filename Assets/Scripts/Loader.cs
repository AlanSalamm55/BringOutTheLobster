using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class Loader
{
    public enum Scene
    {
        MainMenuScene,
        MainScene,
        LoadingScene,
    }

    private static Scene targetScene;

    public static void Load(Scene targetSceneName)
    {
        targetScene = targetSceneName;
        SceneManager.LoadScene(Scene.LoadingScene.ToString());
    }

    public static void LoadCallback()
    {
        SceneManager.LoadScene(targetScene.ToString());
    }
}