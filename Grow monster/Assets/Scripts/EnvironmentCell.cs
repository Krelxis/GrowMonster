using UnityEngine;

public class EnvironmentCell : MonoBehaviour
{
    [SerializeField] private float _reductionRate;
    [SerializeField] private float _destroySize;

    private void Update()
    {
        transform.localScale = Vector3.Lerp(transform.localScale, Vector3.zero, _reductionRate * Time.deltaTime);
        if (transform.localScale.x <= _destroySize) { gameObject.SetActive(false); }
    }
}
