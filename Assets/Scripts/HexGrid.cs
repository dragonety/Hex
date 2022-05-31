using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HexGrid : MonoBehaviour {

	public int chunkCountX = 4, chunkCountZ = 3;

	public Color defaultColor = Color.white;

	public HexCell cellPrefab;
	public Text cellLabelPrefab;
	public HexGridChunk chunkPrefab;

	public Texture2D noiseSource;

	private Dictionary<Vector2Int, HexGridChunk> dictChunk;
	private Dictionary<HexCoordinates, HexCell> dictCell;

	int cellCountX, cellCountZ;

	[SerializeField]
	private Transform labelParent;

	void Awake () {
		HexMetrics.noiseSource = noiseSource;

		cellCountX = chunkCountX * HexMetrics.chunkSizeX;
		cellCountZ = chunkCountZ * HexMetrics.chunkSizeZ;

		for (int i = -10; i < 10; i++)
		{
			var a = i / 3;
			var b = Mathf.CeilToInt((float) i / 3);
			Debug.LogErrorFormat("HERE:{0}:{1}:{2}", i, a, b);
		}
		
		CreateChunks();
		CreateCells();
	}

	void CreateChunks () {
		dictChunk = new Dictionary<Vector2Int, HexGridChunk>();

		for (int z = -chunkCountZ + 1; z <= chunkCountZ; z++) {
			for (int x = -chunkCountX + 1; x <= chunkCountX; x++) {
				HexGridChunk chunk = Instantiate(chunkPrefab, transform, true);
				dictChunk.Add(new Vector2Int(x, z), chunk);
			}
		}
	}

	void CreateCells () {
		dictCell = new Dictionary<HexCoordinates, HexCell>();

		for (int z = -cellCountZ + 1; z <= cellCountZ; z++) {
			for (int x = -cellCountX + 1; x <= cellCountX; x++) {
				CreateCell(x, z);
			}
		}
	}

	void OnEnable () {
		HexMetrics.noiseSource = noiseSource;
	}

	public HexCell GetCell (Vector3 position) {
		position = transform.InverseTransformPoint(position);
		HexCoordinates coord = HexUtils.PositionToCoordinate(position);
		return GetCell(coord);
	}

	public HexCell GetCell (HexCoordinates coordinates) {
		if (dictCell.TryGetValue(coordinates, out HexCell value))
		{
			return value;
		}
		else
		{
			return null;
		}
	}

	public void ShowUI (bool visible) {
		foreach (var chunk in dictChunk.Values)
		{
			chunk.ShowUI(visible);
		}
	}

	void CreateCell (int x, int z) {
		HexCell cell = Instantiate<HexCell>(cellPrefab);
		Vector3 position = HexUtils.OffsetToPosition(x, z);
		cell.transform.localPosition = position;
		HexCoordinates coord = HexUtils.OffsetToCoordinate(x, z);
		cell.coordinates = coord;
		cell.Color = defaultColor;
		
		dictCell.Add(coord, cell);

		for (int i = 0; i < 6; i++)
		{
			HexCoordinates tarCoord = coord.Get((HexDirection)i);
			HexCell tarCell = GetCell(tarCoord);
			if (tarCell)
			{
				cell.SetNeighbor((HexDirection)i, tarCell);
			}
		}

		Text label = Instantiate<Text>(cellLabelPrefab);
		label.rectTransform.anchoredPosition =
			new Vector2(position.x, position.z);
		label.text = cell.coordinates.ToStringOnSeparateLines();
		cell.uiRect = label.rectTransform;

		cell.Elevation = 0;

		AddCellToChunk(x, z, cell);
	}

	void AddCellToChunk (int x, int z, HexCell cell) {
		int chunkX = Mathf.CeilToInt((float)x / HexMetrics.chunkSizeX);
		int chunkZ = Mathf.CeilToInt((float)z / HexMetrics.chunkSizeZ);
		if (dictChunk.TryGetValue(new Vector2Int(chunkX, chunkZ), out HexGridChunk chunk))
		{
			int localX = x - chunkX * HexMetrics.chunkSizeX + x >= 0 ? 0 : HexMetrics.chunkSizeX;
			//int localX = Mathf.Abs(x % HexMetrics.chunkSizeX);
			int localZ = z - chunkZ * HexMetrics.chunkSizeZ + z >= 0 ? 0 : HexMetrics.chunkSizeZ;
			//int localZ = Mathf.Abs(z % HexMetrics.chunkSizeZ);
			Debug.LogErrorFormat("x:{0},z:{1},cx:{2},cz:{3},lx:{4},lz:{5}",x,z,chunkX,chunkZ,localX, localZ);
			chunk.AddCell(cell);
		}
		else
		{
			Debug.LogErrorFormat("X:{0}, Z:{1}", x, z);
		}

		
	}
}