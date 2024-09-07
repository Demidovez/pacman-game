using GameManagerSpace;
using GhostSpace;
using PacmanSpace;
using UnityEngine;
using Zenject;

public class SceneInstaller : MonoInstaller
{
    [SerializeField] private GameManager _gameManager;
    [SerializeField] private GameObject _pacmanPrefab;
    [SerializeField] private Transform _startPoint;
    
    public override void InstallBindings()
    {
        Container.Bind<GameManager>().FromInstance(_gameManager).AsSingle();
        Container.Bind<Ghost>().FromComponentsInHierarchy().AsCached();

        BindPacman();
    }

    private void BindPacman()
    {
        Pacman pacman =
            Container.InstantiatePrefabForComponent<Pacman>(_pacmanPrefab, _startPoint.position, Quaternion.identity,
                null);

        Container.Bind<Pacman>().FromInstance(pacman).AsSingle();
    }
}