using DefaultNamespace;
using UnityEngine;


public class SelectedCounterVis : MonoBehaviour
{
    
    [SerializeField] private BaseCounter baseCounter;
    [SerializeField] private Transform[] visualObjectTransformArray;
    // instead of Transform GameObject ref also works and when calling the function is more direct 
    // VisualGameObject.SetActive(bool)

    public void Start()
    {
        Player.Instance.OnSelectedCounterVis += Player_OnSelectedCounterVis;
    }


    private void Player_OnSelectedCounterVis(object sender, Player.OnSelectedCounterVisEventArgs e)
    {
        if (e.selectedCounter == baseCounter)
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
        foreach (var visualObjectTransform in visualObjectTransformArray)
        {
            visualObjectTransform.gameObject.SetActive(true);
        }
    }

    private void Hide()
    {
        foreach (var visualObjectTransform in visualObjectTransformArray)
        {
            visualObjectTransform.gameObject.SetActive(false);
        }
    }
}