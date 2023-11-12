using System;
using System.Collections.Generic;
using UnityEngine;

public class Route : MonoBehaviour
{
   public int Distance(Position pos1, Position pos2) {
        if (pos1 == null || pos2 == null || pos1 == pos2) {
            return 0;
        }
        if (pos1.leftUp == pos2.leftUp && pos1.rightUp == pos2.rightUp) {
            return 0;
        }
        int luDistance = pos1.leftUp - pos2.leftUp;
        int ruDistance = pos1.rightUp - pos2.rightUp;
        if (luDistance * ruDistance <= 0) {
            return Math.Abs(luDistance) + Math.Abs(ruDistance);
        } else {
            return Math.Max(Math.Abs(luDistance) , Math.Abs(ruDistance));
        }
    }
    public List<Position> OneAStart(Position begin, Position end) {
        List<Position> route = new List<Position>();
        if (!end.Walkable()) {
            return route;
        }
        route.Add(begin);
        
        return route;
    }
    public List<List<Position>> MultyAStart(List<Position> begin, List<Position> end) {
        List<List<Position>> routes = new List<List<Position>>();
        return routes;
        
    }
}
public class Position {
    public int leftUp;
    public int rightUp;
    public bool walkable;
    public bool hasRole;
    public bool Walkable () {
        return walkable && !hasRole;
    }
}
