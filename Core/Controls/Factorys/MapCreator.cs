using System;
using System.Linq;
using System.Drawing;
using DWarp.Core.Models;

namespace DWarp.Core.Controls.Factorys
{
    public static class MapCreator
    {

        public static Creature[,] CreateMap(string map)
        {
            var rows = map.Replace(" ", string.Empty).Split(new[] { "\r", "\n" }, StringSplitOptions.RemoveEmptyEntries);
            if (rows.Select(z => z.Length).Distinct().Count() != 1)
                throw new Exception($"Wrong test map '{map}'");
            var result = new Creature[rows[0].Length, rows.Length];
            for (var x = 0; x < rows[0].Length; x++)
                for (var y = 0; y < rows.Length; y++)
                    result[x, y] = CreateCreatureBySymbol(rows[y][x], x, y);
            return result;
        }

        private static Creature CreateCreatureBySymbol(char c, int x, int y)
        {
            CreatureType type;
            switch (c)
            {
                case 'P':
                    type = CreatureType.PlayerSpawn;
                    break;
                case 'K':
                    type = CreatureType.DummySpawn;
                    break;
                case 'B':
                    type = CreatureType.Button;
                    break;
                case 'D':
                    type = CreatureType.Door;
                    break;
                case '.':
                    type = CreatureType.Void;
                    break;
                case '+':
                    type = CreatureType.Wall;
                    break;
                case 'C':
                    type = CreatureType.CubeSpawn;
                    break;
                case 'E':
                    type = CreatureType.Exit;
                    break;
                default:
                    throw new Exception($"wrong character for ICreature {c}");
            }

            var resultCreature = CreatureFactory.GetNewCreature(type);
            resultCreature.Location = new Point(x, y);
            return resultCreature;
        }

        public static void SpawnDynamicCreatures(State state)
        {
            var map = state.Map;
            foreach (var creature in map)
            {
                var pos = creature.Location;
                switch (creature.Type)
                {
                    case CreatureType.PlayerSpawn:
                        state.Player.Location = new Point(pos.X, pos.Y);
                        state.WarpedPlayer.Location = new Point(pos.X, pos.Y);
                        break;
                    case CreatureType.CubeSpawn:
                        state.Cubes[pos.X, pos.Y] = new Cube(Properties.Resources.Cube, new Point(pos.X, pos.Y));
                        state.Cubes[pos.X, pos.Y].Location = new Point(pos.X, pos.Y);
                        break;
                    case CreatureType.DummySpawn:
                        state.Dummy = new Dummy(Properties.Resources.Dummy, new Point(pos.X, pos.Y));
                        state.Dummy.Location = new Point(pos.X, pos.Y);
                        break;
                }
            }
        }

        public static void WireButtonsWithDoors(State state)
        {
            var map = state.Map;
            var wires = state.CurrentLevel.Wires;
            foreach (var wire in wires)
            {
                var button = map[wire.Button.X, wire.Button.Y] as Button;
                var door = map[wire.Door.X, wire.Door.Y] as Door;
                if (button == null || door == null)
                    throw new ArgumentException($"Wrong wiring rule:" +
                        $"\nButton at [{wire.Button.X},{wire.Button.Y}]: {button is Button}" +
                        $"\nDoor at [{wire.Door.X},{wire.Door.Y}]: {door is Door}");
                button.LinkedDoor = door;
            }
        }
    }
}
