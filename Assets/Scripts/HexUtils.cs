

using UnityEngine;

public static class HexUtils
{
    public static int FloorZero(float origin, int divide = 1, float offset = 0)
    {
        if (origin < 0)
        {
            return Mathf.CeilToInt((origin - offset) / divide);
        }
        else
        {
            return Mathf.FloorToInt((origin + offset) / divide);
        }
    }

    public static int CeilZero(float origin, int divide = 1, float offset = 0)
    {
        if (origin < 0)
        {
            return Mathf.FloorToInt((origin - offset) / divide);
        }
        else
        {
            return Mathf.CeilToInt((origin + offset) / divide);
        }
    }
    
    //Offset
    //Coordinate
    //Position

    #region Offset to
    public static HexCoordinates OffsetToCoordinate(int x, int z) 
    {
        //return new HexCoordinates(x - z / 2, z);
        return new HexCoordinates(x - FloorZero(z,2), z);
    }

    public static HexCoordinates OffsetToCoordinate(Vector2Int offset)
    {
        return OffsetToCoordinate(offset.x, offset.y);
    }

    public static Vector3 OffsetToPosition(int x, int z)
    {
        return new Vector3(
            //(x + z * 0.5f - z / 2) * (HexMetrics.innerRadius * 2f),
            (x + z * 0.5f - FloorZero(z,2)) * (HexMetrics.innerRadius * 2f),
            0f,
            z * (HexMetrics.outerRadius * 1.5f)
        );
    }

    public static Vector3 OffsetToPosition(Vector2Int offset)
    {
        return OffsetToPosition(offset.x, offset.y);
    }
    #endregion

    #region Coordinate to
    public static Vector2Int CoordinateToOffset(HexCoordinates coord)
    {
        return new Vector2Int(coord.X + coord.Z / 2, coord.Z);
    }
    
    public static Vector3 CoordinateToPosition(HexCoordinates coord)
    {
        Vector2Int offset = CoordinateToOffset(coord);
        return OffsetToPosition(offset);
    }
    #endregion

    #region Position to
    public static HexCoordinates PositionToCoordinate(Vector3 position) 
    {
        float x = position.x / (HexMetrics.innerRadius * 2f);
        float y = -x;

        float offset = position.z / (HexMetrics.outerRadius * 3f);
        x -= offset;
        y -= offset;

        int iX = Mathf.RoundToInt(x);
        int iY = Mathf.RoundToInt(y);
        int iZ = Mathf.RoundToInt(-x -y);

        if (iX + iY + iZ != 0) {
            float dX = Mathf.Abs(x - iX);
            float dY = Mathf.Abs(y - iY);
            float dZ = Mathf.Abs(-x -y - iZ);

            if (dX > dY && dX > dZ) {
                iX = -iY - iZ;
            }
            else if (dZ > dY) {
                iZ = -iX - iY;
            }
        }

        return new HexCoordinates(iX, iZ);
    }

    public static Vector2Int PositionToOffset(Vector3 position)
    {
        return CoordinateToOffset(PositionToCoordinate(position));
    }
    #endregion
    
    #region Extensions

    public static HexDirection Opposite (this HexDirection direction) {
        return (int)direction < 3 ? (direction + 3) : (direction - 3);
    }

    public static HexDirection Previous (this HexDirection direction) {
        return direction == HexDirection.NE ? HexDirection.NW : (direction - 1);
    }

    public static HexDirection Next (this HexDirection direction) {
        return direction == HexDirection.NW ? HexDirection.NE : (direction + 1);
    }

    #endregion
}