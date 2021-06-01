using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using DWarp.Core.Controls;
using DWarp.Core.Controls.Factorys;
using DWarp.Core.Drawing;
using DWarp.Resources.Levels;

namespace DWarp.Core.Models
{
    public class State
    {
        public readonly Level CurrentLevel;
        public Creature[,] Map;
        public Cube[,] Cubes;
        public int SpritesSize;
        public bool IsWarped = false;
        public Player Player;
        public Player WarpedPlayer;
        public Dummy Dummy;
        public CommandsStack<ICommand> CommandsStack;
        public int Time;
        private Timer timer;
        public int MapWidth => Map.GetLength(0);
        public int MapHeight => Map.GetLength(1);

        public State(Level level)
        {
            Map = null;
            Player = new Player(Properties.Resources.Player);
            WarpedPlayer = new Player(Properties.Resources.Player);
            CurrentLevel = level;
            Time = level.TimeLimit;
            Map = MapCreator.CreateMap(level.MapPreset);
            Cubes = new Cube[Map.GetLength(0), Map.GetLength(1)];
            MapCreator.SpawnDynamicCreatures(this);
            if (level.Wires != null)
                MapCreator.WireButtonsWithDoors(this);
            WallBuilder.SetWallsSprite(this);
            SpritesSize = 450 / Map.GetLength(0);
            CommandsStack = new CommandsStack<ICommand>(level.StepsLimit);
            WarpedPlayer.PickedCube = null;
            Player.PickedCube = null;
            if (IsWarped)
                DoWarp();
            if (level.TimeLimit > 0)
            {
                if (timer != null)
                    timer.Dispose();
                timer = new Timer();
                timer.Interval = 1000;
                timer.Elapsed += (sender, args) =>
                {
                    Time--;
                    if (Time == 0)
                        Game.ChangeLevel(CurrentLevel);
                };
            }
            if(Dummy != null)
                Dummy.GetPath(this);
        }

        public void Dispose() // ToReflection...
        {
            //Map = null;
            //Cubes = null;
            //SpritesSize = 0;
            //IsWarped = false;
            //Player = null;
            //CommandsStack = null;
            //Time = 0;
            if(timer != null)
                timer.Dispose();
            //timer = null;
        }

        public void StartTimer() => timer.Start();

        public void DoWarp()
        {
            if (!IsWarped)
                IsWarped = true;
            else
            {
                if (WarpedPlayer.PickedCube != null)
                    CubeActions.Place(this, WarpedPlayer);
                CommandsStack.ResetBack();
                IsWarped = false;
            }
            WarpedPlayer.Location.X = Player.Location.X;
            WarpedPlayer.Location.Y = Player.Location.Y;
        }

        public void WarpPlayer()
        {
            if (IsWarped)
            {
                if (WarpedPlayer.PickedCube != null)
                    CubeActions.Place(this, WarpedPlayer);
                CommandsStack.Canceled.Clear();
                Player.Location.X = WarpedPlayer.Location.X;
                Player.Location.Y = WarpedPlayer.Location.Y;
                IsWarped = false;
            }
        }

        public bool InsideMap(Point point) => InsideMap(point.X, point.Y);

        public bool InsideMap(int x, int y) => x >= 0 && x < MapWidth && y >= 0 && y < MapHeight;

        public bool IsWallAt(Point point) => IsWallAt(point.X, point.Y);

        public bool IsWallAt(int x, int y) => Map[x, y].Type == CreatureType.Wall;

        public bool CanMoveAt(Point point) => CanMoveAt(point.X, point.Y);

        public bool CanMoveAt(int x, int y) => !IsWallAt(x, y) && Map[x, y].Type == CreatureType.Door ? ((Door)Map[x, y]).Opened : true;
    }
}
