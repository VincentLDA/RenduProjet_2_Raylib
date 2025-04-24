using Raylib_cs;
using System.Numerics;
using Projet_S.Component.MAP;
using System;

namespace Projet_S.Component.PNJ
{
    public class Apple
    {
        public Coordinates coordinates {  get; private set; }
        private Grid grid;
        private Snake snake;
        private CubeRandom cubeRandom;


        private Texture2D texture = Raylib.LoadTexture("assets/apple/apple_red_32.png");

        public Apple(Grid grid, Snake snake, CubeRandom cubeRandom)
        {
            this.grid = grid;
            this.snake = snake;
            this.cubeRandom = cubeRandom;
            coordinates = Coordinates.zero;
            Spawn();
        }

        public void Spawn()
        {
            Coordinates c = Coordinates.getRandom(grid.columns, grid.rows);           
            while (snake.body.Contains(c) || c.Equals(cubeRandom.coordinates) || MapBinding.HaveCoordinate(grid.mapBindings, c))
            {
                c = Coordinates.getRandom(grid.columns, grid.rows);
            }
            coordinates = c;
        }

        public void Draw()
        {
            var pos = grid.GridToWorld(coordinates);
            pos += new Vector2(grid.cellSize * .5f, grid.cellSize * .5f);
            Raylib.DrawTexture(texture, (int)(pos.X - texture.Width/2), (int)(pos.Y - texture.Height/2), Color.White);
        }
    }
}
