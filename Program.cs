using Raylib_cs;
using Projet_S.SceneGame;
using Projet_S.SceneUtils;
using System.ComponentModel;
using Projet_S.Component.UI;
using Projet_S.Component.MAP;
using System.Numerics;

class Program
{

    static public SceneManager _sceneManager = new();
    static private Score score = new();
    static private HightScore hg = new();
    public static int widthScreen = 1500;
    public static int heightScreen = 900;

    public static Vector2 mousePoint { get; private set; } = new Vector2(0.0f, 0.0f);

    static void Main()
    {

        Raylib.InitWindow(widthScreen, heightScreen, "Snake");
        Raylib.SetTargetFPS(60);


        _sceneManager.Load<SceneHome>();
        ServicesLocator.Register<SceneManager>(_sceneManager);


        while (!Raylib.WindowShouldClose()) {
            mousePoint = Raylib.GetMousePosition();

            _sceneManager.Update();

            Raylib.BeginDrawing();
            Raylib.ClearBackground(Color.Black);


            _sceneManager.Draw();

            Raylib.EndDrawing();            
        }
    }
}