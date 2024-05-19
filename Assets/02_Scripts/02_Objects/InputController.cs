using System;
using UnityEngine;
using UnityEngine.InputSystem;
/// <summary>
/// Input 데이터 처리
/// </summary>
public class InputController : MonoBehaviour
{
    /// <summary>
    /// Start Action 모음
    /// </summary>
    public event Action<bool> onStartEvent;

    /// <summary>
    /// Move Action 모음
    /// </summary>
    public event Action<float> onMoveEvent;

    /// <summary>
    /// Space값 Input 받으면, StartEvent Invoke
    /// </summary>
    /// <param name="value">Event에 보낼 값.</param>
    public void NotifyStartEvent(bool value)
    {
        onStartEvent?.Invoke(value);
    }

    /// <summary>
    /// A,D / (Right Arrow, Left Arrow) 키값 Input 받으면 MoveEvent Invoke
    /// </summary>
    /// <param name="value">Event에 보낼 값</param>
    public void NotifyMoveEvent(float value)
    {
        onMoveEvent?.Invoke(value);
    }

    /// <summary>
    /// InputSystem에서 OnMove 실행 시 발동.
    /// </summary>
    /// <param name="value">(A, Left Arrow) = -1 / (D, Right Arrow) = 1 return</param>
    public void OnMove(InputValue value)
    {
        NotifyMoveEvent(value.Get<float>());
    }
}
