using UnityEngine;

/// <summary>
/// Class responsible of destroying the object after an animation 
/// is finished
/// </summary>
public class DestroyOnAnimationEnd : StateMachineBehaviour
{
    private bool _startTimer;
    private float _animationLength;
    private float _currentTime;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
    {
        StartTimer(animatorStateInfo.length);
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
    {
        if (!_startTimer)
        {
            return;
        }
        _currentTime += Time.deltaTime;
        if (_animationLength <= _currentTime)
        {
            Destroy(animator.gameObject);
        }
    }

    /// <summary>
    /// Starts timer
    /// </summary>
    /// <param name="animationLength">
    /// Length of the animation
    /// <para/>
    /// After this amount of time, the GO is destroyed
    /// </param>
    private void StartTimer(float animationLength)
    {
        _startTimer = true;
        _animationLength = animationLength;
        _currentTime = 0f;
    }

}
