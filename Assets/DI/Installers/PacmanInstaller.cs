using MovementSpace;
using Zenject;

public class PacmanInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<Movement>().FromComponentInHierarchy().AsSingle();
    }
}