using UnityEngine;

public class Monster : MonoBehaviour
{
    public float Speed;
    public float RotateSpeed;

    public Rigidbody Rb;
    public Animator anim;
    public GrowMonster Grow; 

    public virtual void Init()
    {
        Rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        Grow = GetComponent<GrowMonster>();
    }
}
