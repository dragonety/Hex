using UnityEngine;

/*
 *..............................
 *...............*..............
 *.......NW...*******...NE......
 *..........***********.........
 *......W...***********...E.....
 *..........***********.........
 *.......SW...*******...SE......
 *...............*..............
 *..............................
 *..............................
 */

public enum HexDirection {
	NE = 0, 
	E  = 1, 
	SE = 2, 
	SW = 3, 
	W  = 4, 
	NW = 5
}

[System.Serializable]
public struct HexCoordinates 
{

	[SerializeField]
	private int x, z;

	public int X => x;

	public int Z => z;

	public int Y => -X - Z;

	public HexCoordinates (int x, int z) 
	{
		this.x = x;
		this.z = z;
	}

	public override string ToString ()
	{
		return "(" + X + ", " + Y + ", " + Z + ")";
	}

	public string ToStringOnSeparateLines ()
	{
		return X + "\n" + Y + "\n" + Z;
	}

	public HexCoordinates Get(HexDirection direction)
	{
		switch (direction)
		{
			case HexDirection.NE:
				return new HexCoordinates(x, z + 1);
			case HexDirection.E:
				return new HexCoordinates(x + 1, z);
			case HexDirection.SE:
				return new HexCoordinates(x + 1, z - 1);
			case HexDirection.SW:
				return new HexCoordinates(x, z - 1);
			case HexDirection.W:
				return new HexCoordinates(x - 1, z);
			case HexDirection.NW:
				return new HexCoordinates(x - 1, z + 1);
			default:
				return new HexCoordinates(x, z);
		}
	}
	
	public Vector3Int ToVector()
	{
		return new Vector3Int(X, Y, Z);
	}

	#region Override

	public static bool operator ==(HexCoordinates lhs, HexCoordinates rhs) => lhs.x == rhs.x && lhs.z == rhs.z;
	
	public static bool operator !=(HexCoordinates lhs, HexCoordinates rhs) => !(lhs == rhs);
	
	public bool Equals(HexCoordinates other) => this == other;
	
	public override bool Equals(object other)
	{
		if (other is HexCoordinates other1)
		{
			return this.Equals(other1);
		}

		if (other is Vector2Int other2)
		{
			return this.x == other2.x && this.z == other2.y;
		}

		if (other is Vector3Int other3)
		{
			return this.x == other3.x && this.Y == other3.y && this.z == other3.z;
		}

		return false;
	}

	public override int GetHashCode()
	{
		int num1 = this.x;
		int hashCode = num1.GetHashCode();
		num1 = this.z;
		int num2 = num1.GetHashCode() << 2;
		return hashCode ^ num2;
	}

	#endregion
	
	
}