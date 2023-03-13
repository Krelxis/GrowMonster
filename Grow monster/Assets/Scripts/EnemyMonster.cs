using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(GrowMonster))]
[RequireComponent(typeof(NavMeshAgent))]
public class EnemyMonster : MonoBehaviour
{
    private Transform _target;

    private NavMeshAgent _agent;
    private Animator _anim;
    private GrowMonster _grow;

    private void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        _anim = GetComponent<Animator>();
        _grow = GetComponent<GrowMonster>();
    }

    private void Update()
    {
        if (_target == null)
            FindNearestObj();

        if (_target.gameObject.active == false)
            _target = null;

        if (_target == null) { _anim.SetBool("Move", false); return; }

        _agent.SetDestination(_target.transform.position);
        _anim.SetBool("Attack", _grow.BeatsSomething);
        _anim.SetBool("Move", true);
    }

    private void FindNearestObj()
    {
        GameObject[] objects = EnvironmentContainer.Instance.Environments;
        GameObject nearestObject = objects[0];

        for (int i = 0; i < objects.Length; i++)
        {
            if (objects[i].gameObject.active == false)
                continue;

            float dist = Vector3.Distance(transform.position, objects[i].transform.position);
            float lastDist = 0;

            if (i > 1)
                lastDist = Vector3.Distance(transform.position, objects[i - 1].transform.position);

            if (dist < lastDist)
                nearestObject = objects[i];

            if (i == objects.Length - 1)
                _target = nearestObject.transform;
        }

        if (_target == null)
            _target = GameObject.FindObjectOfType<PlayerController>().transform;
    }
}
