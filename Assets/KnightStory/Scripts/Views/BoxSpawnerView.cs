using UnityEngine;

public class BoxSpawnerView : MonoBehaviour
{
    [SerializeField]
    private GameObject _boxPrefab;

    [SerializeField]
    private Transform _boxSpawner;

    private float _spawnTime = 0;
    private float _delay = 0.7f;
    private float _destoryTime = 2.5f;

    void FixedUpdate()
    {
        SpawnBox();
    }

    private void SpawnBox()
    {
        if (_spawnTime > Time.deltaTime)
        {
            _spawnTime -= Time.deltaTime;
        } else
        {
            _spawnTime = _delay;
            var spawnPos = new Vector3(_boxSpawner.position.x + 0.7f, _boxSpawner.position.y - 1f, 0);
            var box = Instantiate(_boxPrefab, spawnPos, Quaternion.identity);
            Destroy(box, _destoryTime);
        }

    }
}
