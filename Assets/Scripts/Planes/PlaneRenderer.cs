using UnityEngine;
using System.Collections.Generic;

public class PlaneRenderer : MonoBehaviour
{
    public Mesh quadMesh;
    public Material instancedMat;

    const int BATCH = 1023;

    List<Matrix4x4> matrices = new List<Matrix4x4>();
    List<Vector4> colors = new List<Vector4>();

    public void Render(List<PlaneRenderData> planes)
    {
        if (planes == null || planes.Count == 0) return;

        matrices.Clear();
        colors.Clear();

        foreach (var p in planes)
        {
            var m = Matrix4x4.TRS(
                p.position,
                Quaternion.Euler(0, 0, p.rotation),
                Vector3.one * p.scale
            );

            matrices.Add(m);
            colors.Add(p.color);
        }

        int index = 0;
        int total = matrices.Count;

        while (index < total)
        {
            int count = Mathf.Min(BATCH, total - index);
            var props = new MaterialPropertyBlock();
            props.SetVectorArray("_Color", colors.GetRange(index, count));

            Graphics.DrawMeshInstanced(
                quadMesh,
                0,
                instancedMat,
                matrices.GetRange(index, count).ToArray(),
                count,
                props,
                UnityEngine.Rendering.ShadowCastingMode.Off,
                false
            );

            index += count;
        }
    }
}
