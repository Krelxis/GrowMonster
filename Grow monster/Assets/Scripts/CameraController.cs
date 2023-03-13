using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private GameObject _mainCamera;
    [SerializeField] private Transform _target;
    [SerializeField] private Vector3 _offset;
    private Vector3 _offsetSecond;

    [HideInInspector] public float OffsetMultiple;

    private void Start()
    {
        _offsetSecond = _offset;
        OffsetMultiple = 1;
    }

    private void FixedUpdate()
    {
        transform.LookAt(_target);

        _offset = Vector3.Lerp(_offset, _offsetSecond * OffsetMultiple, 1 * Time.deltaTime);
        transform.position = new Vector3(_target.transform.position.x, _target.transform.position.y, _target.transform.position.z) + _offset;
    }

    public void ChangeTarget(Transform newTarget)
    {
        _target = newTarget;
    }
}
