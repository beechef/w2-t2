using Assets.Scripts;
using UnityEngine;

public class PointController : MonoBehaviour
{
    //public GameObject pointPrefab;
    //public Point[] Points;
    public Vector3[] Positions;

    public Color LineColor;
    public Color ObjectColor;

    public float ObjectScale;

    void Start()
    {

    }

    void Update()
    {
        //for (int i = 0; i < Points.Length; i++)
        //{
        //    //Points[i].UpdateGOPoint(Positions[i]);
        //}
    }

    private void OnDrawGizmos()
    {
        for (int i = 0; i < Positions.Length; i++)
        {
            drawConntectedLine(i);
            drawPoint(i);
        }
    }

    private void drawConntectedLine(int i)
    {
        Gizmos.color = LineColor;
        int nextLineIndex = 0;
        if (i + 1 >= Positions.Length) nextLineIndex = 0;
        else nextLineIndex = i + 1;
        Gizmos.DrawLine(Positions[i], Positions[nextLineIndex]);
    }
    private void drawPoint(int i)
    {
        Gizmos.color = ObjectColor;
        Gizmos.DrawSphere(Positions[i], ObjectScale);
        Gizmos.DrawIcon(Positions[i], "sv_icon_dot11_pix16_gizmo");
    }
}
