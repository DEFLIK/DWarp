using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using DWarp.Core.Models;

namespace DWarp.Core.Controls
{
    public class DijkstraData
    {
        public Point? Previous { get; set; }
        public int Cost { get; set; }
    }

    public class PathFinder
    {
        public static IEnumerable<Point> GetIncidents(Point location, State state)
        {
            return new Point[]
            {
                new Point(location.X + 1, location.Y),
                new Point(location.X, location.Y + 1),
                new Point(location.X - 1, location.Y),
                new Point(location.X, location.Y - 1)
            }.Where(point => state.InsideMap(point) && !state.IsWallAt(point));
        }

        public IEnumerable<PathWithCost> GetPathsByDijkstra(State state, Point start,
            IEnumerable<Point> targets)
        {
            var targetPoints = targets.ToHashSet();
            var visitedPoints = new HashSet<Point>();
            var track = new Dictionary<Point, DijkstraData>();
            track[start] = new DijkstraData { Previous = null, Cost = 0 };

            while (track.Count > 0)
            {
                (var toOpen, var pointData) = GetCurrentGreedyPointInfo(track, visitedPoints);
                if (pointData == null)
                    break;
                var bestCost = pointData.Cost;
                if (targetPoints.Contains(toOpen))
                    yield return GetPathWithCost(start, toOpen, track);
                foreach (var nextPoint in GetIncidents(toOpen, state))
                    AddIncedentPointToTrack(toOpen, nextPoint, track, state, bestCost);
                visitedPoints.Add(toOpen);
            }
        }

        private static (Point Point, DijkstraData Data) GetCurrentGreedyPointInfo(
            Dictionary<Point, DijkstraData> track,
            HashSet<Point> visitedPoints)
        {
            var currentData = track
                .Where(data => !visitedPoints.Contains(data.Key))
                .OrderBy(data => data.Value.Cost)
                .FirstOrDefault();
            return (currentData.Key, currentData.Value);
        }

        private static void AddIncedentPointToTrack(
            Point sourcePoint,
            Point nextPoint,
            Dictionary<Point, DijkstraData> track,
            State state,
            int bestCost)
        {
            var currentCost = bestCost + 1;
            if (!track.ContainsKey(nextPoint) || track[nextPoint].Cost > currentCost)
                track[nextPoint] = new DijkstraData { Previous = sourcePoint, Cost = currentCost };
        }

        private static PathWithCost GetPathWithCost(Point start, Point end, Dictionary<Point, DijkstraData> track)
        {
            var resultPath = new List<Point>();

            Point? pathPoint = end;
            while (pathPoint != null)
            {
                resultPath.Add(pathPoint.Value);
                pathPoint = track[pathPoint.Value].Previous;
            }

            return new PathWithCost(track[end].Cost, resultPath.Reverse<Point>().ToArray());
        }
    }
}
