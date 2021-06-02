using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Timers;
using DWarp.Core.Controls;
using DWarp.Core.Controls.Factorys;

namespace DWarp.Core.Models
{
    public class Dummy : Player
    {
        public readonly Point RespawnLocation;
        private Timer stepTimer = new Timer();
        private int stepCount = 0;
        private int maxStepsCount;

        public Dummy(Bitmap image, Point respawnPoint) : base(image, 5) 
        {
            RespawnLocation = respawnPoint;
        }

        public PathWithCost GetPath(State state)
        {
            var pathFinder = new PathFinder();
            var cubesLocations = new List<Point>();
            var cubes = state.Cubes;
            if (PickedCube != null)
                if (Location == PickedCube.RespawnLocation)
                    return pathFinder.GetPathsByDijkstra(state, Location, new List<Point>() { RespawnLocation }).FirstOrDefault();
                else
                    return pathFinder.GetPathsByDijkstra(state, Location, new List<Point>() { PickedCube.RespawnLocation }).FirstOrDefault();
            foreach (var cube in cubes)
                if (cube != null)
                    cubesLocations.Add(cube.Location);
            if (cubesLocations.Count == 0)
                return pathFinder.GetPathsByDijkstra(state, Location, new List<Point>() { RespawnLocation }).FirstOrDefault();
            else
                return pathFinder.GetPathsByDijkstra(state, Location, cubesLocations).FirstOrDefault();
        }

        public void BeginWalk(State state, int stepInterval) // torefactor
        {
            var currentPath = GetPath(state);
            if (currentPath == null)
                return;
            stepTimer.Dispose();
            maxStepsCount = currentPath.Path.Count;
            stepCount = 0;
            stepTimer = new Timer();
            stepTimer.Interval = stepInterval;
            stepTimer.Elapsed += (sender, args) =>
            {
                stepCount++;
                if (stepCount < maxStepsCount) // Dummy hasn't reached goal yet
                {
                    var step = currentPath.Path[stepCount];
                    if (state.CanMoveAt(step))
                    {
                        Location.X = step.X;
                        Location.Y = step.Y;
                    }
                    else
                        stepCount--;
                }
                else // Dummy reached goal
                {
                    stepTimer.Dispose();
                    if (Location == RespawnLocation)
                        return;
                    if (PickedCube == null)
                    {
                        CubeActions.Take(state, this);
                        BeginWalk(state, stepInterval);
                        if (state.Map[Location.X, Location.Y].Type == CreatureType.CubeSpawn)
                            CubeActions.Place(state, this);
                    }
                    else
                    {
                        BeginWalk(state, stepInterval);
                        CubeActions.Place(state, this);
                    }
                }
            };
            stepTimer.Start();
        }

        public void PauseWalk() => stepTimer.Stop();
        public void ResumeWalk() => stepTimer.Start();
    }
}
