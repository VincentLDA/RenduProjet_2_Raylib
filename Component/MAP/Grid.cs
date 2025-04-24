using Raylib_cs;
using System.Numerics;
using System.Reflection.Emit;

namespace Projet_S.Component.MAP
{

    public class Grid
    {
        public Color colorCase1 { get; set; } = Color.Brown;
        public Color colorCase2 { get; set; } = Color.DarkBrown;

        public List<MapBinding> mapBindings { get; private set; }

        public Vector2 position = Vector2.Zero;
        public int columns { get; private set; }
        public int rows { get; private set; }
        public int cellSize { get; private set; }

        public int GridWidth { get; private set; }
        public int GridHeight { get; private set; }

        public Grid(int colonnes = 10, int rows = 10, int cellSize = 64)
        {
            this.columns = colonnes;
            this.rows = rows;
            this.cellSize = cellSize;
            this.GridWidth = cellSize * colonnes;
            this.GridHeight = cellSize * rows;
            this.mapBindings = FileLoader.Load();
        }

        public Coordinates WorldToGrid(Vector2 pPosition)
        {
            pPosition -= position;
            pPosition /= cellSize;
            return new Coordinates((int)pPosition.X, (int)pPosition.Y);
        }

        public Vector2 GridToWorld(Coordinates pCoordinates)
        {
            pCoordinates *= cellSize;
            return pCoordinates.ToVector + position;
        }

        public void DrawGrid()
        {
            for (int column = 0; column < columns; column++) {
                for (int row = 0; row < rows; row++) {
                    Coordinates coordinates = new(column, row);
                    Vector2 pos = GridToWorld(new(column, row));


                    MapBinding mapBinding = MapBinding.GetMapBinding(mapBindings, coordinates);
                    if (mapBinding != null)
                    {
                        Raylib.DrawTexture(mapBinding.texture, (int)(pos.X), (int)(pos.Y), Color.White);
                    } else
                    {
                        Color cellColor = ((pos.X + pos.Y) % 2 == 0) ? colorCase1 : colorCase2;
                        Raylib.DrawRectangle((int)(pos.X), (int)(pos.Y), cellSize, cellSize, cellColor);
                    }

                    Raylib.DrawRectangleLines((int)pos.X, (int)pos.Y, cellSize, cellSize, Color.White);

                }
            }
        }

        public bool IsInBounds(Coordinates pCoordinates)
        {
            return pCoordinates.columns >= 0 && pCoordinates.rows >= 0 && pCoordinates.columns < columns && pCoordinates.rows < rows;
        }

        private void DrawCheckerboardBackground(int cellSize, int cols, int rows, Color color1, Color color2)
        {
            for (int y = 0; y < rows; y++)
            {
                for (int x = 0; x < cols; x++)
                {
                    Color cellColor = ((x + y) % 2 == 0) ? color1 : color2;
                    Raylib.DrawRectangle(x * cellSize, y * cellSize, cellSize, cellSize, cellColor);
                }
            }
        }
    }


    public static class FileLoader{

        private static Texture2D wall = Raylib.LoadTexture("assets/wall/wall_block_64_0.png");


        public  static List<MapBinding> Load()
        {
            List<MapBinding> map = new List<MapBinding>();

            string basePath = Directory.GetCurrentDirectory();
            string filePath = Path.Combine(basePath, "assets", "level", "Map1.txt");
            if (File.Exists(filePath))
            {
                string[] lines = File.ReadAllLines(filePath);

                int rows = lines.Length;
                int columns = lines[0].Length;

                for (int row = 0; row < rows; row++)
                {
                    for (int column = 0; column < columns; column++)
                    {
                        Coordinates coordinates = new Coordinates(column, row);
                        Texture2D texture  ;

                        if(lines[row][column].ToString() == "1")
                        {
                            texture = wall;
                            map.Add(new MapBinding(coordinates, texture));
                        }
                    }
                }
                    
            }
            else
            {
                Console.WriteLine("Fichier non trouvé : " + filePath);
            }
            return map;
        }

       
    }
}
