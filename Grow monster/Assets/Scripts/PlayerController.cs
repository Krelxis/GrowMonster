using UnityEngine;
 
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(GrowMonster))]

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float _moveSpeed;
    private float _horSpeed;
    private float _verSpeed;

    [SerializeField] private float _rotateSpeed;

    [SerializeField] private GameObject _canvas;

    public Transform CamPos;

    private State _state = State.Idle;

    private CameraController _cam;
    private VariableJoystick _joystick;
    private Rigidbody _rb;
    private Animator _anim;
    private GrowMonster _grow;

    public void SpawnCanvas()
    {
        _canvas.SetActive(true);
    }

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _anim = GetComponent<Animator>();
        _grow = GetComponent<GrowMonster>();
        _joystick = GameObject.FindObjectOfType<VariableJoystick>();
        _cam = GameObject.Find("CameraContainer").GetComponent<CameraController>();
    }

    private void FixedUpdate()
    {
        Vector3 direction = (Vector3.forward * _joystick.Vertical + Vector3.right * _joystick.Horizontal).normalized;
        _cam.OffsetMultiple = transform.localScale.x;

        if (_joystick.Vertical != 0 || _joystick.Horizontal != 0) { _state = State.Move; }
        else { _state = State.Idle; }

        if (_state == State.Idle) { Idle(); }
        if (_state == State.Move) { Rotate(direction); Move(direction); }
        _anim.SetBool("Attack", _grow.BeatsSomething);
    }

    private void Idle()
    {
        _anim.SetBool("Move", false);
    }

    private void Move(Vector3 dir)
    {
        _rb.velocity = dir * _moveSpeed;
        _anim.SetBool("Move", true);
    }

    private void Rotate(Vector3 dir)
    {
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(dir.x, 0, dir.z));
        transform.rotation = Quaternion.Lerp(transform.rotation, lookRotation, Time.deltaTime * _rotateSpeed);
    } 

    enum State
    {
        Idle, 
        Move
    }
}
