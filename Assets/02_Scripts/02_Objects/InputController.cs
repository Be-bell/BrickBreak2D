using System;
using UnityEngine;
using UnityEngine.InputSystem;
/// <summary>
/// Input ������ ó��
/// </summary>
public class InputController : MonoBehaviour
{
    /// <summary>
    /// Start Action ����
    /// </summary>
    public event Action<bool> onStartEvent;

    /// <summary>
    /// Move Action ����
    /// </summary>
    public event Action<float> onMoveEvent;

    /// <summary>
    /// Space�� Input ������, StartEvent Invoke
    /// </summary>
    /// <param name="value">Event�� ���� ��.</param>
    public void NotifyStartEvent(bool value)
    {
        onStartEvent?.Invoke(value);
    }

    /// <summary>
    /// A,D / (Right Arrow, Left Arrow) Ű�� Input ������ MoveEvent Invoke
    /// </summary>
    /// <param name="value">Event�� ���� ��</param>
    public void NotifyMoveEvent(float value)
    {
        onMoveEvent?.Invoke(value);
    }

    /// <summary>
    /// InputSystem���� OnMove ���� �� �ߵ�.
    /// </summary>
    /// <param name="value">(A, Left Arrow) = -1 / (D, Right Arrow) = 1 return</param>
    public void OnMove(InputValue value)
    {
        NotifyMoveEvent(value.Get<float>());
    }
}
