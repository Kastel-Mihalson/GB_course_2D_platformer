using System.Collections.Generic;
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

    private ParalaxManager _paralaxManager;
    private SpritesAnimator _spritesAnimator;
    private PlayerController _playerController;
    private TurretController _turretController;

    private void Start()
    {
        _paralaxManager = new ParalaxManager(_camera, _background.transform);
        _spritesAnimator = new SpritesAnimator(_animSpriteData);
        _playerController = new PlayerController(_playerView);
        _turretController = new TurretController(_turretView, _playerView.transform);
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
    }
}
