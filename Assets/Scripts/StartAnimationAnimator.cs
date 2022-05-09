using UnityEngine;
using UnityEngine.Events;

public class StartAnimationAnimator : MonoBehaviour
{
    public Animator animator;
    private bool _isAnimationEnded = false;
    public UnityAction StarAnimationeEnd;

    private void Update()
    {
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Waiter") && !_isAnimationEnded)
        {
            _isAnimationEnded = true;
            StartAnimationEnded();
        }
    }

    private void StartAnimationEnded()
    {
        StarAnimationeEnd?.Invoke();
    }

    public void Play()
    {
        animator.Play(0);
    }
    public void PlayGoodEnd()
    {
        animator.speed = 1f;
        animator.Play("GoodEnd");
    }
    public void PlayBadEnd()
    {
        animator.speed = 1f;
        animator.Play("BadEnd");

        for (int i = 0; i < animator.layerCount; i++)
        {
            animator.Play("BadEnd", i);
        }
    }
}
