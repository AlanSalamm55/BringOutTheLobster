using System;
using DefaultNamespace;
using UnityEngine;

public class CuttingCounterAnimation : MonoBehaviour
{
    [SerializeField] private CuttingCounter cuttingCounter;
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        cuttingCounter.OnCutting += cuttingCounter_OnCutting;
    }

    private void cuttingCounter_OnCutting(object sender, EventArgs e)
    {
        animator.SetTrigger("Cut");
    }
}