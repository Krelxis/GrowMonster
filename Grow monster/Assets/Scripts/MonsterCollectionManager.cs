using UnityEngine;

public class MonsterCollectionManager : MonoBehaviour
{
    [SerializeField] private PlayerController[] _monsters = new PlayerController[2];
    [SerializeField] private PlayerController _selectedMonster;
    private int _selectedMonsterNum;

    [SerializeField] private Transform _spawnPos;
    [SerializeField] private CameraController _cam;

    private void Start()
    {
        SpawnMonster(0);
        _cam.ChangeTarget(_selectedMonster.CamPos);
    }

    public void StartGame()
    {
        _selectedMonster.SpawnCanvas();
    }

    public void SelectNext()
    {
        if (_selectedMonsterNum >= _monsters.Length - 1) { return; }

        _selectedMonsterNum++;
        SpawnMonster(_selectedMonsterNum);
        _cam.ChangeTarget(_selectedMonster.CamPos);
    } 

    public void SelectPast()
    {
        if (_selectedMonsterNum <= 0) { return; }

        _selectedMonsterNum--;
        SpawnMonster(_selectedMonsterNum);
        _cam.ChangeTarget(_selectedMonster.CamPos);
    }

    private void SpawnMonster(int monsterNum)
    {
        if (_selectedMonster != null) { Destroy(_selectedMonster.gameObject); }

        PlayerController clone = Instantiate(_monsters[monsterNum], _spawnPos.position, _spawnPos.rotation);
        _selectedMonster = clone;
    }
}
