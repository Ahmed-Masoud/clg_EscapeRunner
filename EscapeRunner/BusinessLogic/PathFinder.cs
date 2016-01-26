using EscapeRunner.View;
using System;
using System.Collections.Generic;
using System.Drawing;

namespace EscapeRunner.BusinessLogic
{
    enum NodeState
    {
        Untested,//isn't tested Yet
        Open,//taken in consideration while finding the path(monster can use it)
        Closed//tested before and the path failed so it will not be used again
    }

    public class RouteInformation
    {
        public IndexPair StartLocationIndex { get; set; }
        public IndexPair EndLocationIndex { get; set; }
        public RouteInformation(LevelTile startLocation, LevelTile endLocation)
        {
            StartLocationIndex = startLocation.TileIndecies;
            EndLocationIndex = endLocation.TileIndecies;
        }
        public RouteInformation(IndexPair startLocation, IndexPair endLocation)
        {
            StartLocationIndex = startLocation;
            EndLocationIndex = endLocation;
        }
    }

    class Node
    {
        public NodeState State { get; set; }
        public bool walkable;
        public IndexPair destinationLocation;
        public IndexPair Location { get; set; }
        private Node parentNode;
        public Node ParentNode
        {
            get { return parentNode; }
            set
            {
                parentNode = value;
                G = parentNode.G + GetTraversalCost(Location, parentNode.Location);
                F = G + H;
            }
        }
        public float F { get; set; }
        public float G { get; set; }
        public float H { get; set; }
        public Node(IndexPair destination, IndexPair index)
        {
            if (MapLoader.IsWalkable(index))
                walkable = true;
            else walkable = false;
            destinationLocation = destination;
            State = NodeState.Untested;
            Location = index;
            G = 0;
            H = GetTraversalCost(index, destination);
        }
        internal static float GetTraversalCost(IndexPair location, IndexPair otherLocation)
        {
            float deltaX = otherLocation.I - location.I;
            float deltaY = otherLocation.J - location.J;
            return (float)Math.Sqrt(deltaX * deltaX + deltaY * deltaY);
        }

    }

