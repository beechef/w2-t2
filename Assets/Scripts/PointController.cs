using Assets.Scripts;
using UnityEngine;

public class PointController : MonoBehaviour
{
    public GameObject pointPrefab;
    //public Point[] Points;
    public Vector3[] Positions;

    public Color lineColor;

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
        Gizmos.color = lineColor;
        int nextLineIndex = 0;
        for (int i = 0; i < Positions.Length; i++)
        {
            if (i + 1 >= Positions.Length) nextLineIndex = 0;
            else nextLineIndex++;
            Gizmos.DrawLine(Positions[i], Positions[nextLineIndex]);
        }
    }
}
