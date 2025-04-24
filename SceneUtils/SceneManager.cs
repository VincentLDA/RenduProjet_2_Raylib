namespace Projet_S.SceneUtils
{
   

    public class SceneManager : ISceneManager
    {
        private Scene? _currentScene ;

        public SceneManager()
        {
            ServicesLocator.Register<ISceneManager>(this);
        }

        public void Load<T>() where T : Scene, new()
        {
            if( _currentScene != null ) _currentScene.Unload();

            _currentScene = new T();
            _currentScene.Load();

        }

        public void Update() => _currentScene?.Update();
        public void Draw() => _currentScene?.Draw();

    }
}
