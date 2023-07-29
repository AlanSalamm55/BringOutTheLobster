using System;

using UnityEngine;

public class ContainerCounterAnimation : MonoBehaviour
{
    [SerializeField] private ContainerCounter containerCounter;
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        containerCounter.OnPlayerGrabObject += PlayAnimation;
    }

    private void PlayAnimation(object sender, EventArgs e)
    {
        animator.SetTrigger("OpenClose");
    }
}