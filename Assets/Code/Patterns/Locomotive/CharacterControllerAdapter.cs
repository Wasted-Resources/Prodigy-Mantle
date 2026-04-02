using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class CharacterControllerAdapter : MonoBehaviour, IMotionBody
{
    private CharacterController _cc ; 
    public bool IsGrounded => _cc.isGrounded ;

    void Awake() => _cc = GetComponent<CharacterController>();

    public void Move(Vector3 velocity)
    {
       _cc.Move(velocity * Time.deltaTime);
    }
}