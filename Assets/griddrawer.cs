using UnityEngine;

[ExecuteAlways]
public class RuntimeGrid : MonoBehaviour
{
    public int gridWidth = 20;
    public int gridHeight = 20;
    public float cellSize = 1f;

    public Material gridMaterial;
    public Material axisMaterial;
    public Color gridColor = Color.gray;
    public Color axisColor = Color.white;
    public Font labelFont;
    public int fontSize = 20;

    [Header("Sorting")]
    public string sortingLayerName = "GridBackground";  // Add this sorting layer in Unity

    private void Start()
    {
        DrawGrid();
    }

    private void OnValidate()
    {
        if (Application.isPlaying) DrawGrid();
    }

    void DrawGrid()
    {
        // Clear old lines and labels
        foreach (Transform child in transform)
        {
            DestroyImmediate(child.gameObject);
        }

        for (int x = -gridWidth; x <= gridWidth; x++)
        {
            bool isAxis = (x == 0);
            CreateLine(
                new Vector3(x * cellSize, -gridHeight * cellSize, 0),
                new Vector3(x * cellSize, gridHeight * cellSize, 0),
                isAxis ? axisColor : gridColor,
                isAxis ? axisMaterial : gridMaterial
            );

            if (x != 0) CreateLabel(new Vector3(x * cellSize, -0.3f * cellSize, 0), x.ToString());
        }

        for (int y = -gridHeight; y <= gridHeight; y++)
        {
            bool isAxis = (y == 0);
            CreateLine(
                new Vector3(-gridWidth * cellSize, y * cellSize, 0),
                new Vector3(gridWidth * cellSize, y * cellSize, 0),
                isAxis ? axisColor : gridColor,
                isAxis ? axisMaterial : gridMaterial
            );

            if (y != 0) CreateLabel(new Vector3(0.3f * cellSize, y * cellSize, 0), y.ToString());
        }
    }

    void CreateLine(Vector3 start, Vector3 end, Color color, Material material)
    {
        GameObject lineObj = new GameObject("GridLine");
        lineObj.transform.parent = transform;

        LineRenderer lr = lineObj.AddComponent<LineRenderer>();
        lr.positionCount = 2;
        lr.SetPositions(new Vector3[] { start, end });
        lr.startWidth = 0.02f;
        lr.endWidth = 0.02f;
        lr.material = material;
        lr.startColor = color;
        lr.endColor = color;
        lr.useWorldSpace = true;

        lr.sortingLayerName = sortingLayerName;
        lr.sortingOrder = 0;
    }

    void CreateLabel(Vector3 position, string text)
    {
        GameObject label = new GameObject("Label_" + text);
        label.transform.parent = transform;
        label.transform.position = position;

        TextMesh tm = label.AddComponent<TextMesh>();
        tm.text = text;
        tm.fontSize = fontSize;
        tm.characterSize = 0.1f;
        tm.anchor = TextAnchor.MiddleCenter;
        tm.font = labelFont;
        tm.color = Color.white;

        if (labelFont != null)
        {
            var renderer = tm.GetComponent<MeshRenderer>();
            renderer.material = labelFont.material;
            renderer.sortingLayerName = sortingLayerName;
            renderer.sortingOrder = 0;
        }
    }
}
