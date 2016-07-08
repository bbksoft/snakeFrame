using UnityEngine;


public class PointDraw : MonoBehaviour {

    public Color[] tColors;
    public float uWidth = 1.0f;
    public float drawWidth = 0.5f;

    public bool _debug;

    MeshFilter m;

    void Awake () {
        m = GetComponent<MeshFilter>();
	}
	
    void Update()
    {
        if (_debug)
        {
            PointData[] datas = new PointData[100];

            for (int pos = 0; pos < datas.Length; pos++)
            {
                datas[pos] = new PointData();
                datas[pos].type = pos;
                datas[pos].pos = new Vector2(pos / 10, (pos % 10));               
            }

            SetDatas(datas);
        }
    }

	public void SetDatas(PointData[] datas) {

        Mesh mesh = new Mesh();

        int count = datas.Length;

        Vector3[] vertices = new Vector3[count*4];
        Vector2[] UV = new Vector2[count * 4];
        Color[] colors = new Color[count * 4];

        int[] triangles = new int[count * 6];
     

        float dw = drawWidth / 2;
        float uw = uWidth;

        for (int pos = 0; pos < count; pos++)
        {
            PointData d = datas[pos];

            vertices[pos * 4 + 0] = new Vector3(d.pos.x * uw - dw, d.pos.y * uw - dw, 1);
            vertices[pos * 4 + 1] = new Vector3(d.pos.x * uw - dw, d.pos.y * uw + dw, 1);
            vertices[pos * 4 + 2] = new Vector3(d.pos.x * uw + dw, d.pos.y * uw + dw, 1);
            vertices[pos * 4 + 3] = new Vector3(d.pos.x * uw + dw, d.pos.y * uw - dw, 1);

            UV[pos * 4 + 0] = new Vector2(0, 0);
            UV[pos * 4 + 1] = new Vector2(0, 1);
            UV[pos * 4 + 2] = new Vector2(1, 1);
            UV[pos * 4 + 3] = new Vector2(1, 0);

            Color c = tColors[d.type % tColors.Length];

            colors[pos * 4 + 0] = c;
            colors[pos * 4 + 1] = c;
            colors[pos * 4 + 2] = c;
            colors[pos * 4 + 3] = c;

            triangles[pos * 6 + 0] = pos * 4 + 0;
            triangles[pos * 6 + 1] = pos * 4 + 1;
            triangles[pos * 6 + 2] = pos * 4 + 2;
            triangles[pos * 6 + 3] = pos * 4 + 2;
            triangles[pos * 6 + 4] = pos * 4 + 3;
            triangles[pos * 6 + 5] = pos * 4 + 0;
        }

        mesh.vertices = vertices;
        mesh.uv = UV;
        mesh.triangles = triangles;
        mesh.colors = colors;

        m.mesh = mesh;
    }
}
