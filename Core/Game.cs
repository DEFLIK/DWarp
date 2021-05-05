using System.Timers;

namespace DWarp
{
    public static class Game
    {
        public static Level CurrentLevel = Presets.HardLevel;

        public static Creature[,] Map;
        public static Cube[,] Cubes;
        public static int SpritesSize;
        public static bool IsWarped = false;
        public static Player Player = new Player(Properties.Resources.Player);
        public static Player WarpedPlayer = new Player(Properties.Resources.Player);
        public static CommandsStack<ICommand> CommandsStack;
        public static int Time;

        private static Timer timer;

        public static void Load(Level level)
        {
            Time = level.TimeLimit;
            Map = MapCreator.CreateMap(level.Map);
            Cubes = new Cube[Map.GetLength(0), Map.GetLength(1)];
            MapCreator.SpawnDynamicCreatures(Map);
            if (level.Wires != null)
                MapCreator.WireButtonsWithDoors(Map, level.Wires);
            SpritesSize = 230 / Map.GetLength(0);
            CommandsStack = new CommandsStack<ICommand>(CurrentLevel.StepsLimit);
            WarpedPlayer.PickedCube = null;
            Player.PickedCube = null;
            if (IsWarped)
                DoWarp();
            if (level.TimeLimit > 0)
            {
                timer = new Timer();
                timer.Interval = 1000;
                timer.Elapsed += (sender, args) =>
                {
                    Time--;
                    if (Time <= 0)
                    {
                        timer.Dispose();
                        Load(CurrentLevel);
                    }
                };
            }
        }

        public static void StartTimer() => timer.Start();

        public static void DoWarp()
        {
            if (!IsWarped)
                IsWarped = true;
            else
            {
                if (WarpedPlayer.PickedCube != null)
                {
                    CubeActions.Place(WarpedPlayer);
                }
                CommandsStack.ResetBack();
                IsWarped = false;
            }
            WarpedPlayer.Location.X = Player.Location.X;
            WarpedPlayer.Location.Y = Player.Location.Y;
        }

        public static void WarpPlayer()
        {
            if (IsWarped)
            {
                if (WarpedPlayer.PickedCube != null)
                    CubeActions.Place(WarpedPlayer);
                CommandsStack.Canceled.Clear();
                Player.Location.X = WarpedPlayer.Location.X;
                Player.Location.Y = WarpedPlayer.Location.Y;
                IsWarped = false;
            }
        }
    }
}
