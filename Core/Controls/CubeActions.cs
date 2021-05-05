using DWarp.Core.Controls.Factorys;
using DWarp.Core.Drawing;
using DWarp.Core.Models;

namespace DWarp.Core.Controls
{
    public static class CubeActions
    {
        /// <summary>
        /// Поднимает кубик с помощью рук указанного игрока
        /// </summary>
        /// <returns> Результат поднятия кубика: true - кубик взят, false - кубик не был поднят</returns>
        public static bool Take(Player player)
        {
            var cube = Game.Cubes[player.Location.X, player.Location.Y];
            if (cube is Cube)
            {
                player.PickedCube = cube;
                Game.Cubes[player.Location.X, player.Location.Y] = null;
                if (Game.Map[player.Location.X, player.Location.Y] is Button)
                    (Game.Map[player.Location.X, player.Location.Y] as Button).Update();
                return true;
            }
            return false;
        }


        /// <summary>
        /// Кладёт кубик из рук указанного игрока
        /// </summary>
        /// <returns> Результат расположения кубика: true - кубик опущен, false - кубик остался в руках</returns>
        public static bool Place(Player player)
        {
            var mapCreature = Game.Map[player.Location.X, player.Location.Y];
            var cube = Game.Cubes[player.Location.X, player.Location.Y];
            if (cube == null && mapCreature.Type != CreatureType.Wall && mapCreature.Type != CreatureType.Door && player.PickedCube != null)
            {
                Game.Cubes[player.Location.X, player.Location.Y] = player.PickedCube;
                (player.PickedCube.Location.X, player.PickedCube.Location.Y) = (player.Location.X, player.Location.Y);
                Animations.Fall(player.PickedCube, 10);
                player.PickedCube = null;
                if (Game.Map[player.Location.X, player.Location.Y] is Button)
                    (Game.Map[player.Location.X, player.Location.Y] as Button).Update();
                return true;
            }
            return false;
        }
    }
}
