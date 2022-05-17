using Assets.Scripts;
using System;
using UnityEngine;
using UnityEditor;


public class PointController : MonoBehaviour
{
    //public GameObject pointPrefab;
    //public Point[] Points;
    public static readonly int OFFSET_HOTCONTROL = 10;

    public Vector3[] Positions;
    public int[] HotControls;

    public Color LineColor;
    public Color ObjectColor;

    public float ObjectScale;

    public float diff;

    public int SelectedPointIndex { get; set; }


    private Camera sceneCamera;


    void Start()
    {
        HotControls = new int[100];
        Array.Fill(HotControls, -1);
    }

    void Update()
    {

    }

    private void OnGUI()
    {
      
    }

    private void OnDrawGizmos()
    {
        sceneCamera = SceneView.currentDrawingSceneView.camera;
        Vector3 mousePosition = Event.current.mousePosition;
        float ppp = EditorGUIUtility.pixelsPerPoint;
        
       
        if (Event.current.isMouse && Event.current.button == 0)
        {
            Vector3 point = new Vector3();

            mousePosition.x *= ppp;
            mousePosition.y = sceneCamera.pixelHeight - mousePosition.y * ppp;

            point = sceneCamera.ScreenToWorldPoint(new Vector3(mousePosition.x, mousePosition.y, sceneCamera.nearClipPlane));

            Vector3 dir1;
            Vector3 dir2;

            Vector3 mPoint1;
            Vector3 mPoint2;
            Vector3 dirMPoint;

            float distance = 0f;

            for (int i = 0; i < Positions.Length; i++)
            {
                int nextLineIndex = 0;
                if (i + 1 >= Positions.Length) nextLineIndex = 0;
                else nextLineIndex = i + 1;
                dir1 = Positions[nextLineIndex] - Positions[i];
                dir2 = sceneCamera.transform.position - point;

                mPoint1 = Positions[i];
                mPoint2 = sceneCamera.transform.position;
                dirMPoint = mPoint2 - mPoint1;


                distance = Mathf.Abs(Vector3.Dot(Vector3.Cross(dir1, dir2), dirMPoint)) / absVector(Vector3.Cross(dir1, dir2));
                if (distance < diff)
                {

                }
            }
        }
        for (int i = 0; i < Positions.Length; i++)
        {
            drawConntectedLine(i);
            drawPoint(i);
        }
    }

    //public void DeletePosition()
    //{
    //    Vector3[] tmp = new Vector3[Positions.Length - 1];
    //    int count = 0;
    //    for (int i = 0; i < Positions.Length; i++)
    //    {
    //        if (SelectedPointIndex == i) continue;
    //        tmp[count++] = Positions[i];
    //    }
    //    Positions = tmp;
    //}

  
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

    private float absVector(Vector3 vector)
    {
        return (Mathf.Sqrt(Mathf.Pow(vector.x, 2) + Mathf.Pow(vector.y, 2) + Mathf.Pow(vector.z, 2)));
    }
}

[CustomEditor(typeof(PointController))]
public class MovePoint : Editor
{

    public int currentIndex;
    private void OnSceneGUI()
    {  
        var p = target as PointController;

        //p.SelectedPointIndex = getIndexHotControl();

        //if (Event.current.isKey && Event.current.keyCode == KeyCode.D)
        //{
        //    if (p.SelectedPointIndex != -1)
        //    {
        //        p.DeletePosition();
        //        GUIUtility.hotControl = -1;
        //    }
        //}

        for (int i = 0; i < p.Positions.Length; i++)
        {
            p.Positions[i] = Handles.PositionHandle(p.Positions[i], Quaternion.identity);

            float size = 0.5f;
            Vector3 handleDirection = Vector3.up;

            EditorGUI.BeginChangeCheck();
            //Vector3 newTargetPosition = Handles.Slider2D(i + 1, p.Positions[i], handleDirection, Vector3.right, Vector3.forward, size, Handles.SphereHandleCap, new Vector2(1,1));
            Vector3 newTargetPosition = Handles.FreeMoveHandle(i + PointController.OFFSET_HOTCONTROL, p.Positions[i], Quaternion.identity, size, Vector3.one, Handles.SphereHandleCap);
            if (EditorGUI.EndChangeCheck())
            {
                Undo.RecordObject(p, "Change Look At Target Position");
                p.Positions[i] = newTargetPosition;
            }
        }
    }
    //private int getIndexHotControl()
    //{
    //    if (GUIUtility.hotControl - PointController.OFFSET_HOTCONTROL >= 0 && GUIUtility.hotControl - PointController.OFFSET_HOTCONTROL < ((PointController) target).Positions.Length)
    //    {
    //        return GUIUtility.hotControl - PointController.OFFSET_HOTCONTROL;
    //    }
    //    return -1;
    //}
}

