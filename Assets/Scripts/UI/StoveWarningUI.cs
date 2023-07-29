using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoveWarningUI : MonoBehaviour
{
    [SerializeField] private StoveCounter stoveCounter;
    [SerializeField] private float burnShowProgressAmount = 0.5f;


    private void Start()
    {
        stoveCounter.OnProgressChange += StoveCounter_OnProgressChange;
        Hide();
    }

    private void StoveCounter_OnProgressChange(object sender, IHasProgress.OnprogressChangeEventArgs e)
    {
        if (stoveCounter.IsFried() && e.progressNormalized >= burnShowProgressAmount)
        {
            Show();
        }
        else
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