using GameManagerSpace;
using GhostSpace;
using PacmanSpace;
using UnityEngine;
using Zenject;

public class SceneInstaller : MonoInstaller
{
    [SerializeField] private GameManager _gameManager;
    
    public override void InstallBindings()
    {
        Container.Bind<GameManager>().FromInstance(_gameManager).AsSingle();
        Container.Bind<Pacman>().FromComponentInHierarchy().AsSingle();
        Container.Bind<Ghost>().FromComponentsInHierarchy().AsCached();
    }
}