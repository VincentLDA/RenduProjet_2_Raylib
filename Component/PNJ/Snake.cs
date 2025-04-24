using Raylib_cs;
using System;
using Projet_S.Component.MAP;

namespace Projet_S.Component.PNJ
{
    public class Snake
    {
        private Grid grid;

        public Queue<Coordinates> body { get;} = new Queue<Coordinates>();
        private Coordinates direction = Coordinates.left;
        private Coordinates nextDirection = Coordinates.left;


        private Texture2D textureHead = Raylib.LoadTexture("assets/snake/snake_green_head_64.png");
        private Texture2D textureBody = Raylib.LoadTexture("assets/snake/snake_green_blob_64.png");
        //private Texture2D textureQueue = Raylib.LoadTexture("assets/snake/snake_green_head_32.png");

        public Coordinates Head => body.Last();
        public double moveSpeed = 1f;
        private bool isGrowing = false;

        public Snake(Grid grid,Coordinates start,int startSize = 3)
        {
            this.grid = grid;
            for (int i = 0; i < startSize; i++) {
                body.Enqueue(start + direction * i);            
            }
        }
        
        public void Move()
        {
            direction = nextDirection;
            body.Enqueue(body.Last() + direction);
            if (!isGrowing) body.Dequeue();
            else isGrowing = false;
        }

        public void ChangeDirection(Coordinates newDirection)
        {
            if (newDirection == -direction || newDirection == Coordinates.zero) return;
            nextDirection = newDirection;
        }

        public void Update()
        {
        }

        public void Draw()
        {
            foreach (var coordinates in body)
            {
                var pos = grid.GridToWorld(coordinates);
                Texture2D textureDraw;

                if (coordinates.Equals(body.Last()))
                {
                    textureDraw = textureHead;
                    Raylib.DrawRectangle((int)pos.X, (int)pos.Y, grid.cellSize, grid.cellSize, Color.Magenta);
                }

                if (coordinates.Equals(body.First()))
                {
                    textureDraw = textureHead;
                    Raylib.DrawRectangle((int)pos.X, (int)pos.Y, grid.cellSize, grid.cellSize, Color.Beige);
                }

                if (coordinates.Equals(body.Last()))
                {
                    textureDraw = textureHead;
                } else
                {
                    textureDraw= textureBody;
                }
                Raylib.DrawTexture(textureDraw, (int)(pos.X), (int)(pos.Y), Color.White);
                //Raylib.DrawRectangle((int)pos.X, (int)pos.Y, grid.cellSize, grid.cellSize, Color.Magenta);

            }


        }

        public bool IsCollidingWithApple(Apple apple)
        {
            return Head == apple.coordinates;
        }
        public bool IsCollidingWithCubeRandom(CubeRandom cubeRandom)
        {
            return Head == cubeRandom.coordinates;
        }

        public bool IsCollidingWithItself()
        {
            return body.Count != body.Distinct().Count();
        }

        public bool IsCollidingWithWall()
        {
            return MapBinding.HaveCoordinate(grid.mapBindings, Head);
        }

        public bool IsOutOfBounds()
        {
            return grid.IsInBounds(Head);
        }

        public void Grow()
        {
            isGrowing = true;
        }

        public void speedUp()
        {
            if((moveSpeed * 0.9f) > 0.5)
                moveSpeed *= 0.9f;
        }

    }
}
