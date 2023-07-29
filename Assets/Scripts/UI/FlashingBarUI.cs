using System;
using UnityEngine;

public class FlashingBarUI : MonoBehaviour
{
    [SerializeField] private StoveCounter stoveCounter;
    [SerializeField] private float burnShowProgressAmount = 0.5f;

    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        stoveCounter.OnProgressChange += StoveCounter_OnProgressChange;
        animator.SetBool("IsFlashing", false);
    }

    private void StoveCounter_OnProgressChange(object sender, IHasProgress.OnprogressChangeEventArgs e)
    {
        bool show = stoveCounter.IsFried() && e.progressNormalized >= burnShowProgressAmount;
        animator.SetBool("IsFlashing", show);
    }
}