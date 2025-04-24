
namespace Projet_S.SceneUtils
{
    public interface ISceneManager
    {
        void Load<T>() where T : Scene, new();
    }
}
