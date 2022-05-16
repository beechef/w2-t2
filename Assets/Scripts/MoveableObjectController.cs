using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveableObjectController : MonoBehaviour
{
    [SerializeField]
    private PointController pointController;

    public Color dirColor;
    //private Point[] points;
    //private Vector3[] PosPoints;

    public float MoveSpeed;
    public float Diff;

    public int CurrentPointIndex;

    void Start()
    {
        //points = pointController.Points;
    }

    void Update()
    {
        if (isValidateIndex())
        {
            float distance = Vector3.Distance(pointController.Positions[CurrentPointIndex], transform.position);
            if (distance <= Diff)
            {
                if (CurrentPointIndex + 1 >= pointController.Positions.Length)
                {
                    CurrentPointIndex = 0;
                }
                else
                {
                    CurrentPointIndex++;
                }
            }
            moveToPoint();
        }
    }
    private void OnDrawGizmosSelected()
    {
        drawDirection();
    }
    bool isValidateIndex()
    {
        return (CurrentPointIndex >= 0 && CurrentPointIndex < pointController.Positions.Length);
    }
    void moveToPoint()
    {
        Vector3 direction = (pointController.Positions[CurrentPointIndex] - transform.position).normalized;
        transform.Translate(direction * MoveSpeed * Time.deltaTime);
    }
    void drawDirection()
    {
        Gizmos.color = dirColor;
        if (isValidateIndex())
        {
            Gizmos.DrawLine(transform.position, pointController.Positions[CurrentPointIndex]);
        }
    }
}
