using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HexGridChunk : MonoBehaviour {

	List<HexCell> cells;

	HexMesh hexMesh;
	Canvas gridCanvas;

	void Awake () {
		gridCanvas = GetComponentInChildren<Canvas>();
		hexMesh = GetComponentInChildren<HexMesh>();

		cells = new List<HexCell>(HexMetrics.chunkSizeX * HexMetrics.chunkSizeZ);
		ShowUI(false);
	}

	public void AddCell (HexCell cell) {
		//cells[index] = cell;
		cells.Add(cell);
		cell.chunk = this;
		cell.transform.SetParent(transform, false);
		cell.uiRect.SetParent(gridCanvas.transform, false);
	}

	public void Refresh () {
		enabled = true;
	}

	public void ShowUI (bool visible) {
		gridCanvas.gameObject.SetActive(visible);
	}

	void LateUpdate () {
		hexMesh.Triangulate(cells.ToArray());
		enabled = false;
	}
}