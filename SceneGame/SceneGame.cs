using Raylib_cs;
using System.Numerics;
using Projet_S.SceneUtils;
using Projet_S.Services;
using Projet_S.Component.MAP;
using Projet_S.Component.PNJ;
using Projet_S.Component.UI;
using static System.Formats.Asn1.AsnWriter;

namespace Projet_S.SceneGame
{
    public class SceneGame : Scene
    {

        private Grid grid;
        private Snake snake;
        private Apple apple;
        private CubeRandom cubeRandom;

        static IScoreController scoreController = ServicesLocator.Get<IScoreController>();


        private GameTimer gameTimer;
        private GameTimer gameOverTimer;
        private bool isGameOver = false;

        public SceneGame()
        {
            grid = new Grid(11,11);
            snake = new Snake(grid, new (5,5));
            cubeRandom = new CubeRandom(grid, snake, apple);
            apple = new Apple(grid,snake, cubeRandom);

            
        }
         
        public override void Load()
        {
            gameTimer = new GameTimer((float)snake.moveSpeed, OnMoveTimerTriggered);
            gameOverTimer = new GameTimer(2f, () =>
            {
                ServicesLocator.Get<ISceneManager>().Load<SceneGameOver>();
            }, false);
            gameOverTimer.Stop();

            cubeRandom.Load();

            int x = (Program.widthScreen - grid.GridWidth) / 2;
            int y = (Program.heightScreen - grid.GridHeight) / 2;
            grid.position = new Vector2(x, y);
        }

        public override void Draw()
        {
            grid.DrawGrid();
            cubeRandom.Draw();
            apple.Draw();
            snake.Draw();
            if (isGameOver)
                Raylib.DrawText("GAMEOVER", 80, 100, 20, Color.Red);
            else
            {
                Raylib.DrawText($"SCORE {scoreController.GetScore()}", 80, 100, 40, Color.White);
                Raylib.DrawText($"Apparition de bonus :", 20, 150, 35, Color.White);
                Raylib.DrawText($"{cubeRandom.spawnLuck}%", 140, 200, 35, Color.White);
            }
                
        }        

        public override void Update()
        {
            gameOverTimer.Update(Raylib.GetFrameTime());

            if (isGameOver) return;
            snake.ChangeDirection(GetInputsDirection());
            gameTimer.Update(Raylib.GetFrameTime());

            cubeRandom.Update();

            if (Raylib.IsKeyPressed(KeyboardKey.Left)) {
                apple.Spawn();
            }
        }

        public override void Unload()
        {

        }

        public void OnMoveTimerTriggered()
        {
            snake.Move();

            if (snake.IsCollidingWithItself() || snake.IsCollidingWithWall())
            {
                gameOverTimer.Start();
                isGameOver = true;
            }

            if (snake.IsCollidingWithApple(apple))
            {
                apple.Spawn();
                snake.Grow();
                snake.speedUp();
                scoreController.AddScore(10);
                gameTimer.SetDuration((float)snake.moveSpeed); 
                cubeRandom.Spawn();
                cubeRandom.increaseLuck();
            }
            if (snake.IsCollidingWithCubeRandom(cubeRandom))
            {
                cubeRandom.Bonus();
            }
        }

        private Coordinates GetInputsDirection()
        {
            var direction = Coordinates.zero;
            if (Raylib.IsKeyDown(KeyboardKey.W)) direction = Coordinates.top;
            if (Raylib.IsKeyDown(KeyboardKey.S)) direction = Coordinates.bottom;
            if (Raylib.IsKeyDown(KeyboardKey.A)) direction = Coordinates.left;
            if (Raylib.IsKeyDown(KeyboardKey.D)) direction = Coordinates.right;
            return direction;
        }
    }
}
