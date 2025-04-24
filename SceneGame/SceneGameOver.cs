using System.Numerics;
using Projet_S.Component.UI;
using Projet_S.SceneUtils;
using Raylib_cs;

namespace Projet_S.SceneGame
{
    public class SceneGameOver : Scene
    {

        private Texture2D button = Raylib.LoadTexture("assets/ui/Circle_Buttons/Circle_Blue_Button_Normal.png");
        private Texture2D buttonOver = Raylib.LoadTexture("assets/ui/Circle_Buttons/Circle_Blue_Button_Pressed.png");
        private Texture2D btnIcons = Raylib.LoadTexture("assets/ui/Icons/Refresh.png");

        static IScoreController scoreController = ServicesLocator.Get<IScoreController>();

        private string GameOver_TXT = "GAME OVER";
        private int fontSize = 40;
        private int Spacing = 10;
        private string Score_TXT = "Score : ";

        private ButtonTexture btnRestart;

        public SceneGameOver()
        {
            btnRestart = new ButtonTexture(restart, button, buttonOver, btnIcons);
        }

        public override void Draw()
        {
            int x = Program.widthScreen / 2;
            int y = Program.heightScreen / 2;

            Vector2 txtGameOverSize = Raylib.MeasureTextEx(Raylib.GetFontDefault(), GameOver_TXT, fontSize, 1);

            int Y_GameOver = (int)(y - txtGameOverSize.Y / 2);
            Raylib.DrawText(GameOver_TXT, (int)(x - txtGameOverSize.X / 2), Y_GameOver, fontSize, Color.Red);


            string score = Score_TXT + scoreController.GetScore().ToString();
            Vector2 txtScoreSize = Raylib.MeasureTextEx(Raylib.GetFontDefault(), score, fontSize, 1);
            int Y_TxtScore = (int)(Y_GameOver + txtGameOverSize.Y + Spacing);
            Raylib.DrawText(score, (int)(x - txtScoreSize.X / 2), Y_TxtScore, fontSize, Color.Red);

            int Y_BtnRestart = (int)(Y_TxtScore + txtScoreSize.Y + Spacing);
            btnRestart.Y = Y_BtnRestart;
            btnRestart.Draw();
        }

        public override void Load()
        {
        }

        public override void Update()
        {

            btnRestart.Update();
        }

        public override void Unload()
        {
        }

        private void restart()
        {
            scoreController.Reset();
            Program._sceneManager.Load<SceneGame>();
        }

    }
}
