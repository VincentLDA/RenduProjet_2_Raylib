using Projet_S.Component.MAP;
using Projet_S.Component.UI;
using Projet_S.Enums;
using Projet_S.SceneUtils;
using Raylib_cs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Projet_S.Component.PNJ
{
    public class CubeRandom
    {
        static IScoreController scoreController = ServicesLocator.Get<IScoreController>();
        private RandomSelector randomSelector;

        public Dictionary<CubeRandomBonus, string> di_cubeRandomBonus { get; private set; }
        public CubeRandomBonus CubeRandomBonus { get; set; }

        public Coordinates coordinates { get; private set; }
        private Grid grid;
        private Snake snake;
        private Apple apple;


        private Texture2D texture = Raylib.LoadTexture("assets/ui/Square_Buttons/Square_Light_Blue_Button_Pressed.png"); 
        private Texture2D icons = Raylib.LoadTexture("assets/ui/Icons/Interrogation.png");

        private bool spawnCubeBonus = false;
        public int spawnLuck = 20;

        public CubeRandom(Grid grid, Snake snake, Apple apple)
        {
            this.grid = grid;
            this.snake = snake;
            this.apple = apple;
            coordinates = Coordinates.zero;

            int size = grid.cellSize - 15;
            texture.Height = size;
            texture.Width = size;
            icons.Height = size;
            icons.Width = size;

            CubeRandomBonus = CubeRandomBonus.WAITING;
            randomSelector = new RandomSelector(this, snake);

            setCubeRandomBonus();
            Spawn();
        }

        public void Spawn()
        {
            Random rand = new Random(); 
            int chance = rand.Next(100);
            Console.WriteLine($"{chance}/100");
            if (chance <= spawnLuck)
            {
                Coordinates c = Coordinates.getRandom(grid.columns, grid.rows);
                while (snake.body.Contains(c) || c.Equals(apple?.coordinates) || MapBinding.HaveCoordinate(grid.mapBindings, c))
                {
                    c = Coordinates.getRandom(grid.columns, grid.rows);
                }
                coordinates = c;
                spawnCubeBonus = true;
                spawnLuck = 20;
            } 
        }

        public void Load()
        {
            randomSelector.Load();
        }

        public void Draw()
        {
            if (spawnCubeBonus)
            {
                var pos = grid.GridToWorld(coordinates);
                pos += new Vector2(grid.cellSize * .5f, grid.cellSize * .5f);

                Raylib.DrawTexture(texture, (int)(pos.X - texture.Width / 2), (int)(pos.Y - texture.Height / 2), Color.White);
                Raylib.DrawTexture(icons, (int)(pos.X - icons.Width / 2), (int)(pos.Y - icons.Height / 2), Color.White);

            }
            randomSelector.Draw();

        }

        public void Update()
        {
            randomSelector.Update();

        }
        private void setCubeRandomBonus()
        {
            di_cubeRandomBonus = new Dictionary<CubeRandomBonus, string>();

            di_cubeRandomBonus.Add(CubeRandomBonus.WAITING, "");
            di_cubeRandomBonus.Add(CubeRandomBonus.SCORE_50, "Bonus de 50 PTS");
            di_cubeRandomBonus.Add(CubeRandomBonus.SCORE_10, "Bonus de 10 PTS");
            di_cubeRandomBonus.Add(CubeRandomBonus.GROW, "Grow");
        }

        public void increaseLuck()
        {
            Console.WriteLine("Increase");
            spawnLuck += 5;
        }

        public void Rand()
        {
            var filteredList = di_cubeRandomBonus
               .Where(c => c.Key != CubeRandomBonus.WAITING)
               .ToList();

            Random rand = new Random();
            int index = rand.Next(filteredList.Count);

            this.CubeRandomBonus = filteredList[index].Key;
        }

        public void Bonus()
        {
            spawnCubeBonus = false;
            randomSelector.ROLL();           
        }
        public void ApplyBonus()
        {
            if (this.CubeRandomBonus == CubeRandomBonus.SCORE_10)
            {
                scoreController.AddScore(10);
                CubeRandomBonus = CubeRandomBonus.WAITING;
            }
            else if (this.CubeRandomBonus == CubeRandomBonus.SCORE_50)
            {
                scoreController.AddScore(50);
                CubeRandomBonus = CubeRandomBonus.WAITING;
            }
            else if (this.CubeRandomBonus == CubeRandomBonus.GROW)
            {
                snake.Grow();
                CubeRandomBonus = CubeRandomBonus.WAITING;
            }
        }
    }
}
