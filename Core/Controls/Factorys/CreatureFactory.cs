using System;
using DWarp.Core.Models;

namespace DWarp.Core.Controls.Factorys
{
    public enum CreatureType
    {
        Player,
        WarpedPlayer,
        Wall,
        PlayerSpawn,
        Void,
        Button,
        Door,
        CubeSpawn,
        Exit
    }

    public static class CreatureFactory
    {
        static public Creature GetNewCreature(CreatureType type)
        {
            switch (type)
            {
                case CreatureType.Void:
                    return new Creature(type, Properties.Resources.Floor);
                case CreatureType.Wall:
                    return new Creature(type, null);
                case CreatureType.Button:
                    return new Button(Properties.Resources.ButtonF);
                case CreatureType.PlayerSpawn:
                    return new Creature(type, Properties.Resources.Spawn);
                case CreatureType.Door:
                    return new Door(Properties.Resources.Door);
                case CreatureType.CubeSpawn:
                    return new Creature(type, Properties.Resources.Floor);
                case CreatureType.Exit:
                    return new Creature(type, Properties.Resources.Exit);
                default:
                    throw new ArgumentException($"{type} is not registred Creature type");
            }
        }
    }
}
