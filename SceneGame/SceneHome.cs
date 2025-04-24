using Projet_S.Component.UI;
using Projet_S.SceneUtils;
using Raylib_cs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Projet_S.SceneGame
{
    public class SceneHome : Scene
    {
        private Texture2D button = Raylib.LoadTexture("assets/ui/Circle_Buttons/Circle_Blue_Button_Normal.png");
        private Texture2D buttonOver = Raylib.LoadTexture("assets/ui/Circle_Buttons/Circle_Blue_Button_Pressed.png");
        private Texture2D btnIcons = Raylib.LoadTexture("assets/ui/Icons/Arrow_Right.png");
        private ButtonTexture btnPlay;

        private Texture2D txtu = Raylib.LoadTexture("assets/background.png");
        

        public SceneHome()
        {
            btnPlay = new ButtonTexture(Play, button, buttonOver, btnIcons);
            //btnPlay = new ButtonTexture(Play, button, buttonOver, btnIcons);
            txtu.Width = Program.widthScreen;
            txtu.Height = Program.heightScreen;

        }

        public override void Draw()
        {


            Raylib.DrawTexture(txtu, 0,0, Color.White);

            Raylib.DrawTriangleLines(new Vector2(Program.widthScreen / 4.0f * 3.0f, 160.0f),
                              new Vector2(Program.widthScreen / 4.0f * 3.0f - 20.0f, 230.0f),
                              new Vector2(Program.widthScreen / 4.0f * 3.0f + 20.0f, 230.0f), Color.White);


            btnPlay.Draw();
        }

        public override void Load()
        {

        }

        public override void Unload()
        {

        }

        public override void Update()
        {
            btnPlay.Update();
        }

        public void Play()
        {
            Program._sceneManager.Load<SceneGame>();
        }

    }

}
