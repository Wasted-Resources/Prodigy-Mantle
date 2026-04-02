using UnityEngine;

public interface IMotionBody
{
    void Move(Vector3 velocity);
    bool IsGrounded { get; }
}