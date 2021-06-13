using System;
using System.Drawing;
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
        public static bool Take(State state, Player player)
        {
            var cube = state.Cubes[player.Location.X, player.Location.Y];
            if (cube is Cube)
            {
                player.PickedCube = cube;
                state.Cubes[player.Location.X, player.Location.Y] = null;
                if (state.Map[player.Location.X, player.Location.Y] is Button)
                    (state.Map[player.Location.X, player.Location.Y] as Button).Update(state);
                cube.Sprite.Visible = false;
                return true;
            }
            return false;
        }


        /// <summary>
        /// Кладёт кубик из рук указанного игрока
        /// </summary>
        /// <returns> Результат расположения кубика: true - кубик опущен, false - кубик остался в руках</returns>
        public static bool Place(State state, Player player) // torefactor
        {
            var mapCreature = state.Map[player.Location.X, player.Location.Y];
            var cube = state.Cubes[player.Location.X, player.Location.Y];
            if (cube == null && mapCreature.Type != CreatureType.Wall && mapCreature.Type != CreatureType.Door && player.PickedCube != null)
            {
                state.Cubes[player.Location.X, player.Location.Y] = player.PickedCube;
                (player.PickedCube.Location.X, player.PickedCube.Location.Y) = (player.Location.X, player.Location.Y);
                Animations.Fall(state, player.PickedCube);
                player.PickedCube.Sprite.Visible = true;
                if (state.Dummy != null && state.Dummy.PickedCube == null)
                    state.Dummy.BeginWalk(state, 1000);
                player.PickedCube = null;
                if (state.Map[player.Location.X, player.Location.Y] is Button)
                    (state.Map[player.Location.X, player.Location.Y] as Button).Update(state);
                return true;
            }
            return false;
        }

        /// <summary>
        /// Перемещает кубик из указанного места в другое
        /// </summary>
        /// <returns> Результат перемещения кубика: true - кубик перемещён, false - кубик остался на месте</returns>
        public static bool Move(State state, Point location, Point newLocation)
        {
            var cube = state.Cubes[location.X, location.Y];
            if (cube == null)
                throw new ArgumentNullException($"There is no cube at {location.X} {location.Y} on map");
            if (state.InsideMap(location) && state.CanMoveAt(newLocation))
            {
                state.Cubes[location.X, location.Y] = null;
                state.Cubes[newLocation.X, newLocation.Y] = cube;
                (cube.Location.X, cube.Location.Y) = (newLocation.X, newLocation.Y);
                return true;
            }
            else
                return false;
        }
    }
}
