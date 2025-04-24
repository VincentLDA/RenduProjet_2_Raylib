using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Raylib_cs;

namespace Projet_S.Component.MAP
{
    public class MapBinding
    {
        public Coordinates Coordinates { get; set; }
        public Texture2D texture { get; set; }

        public MapBinding(Coordinates Coordinates, Texture2D texture)
        {
            this.Coordinates = Coordinates;
            this.texture = texture;
        }

        public static bool HaveCoordinate(List<MapBinding> mapBindings, Coordinates coordinates)
        {
            return mapBindings.Any(mb => mb.Coordinates == coordinates);
        }

        public static MapBinding GetMapBinding(List<MapBinding> mapBindings, Coordinates coordinates)
        {   
            if(HaveCoordinate(mapBindings, coordinates))
            {
                MapBinding mapBinding = mapBindings.Single(mb => mb.Coordinates == coordinates);
                if (mapBinding != null)
                {
                    return mapBinding;
                }
            }
            return null;
        }

    }
}
