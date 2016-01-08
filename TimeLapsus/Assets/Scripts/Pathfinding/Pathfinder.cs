using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class Pathfinder : ScriptWithController
{
    class Neighbour
    {
        public ColliderGraphNode NeighbourNode;
        public List<Vector2[]> Intersections;
    }
    class ColliderGraphNode
    {
        public bool Visited;
        public PolygonCollider2D Collider;
        public List<Neighbour> Neighbours = new List<Neighbour>();
    }
    class SearchNode
    {
        public ColliderGraphNode CurrentColliderNode;
        public SearchNode PreviousNode;
    }
    ColliderGraphNode[] colliderGraph;
    protected override void Start()
    {
        base.Start();
        PolygonCollider2D[] colliders;
        var walkableAreas = FindObjectsOfType<WalkableArea>();
        colliders = new PolygonCollider2D[walkableAreas.Length];
        for (int i = 0; i < walkableAreas.Length; ++i) colliders[i] = walkableAreas[i].GetComponent<PolygonCollider2D>();
        colliderGraph = new ColliderGraphNode[walkableAreas.Length];
        for (int i = 0; i < colliderGraph.Length; ++i)
        {
            colliderGraph[i] = new ColliderGraphNode();
            colliderGraph[i].Collider = colliders[i];
            for (int j = 0; j < i; ++j)
            {
                    var path1 = colliderGraph[i].Collider.GetPath(0);
                    var path2 = colliderGraph[j].Collider.GetPath(0);
                    List<Vector2[]> intersections = GetIntersections(path1, path2);
                    if (intersections.Count == 0)
                    {
                        continue;
                    }
                    colliderGraph[i].Neighbours.Add(new Neighbour() { NeighbourNode = colliderGraph[j], Intersections = intersections });
                    colliderGraph[j].Neighbours.Add(new Neighbour() { NeighbourNode = colliderGraph[i], Intersections = intersections });
            }
        }
    }
    private IEnumerable<Tuple<Vector2,Vector2>> GetLineSegments(Vector2[] path)
    {
        for (int i = 1; i < path.Length; ++i)
        {
            yield return new Tuple<Vector2, Vector2>(path[i - 1], path[i]);
        }
        yield return new Tuple<Vector2, Vector2>(path[0], path[path.Length - 1]);
    }
    private List<Vector2[]> GetIntersections(Vector2[] path1, Vector2[] path2)
    {
        List<Vector2[]> toReturn = new List<Vector2[]>();
        foreach (var segment1 in GetLineSegments(path1))
        {
            foreach(var segment2 in GetLineSegments(path2))
            {
                Vector2[] intersection = GetIntersection(segment1, segment2);
                if (intersection != null)
                {
                    toReturn.Add(intersection);
                }
            }
        }
        return toReturn;
    }

    private Vector2[] GetIntersection(Tuple<Vector2, Vector2> segment1, Tuple<Vector2, Vector2> segment2)
    {
        double deltaX1 = segment1.First.x - segment1.Second.x;
        double slope1 = deltaX1 == 0 ? double.PositiveInfinity : (segment1.First.y - segment1.Second.y) / (deltaX1);
        double deltaX2 = segment2.First.x - segment2.Second.x;
        double slope2 = deltaX2 == 0 ? double.PositiveInfinity : (segment2.First.y - segment2.Second.y) / deltaX2;
        double h1 = slope1 == double.PositiveInfinity ? segment1.First.x : segment1.First.y - slope1 * segment1.First.x;
        double h2 = slope2 == double.PositiveInfinity ? segment2.First.x : segment2.First.y - slope2 * segment2.First.x;
        if (slope1 == slope2 && h1 == h2)
        {
            // Identical lines defined by the segments. Find if the segments overlap.
            double segment1MinCoordinate = slope1 == double.PositiveInfinity ? Math.Min(segment1.First.y, segment1.Second.y) : Math.Min(segment1.First.x, segment1.Second.x);
            double segment1MaxCoordinate = slope1 == double.PositiveInfinity ? Math.Max(segment1.First.y, segment1.Second.y) : Math.Max(segment1.First.x, segment1.Second.x);
            double segment2MinCoordinate = slope2 == double.PositiveInfinity ? Math.Min(segment2.First.y, segment2.Second.y) : Math.Min(segment2.First.x, segment2.Second.x);
            double segment2MaxCoordinate = slope2 == double.PositiveInfinity ? Math.Max(segment2.First.y, segment2.Second.y) : Math.Max(segment2.First.x, segment2.Second.x);
            double coordinateStart = Math.Max(segment1MinCoordinate, segment2MinCoordinate);
            double coordinateEnd = Math.Min(segment1MaxCoordinate, segment2MaxCoordinate);
            if (coordinateEnd < coordinateStart)
            {
                return null;
            }
            if (slope1 == double.PositiveInfinity) return new Vector2[] { new Vector2(segment2.First.x, (float)coordinateStart), new Vector2(segment2.First.x, (float)(coordinateEnd)) };
            else return new Vector2[] { new Vector2((float)coordinateStart, (float)(slope1*coordinateStart + h1)), new Vector2((float)(coordinateEnd), (float)(slope1 * coordinateEnd + h1))};
        }
        else
        {
            //HACK: so apparently when they are connected only by a point this gives shit results.
            return null;
            if (slope1 == slope2) return null;
            double xIntersection;
            double yIntersection;
            if (slope1 == double.PositiveInfinity || slope2 == double.PositiveInfinity)
            {
                 xIntersection = slope1 == double.PositiveInfinity ? segment1.First.x : segment2.First.x;
                yIntersection = slope1 == double.PositiveInfinity ? slope2 * xIntersection + h2 : slope1 * xIntersection + h1;
            }
            else
            {
                xIntersection = (h2 - h1) / (slope1 - slope2);
                yIntersection = slope1 * xIntersection + h1;
            }
            double firstMinX= Math.Min(segment1.First.x, segment1.Second.x);
            double firstMaxX = Math.Max(segment1.First.x, segment1.Second.x);
            double secondMinX = Math.Min(segment2.First.x, segment2.Second.x);
            double secondMaxX = Math.Max(segment2.First.x, segment2.Second.x);
            if (xIntersection >= firstMinX && xIntersection >= secondMinX && xIntersection <= firstMaxX && xIntersection <= secondMaxX)
                return new Vector2[] { new Vector2((float)(xIntersection), (float)(yIntersection)) };
            else return null;
        }
    }

    public Vector2[] GetPath(Vector2 originPoint, Vector2 targetPoint)
    {
        ColliderGraphNode sourceColliderNode = null;
        ColliderGraphNode endColliderNode = null;
        foreach (var node in colliderGraph)
        {
            node.Visited = false;
            if (node.Collider.OverlapPoint(originPoint))
            {
                sourceColliderNode = node;
            }
            if (node.Collider.OverlapPoint(targetPoint))
            {
                endColliderNode = node;
            }
        }
        if (sourceColliderNode == null) throw new ArgumentException("Origin point not in a walkable area");
        if (endColliderNode == null) throw new ArgumentException("Target point not in a walkable area");
        if (sourceColliderNode == endColliderNode) return new Vector2[] { targetPoint };
        Queue<SearchNode> bfsQueue = new Queue<SearchNode>();
        bfsQueue.Enqueue(new SearchNode() { CurrentColliderNode = endColliderNode, PreviousNode = null });
        SearchNode currentNode;
        SearchNode result = null;
        while (bfsQueue.Any())
        {
            currentNode = bfsQueue.Dequeue();
            if (currentNode.CurrentColliderNode == sourceColliderNode)
            {
                result = currentNode;
                break;
            }
            currentNode.CurrentColliderNode.Visited = true;
            foreach (var neighbour in currentNode.CurrentColliderNode.Neighbours)
            {
                if (!neighbour.NeighbourNode.Visited)
                {
                    bfsQueue.Enqueue(new SearchNode() { CurrentColliderNode = neighbour.NeighbourNode, PreviousNode = currentNode });
                }
            }
        }
        if (result == null) throw new ArgumentException("Source and Target colliders are not connected in any way");
        Vector2 lastPosition = new Vector2(originPoint.x, originPoint.y);
        currentNode = result;
        List<List<Vector2>> intersectionGraph = new List<List<Vector2>>();
        while (currentNode.PreviousNode != null)
        {
            intersectionGraph.Add(new List<Vector2>());
            var currentIntersectionList = intersectionGraph.Last();
            var intersections = currentNode.CurrentColliderNode.Neighbours.Where((p) => p.NeighbourNode == currentNode.PreviousNode.CurrentColliderNode).First().Intersections;
            foreach (var intersection in intersections)
            {
                if (intersection.Length == 1)
                {
                    currentIntersectionList.Add(intersection[0]);
                }
                else if (intersection.Length == 2)
                {
                    var intersectionWithStraightLine = GetIntersection(new Tuple<Vector2, Vector2>(lastPosition, targetPoint), new Tuple<Vector2, Vector2>(intersection[0], intersection[1]));
                    if (intersectionWithStraightLine != null)
                    {
                       currentIntersectionList.Add(intersectionWithStraightLine[0]);
                    }
                    var delta = (intersection[1] - intersection[0]) / 16;
                    var currentIntersection = intersection[0];
                    for (int i = 0; i < 17; ++i)
                    {
                        currentIntersectionList.Add(currentIntersection);
                        currentIntersection += delta;
                    }
                }
                else
                {
                    throw new Exception("Unexpected intersection");
                }
            }
            currentNode = currentNode.PreviousNode;
        }
        intersectionGraph.Add(new List<Vector2>() { targetPoint });
        return GetBestPath(intersectionGraph, originPoint).ToArray();
    }
    List<Vector2> GetBestPath(List<List<Vector2>> intersectionGraph, Vector2 startPoint)
    {
        float currentBestDistance = float.MaxValue;
        List<Vector2> currentBestResult = null;
        int graphIndex = 0;
        foreach (var intersection in intersectionGraph[graphIndex])
        {
            var currentResult = GetBestPathRecursive(intersectionGraph, graphIndex + 1);
            float distance = currentResult.First == null ? currentResult.Second : currentResult.Second + Vector2.Distance(intersection, currentResult.First.Last());
            distance += Vector2.Distance(startPoint, intersection);
            if (distance < currentBestDistance)
            {
                currentBestDistance = distance;
                currentBestResult = currentResult.First == null ? new List<Vector2>() : currentResult.First;
                currentBestResult.Add(intersection);
            }
        }
        currentBestResult.Reverse();
        return currentBestResult;
    }
    Tuple<List<Vector2>,float> GetBestPathRecursive(List<List<Vector2>> intersectionGraph, int graphIndex)
    {
        if (graphIndex >= intersectionGraph.Count)
        {
            return new Tuple<List<Vector2>, float>(null, 0);
        }
        else
        {
            float currentBestDistance = float.MaxValue;
            List<Vector2> currentBestResult = null;
            foreach (var intersection in intersectionGraph[graphIndex])
            {
                var currentResult = GetBestPathRecursive(intersectionGraph, graphIndex + 1);
                float distance = currentResult.First == null ? currentResult.Second :  currentResult.Second + Vector2.Distance(intersection, currentResult.First.Last());
                if (distance < currentBestDistance)
                {
                    currentBestDistance = distance;
                    currentBestResult = currentResult.First == null ? new List<Vector2>() : currentResult.First;
                    currentBestResult.Add(intersection);
                }
            }
            return new Tuple<List<Vector2>, float>(currentBestResult, currentBestDistance);
        }
    }
}