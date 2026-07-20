using UnityEngine;

public class ScreenFade : MonoBehaviour
{
    public Animator animator;
    public void FadeIn()
    {
        animator.Play("FadeIn");
    }

    public void FadeOut()
    {
        animator.Play("FadeOut");
    }
}