    class PathFinder
    {
        private RouteInformation informations;
        private Node startNode;
        private Node endNode;
        private IndexPair levelSize;
        public Node[,] Map { get; set; }
        public PathFinder(RouteInformation informations)
        {
            levelSize = MapLoader.LevelDimensions;
            this.informations = informations;
            initializeMap();
            startNode = Map[informations.StartLocationIndex.I, informations.StartLocationIndex.J];
            startNode.State = NodeState.Open;
            endNode = Map[informations.EndLocationIndex.I, informations.EndLocationIndex.J];
        }
        public void initializeMap()
        {
            Map = new Node[levelSize.I, levelSize.J];
            for (int i = 0; i < levelSize.I; i++)
            {
                for (int j = 0; j < levelSize.J; j++)
                {
                    Map[i, j] = new Node(informations.EndLocationIndex, new IndexPair(i, j));
                }
            }
        }
        public List<IndexPair> FindPath()
        {
            // The start node is the first entry in the 'open' list
            List<IndexPair> path = new List<IndexPair>();
            bool success = Search(startNode);
            if (success)
            {
                // If a path was found, follow the parents from the end node to build a list of locations
                Node node = this.endNode;
                while (node.ParentNode != null)
                {
                    path.Add(node.Location);
                    node = node.ParentNode;
                }

                // Reverse the list so it's in the correct order when returned
                path.Reverse();
            }
            return Minimize(path);
        }
        /// <summary>
        /// returns true if(the path leads to the destination and false if the path leads to dead end
        /// </summary>
        /// <param name="currentNode"></param>
        /// <returns></returns>
        private bool Search(Node currentNode)
        {
            // Set the current node to Closed since it cannot be traversed more than once
            currentNode.State = NodeState.Closed;
            List<Node> nextNodes = GetAdjacentWalkableNodes(currentNode);

            // Sort by F-value so that the shortest possible routes are considered first
            nextNodes.Sort((node1, node2) => node1.F.CompareTo(node2.F));
            foreach (var nextNode in nextNodes)
            {
                // Check whether the end node has been reached
                if (nextNode.Location == this.endNode.Location)
                {
                    return true;
                }
                else
                {
                    // If not, check the next set of nodes
                    if (Search(nextNode)) // Note: Recurses back into Search(Node)
                        return true;
                }
            }

            // The method returns false if this path leads to be a dead end
            return false;
        }
        private List<Node> GetAdjacentWalkableNodes(Node fromNode)
        {
            List<Node> walkableNodes = new List<Node>();
            IEnumerable<IndexPair> nextLocations = GetAdjacentLocations(fromNode.Location);
            foreach (var location in nextLocations)
            {
                int x = location.I;
                int y = location.J;

                // Stay within the grid's boundaries
                if (x < 0 || x >= levelSize.I || y < 0 || y >= levelSize.J)
                    continue;

                Node node = Map[x, y];
                // Ignore non-walkable nodes
                if (!node.walkable)
                    continue;

                // Ignore already-closed nodes
                if (node.State == NodeState.Closed)
                    continue;

                // Already-open nodes are only added to the list if their G-value is lower going via this route.
                if (node.State == NodeState.Open)
                {
                    float traversalCost = Node.GetTraversalCost(node.Location, node.ParentNode.Location);
                    float gTemp = fromNode.G + traversalCost;
                    if (gTemp < node.G)
                    {
                        node.ParentNode = fromNode;
                        walkableNodes.Add(node);
                    }
                }
                else
                {
                    // If it's untested, set the parent and flag it as 'Open' for consideration
                    node.ParentNode = fromNode;
                    node.State = NodeState.Open;
                    walkableNodes.Add(node);
                }
            }
            return walkableNodes;

        }
        private static IEnumerable<IndexPair> GetAdjacentLocations(IndexPair fromLocation)
        {
            return new IndexPair[]
            {
                new IndexPair(fromLocation.I-1, fromLocation.J  ),
                new IndexPair(fromLocation.I,   fromLocation.J+1),
                new IndexPair(fromLocation.I+1, fromLocation.J  ),
                new IndexPair(fromLocation.I,   fromLocation.J-1)
            };
        }
        private static List<IndexPair> Minimize(List<IndexPair> path)
        {
            int temp = 0;
            for (int i = 0; i < path.Count; i++)
            {
                for (int j = i + 1; j < path.Count; j++)
                {
                    if (IsShortCut(path[i], path[j]))
                        temp = j;
                }
                if (temp != 0)
                {
                    path = InsertList(path, GetStraighPath(path[i], path[temp]), i, temp);
                    temp = 0;
                }
            }
            return path;
        }
        private static bool IsShortCut(IndexPair firstIndex, IndexPair lastIndex)
        {
            if (firstIndex.I != lastIndex.I && firstIndex.J != lastIndex.J) return false;
            if (firstIndex == lastIndex) return false;
            int I = firstIndex.I, J = firstIndex.J, delta;
            if (firstIndex.I == lastIndex.I)
            {
                if (firstIndex.J > lastIndex.J)
                    delta = -1;
                else delta = 1;
                while (J != lastIndex.J)
                {
                    J += delta;
                    if (!MapLoader.IsWalkable(new IndexPair(I, J)))
                        return false;
                }
                return true;
            }
            else if (firstIndex.J == lastIndex.J)
            {
                if (firstIndex.I > lastIndex.I)
                    delta = -1;
                else delta = 1;
                while (I != lastIndex.I)
                {
                    I += delta;
                    if (!MapLoader.IsWalkable(new IndexPair(I, J)))
                        return false;
                }
                return true;
            }
            return false;
        }
        private static List<IndexPair> GetStraighPath(IndexPair firstIndex, IndexPair lastIndex)
        {
            List<IndexPair> newPath = new List<IndexPair>();
            int I = firstIndex.I, J = firstIndex.J, delta;
            if (firstIndex.I == lastIndex.I)
            {
                if (firstIndex.J > lastIndex.J)
                    delta = -1;
                else delta = 1;
                newPath.Add(firstIndex);
                while (J != lastIndex.J)
                {
                    J += delta;
                    newPath.Add(new IndexPair(I, J));
                }
            }
            else if (firstIndex.J == lastIndex.J)
            {
                if (firstIndex.I > lastIndex.I)
                    delta = -1;
                else delta = 1;
                newPath.Add(firstIndex);
                while (I != lastIndex.I)
                {
                    I += delta;
                    newPath.Add(new IndexPair(I, J));
                }
            }
            return newPath;
        }
        private static List<IndexPair> InsertList(List<IndexPair> insertIn, List<IndexPair> toBeInserted, int startIndex, int lastIndex)
        {
            if (lastIndex < startIndex) return null;
            if (startIndex > insertIn.Count || lastIndex > insertIn.Count) return null;
            List<IndexPair> newList = new List<IndexPair>();
            for (int i = 0; i < startIndex; i++)
            {
                newList.Add(insertIn[i]);
            }
            for (int i = 0; i < toBeInserted.Count; i++)
            {
                newList.Add(toBeInserted[i]);
            }
            for (int i = lastIndex + 1; i < insertIn.Count; i++)
            {
                newList.Add(insertIn[i]);
            }
            return newList;
        }
    }
}


