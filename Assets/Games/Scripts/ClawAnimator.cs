using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClawAnimator : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private Rope rope;
    private void Start()
    {
        animator = GetComponent<Animator>();
        rope.OnClawGrabChange += GrabChangeHandler;
    }
    private void GrabChangeHandler(bool isGrabbing)
    {
        animator.SetBool("isGrabbing", isGrabbing);
    }
}
