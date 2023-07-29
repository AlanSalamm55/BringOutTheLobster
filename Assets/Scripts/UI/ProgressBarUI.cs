using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBarUI : MonoBehaviour
{
    private IHasProgress hasProgress;
    [SerializeField] private Image barImage;
    [SerializeField] private GameObject hasProgressGameObject;

    private void Start()
    {
        hasProgress = hasProgressGameObject.GetComponent<IHasProgress>();
        if (hasProgress == null) 
        {
            Debug.LogError("Game Object:"+ hasProgressGameObject+"Doesnt have a component that doesnt Implement IHasProgres");
        }
        hasProgress.OnProgressChange += HasProgress_OnProgressChange;
        barImage.fillAmount = 0f;
        Hide();
    }

    private void HasProgress_OnProgressChange(object sender, IHasProgress.OnprogressChangeEventArgs e)
    {
        barImage.fillAmount = e.progressNormalized;
        if (e.progressNormalized == 0f || e.progressNormalized == 1.0f)
        {
            Hide();
        }
        else
        {
            Show();
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