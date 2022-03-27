using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField]
    private Camera _camera;

    [SerializeField]
    private SpriteRenderer _background;

    [SerializeField]
    private TurretView _turretView;

    [SerializeField]
    private PlayerView _playerView;

    [SerializeField]
    private AnimSpriteData _animSpriteData;

    [SerializeField]
    private AiConfig _config;

    [SerializeField]
    private EnemyView _enemyView;

    private ParalaxManager _paralaxManager;
    private SpritesAnimator _spritesAnimator;
    private PlayerController _playerController;
    private TurretController _turretController;
    private SimplePatrolAiController _simplePatrolAi;

    private void Start()
    {
        _paralaxManager = new ParalaxManager(_camera, _background.transform);
        _spritesAnimator = new SpritesAnimator(_animSpriteData);
        _playerController = new PlayerController(_playerView);
        _turretController = new TurretController(_turretView, _playerView.transform);
        _simplePatrolAi = new SimplePatrolAiController(_enemyView, _config);
    }

    private void Update()
    {
        _paralaxManager.Update();
        _spritesAnimator.Update();
        _playerController.Update();
        _turretController.Update();
    }

    private void FixedUpdate()
    {
        _playerController.FixedUpdate();
        _turretController.FixedUpdate();
        _simplePatrolAi.FixedUpdate();
    }
}
