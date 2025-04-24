using Raylib_cs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Projet_S.Component.UI
{
    public class ButtonTexture
    {
        private Texture2D textureButton;
        private Texture2D buttonOver;
        private Texture2D btnIcons;
        public int HeightBtnTexture { get; set; } = 128;
        public int WidthBtnTexture { get; set; } = 128;
        public int HeightIconTexture { get; set; } = 128;
        public int WidthIconTexture { get; set; } = 128;

        private int btnState = 0;               // Button state: 0-NORMAL, 1-MOUSE_HOVER, 2-PRESSED
        private bool btnAction = false;         // Button action should be activated
        private Rectangle sourceRec, btnBounds;

        private Action callback;

        public int X { get; set; }
        public int Y { get; set; }

        public ButtonTexture(Action callback, Texture2D textureButton, Texture2D textureButtonOver, Texture2D textureIcons)
        {
            this.textureButton = textureButton;
            this.buttonOver = textureButtonOver;
            this.btnIcons = textureIcons;

            this.textureButton.Width = this.WidthBtnTexture;
            this.textureButton.Height = this.HeightBtnTexture;

            this.buttonOver.Width = this.WidthBtnTexture;
            this.buttonOver.Height = this.HeightBtnTexture;

            this.btnIcons.Width = this.WidthIconTexture;
            this.btnIcons.Height = this.HeightIconTexture;

            this.X = Program.widthScreen / 2 - this.textureButton.Width / 2;
            this.Y = Program.heightScreen / 2 - this.textureButton.Height / 2;

            this.sourceRec = new Rectangle(0, 0, this.WidthBtnTexture, this.HeightBtnTexture);

            this.callback = callback;

        }


        public void Update()
        {
            this.btnAction = false;
            if (Raylib.CheckCollisionPointRec(Program.mousePoint, this.btnBounds))
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

            this.sourceRec.Y = this.btnState * this.HeightBtnTexture;
        }

        public void Draw()
        {
            this.btnBounds = new Rectangle(X, Y, this.WidthBtnTexture, this.HeightBtnTexture);
            Rectangle iconBounds = new Rectangle(X, Y - 10, this.WidthBtnTexture, this.HeightBtnTexture);
            Texture2D textureBtn = this.textureButton;
            if (this.btnState == 1)
            {
                textureBtn = this.buttonOver;
                iconBounds.Y = (int)iconBounds.Y + 5;
            }
            Raylib.DrawTextureRec(textureBtn, this.sourceRec, new Vector2(this.btnBounds.X, this.btnBounds.Y), Color.White);
            Raylib.DrawTextureRec(this.btnIcons, this.sourceRec, new Vector2(iconBounds.X, iconBounds.Y), Color.White);
        }
    }
}
