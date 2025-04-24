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
        private ButtonTriangle btnTriangle;
        private Texture2D txtu = Raylib.LoadTexture("assets/background.png");
        private string TXT_INFO = "Click on snake to play!";

        float centerX = Program.widthScreen / 2.0f;
        float centerY = Program.heightScreen / 2.0f;

        Vector2 pointRight ;
        Vector2 pointTop;
        Vector2 pointBottom ;

        public SceneHome()
        {
            pointRight = new Vector2(centerX + 170, centerY + 70);
            pointTop = new Vector2(centerX - 280, centerY + 350);
            pointBottom = new Vector2(centerX - 280, centerY - 120.0f);

            //btnPlay = new ButtonTexture(Play, button, buttonOver, btnIcons);
            btnTriangle = new ButtonTriangle(Play, pointRight, pointTop, pointBottom);
            txtu.Width = Program.widthScreen;
            txtu.Height = Program.heightScreen;

        }

        public override void Draw()
        {
            Raylib.DrawTexture(txtu, 0, 0, Color.White);

            //btnPlay.Draw();
            btnTriangle.Draw();


            Vector2 txtInfoPlaySize = Raylib.MeasureTextEx(Raylib.GetFontDefault(), TXT_INFO, 10, 1);
            Raylib.DrawText(TXT_INFO, (int)(Program.widthScreen / 2 - txtInfoPlaySize.X / 2 - 60), Program.heightScreen - 40 , 20, Color.White);




        }

        public override void Load()
        {

        }

        public override void Unload()
        {

        }

        public override void Update()
        {
            //btnPlay.Update();
            btnTriangle.Update();
        }

        public void Play()
        {
            Program._sceneManager.Load<SceneGame>();
        }

    }


    public class ButtonTriangle
    {
        public int HeightBtnTexture { get; set; } = 128;
        public int WidthBtnTexture { get; set; } = 128;

        private int btnState = 0;               // Button state: 0-NORMAL, 1-MOUSE_HOVER, 2-PRESSED
        private bool btnAction = false;         // Button action should be activated
        private Rectangle btnBounds;
        private Action callback;

        Vector2 pointRight { get; set; }
        Vector2 pointTop { get; set; }
        Vector2 pointBottom { get; set; }

        public int X { get; set; }
        public int Y { get; set; }

        public ButtonTriangle(Action callback, Vector2 pointRight, Vector2 pointTop, Vector2 pointBottom)
        {
            this.pointRight = pointRight;
            this.pointTop = pointTop;
            this.pointBottom = pointBottom; 
            this.callback = callback;
            //this.X = Program.widthScreen / 2 - this.WidthBtnTexture / 2;
            //this.Y = Program.heightScreen / 2 - this.HeightBtnTexture / 2;

            this.X = Program.widthScreen / 2;
            this.Y = Program.heightScreen / 2;

            this.btnBounds = new Rectangle(X - WidthBtnTexture / 2, Y - HeightBtnTexture / 2, WidthBtnTexture, HeightBtnTexture);
        }


        public void Update()
        {
            this.btnAction = false;
            if (PointInTriangle(Program.mousePoint, pointRight, pointBottom, pointTop))
            {
                if (Raylib.IsMouseButtonDown(MouseButton.Left)) this.btnState = 2;
                else this.btnState = 1;

                if (Raylib.IsMouseButtonReleased(MouseButton.Left)) this.btnAction = true;
            }
            else btnState = 0;

            if (this.btnAction)
            {
                this.callback?.Invoke();
            }

        }

        public void Draw()
        {
            Raylib.DrawTriangle(pointRight, pointBottom, pointTop, new Color(255, 255, 255, 0));
        }

        private bool PointInTriangle(Vector2 pt, Vector2 v1, Vector2 v2, Vector2 v3)
        {
            float dX = pt.X - v3.X;
            float dY = pt.Y - v3.Y;
            float dX21 = v3.X - v2.X;
            float dY12 = v2.Y - v3.Y;
            float D = dY12 * (v1.X - v3.X) + dX21 * (v1.Y - v3.Y);
            float s = dY12 * dX + dX21 * dY;
            float t = (v3.Y - v1.Y) * dX + (v1.X - v3.X) * dY;
            if (D < 0) return s <= 0 && t <= 0 && s + t >= D;
            return s >= 0 && t >= 0 && s + t <= D;
        }
    }
}
