using UnityEngine;

namespace Assets.Scripts
{
    public class Point
    {
        public GameObject GOPoint { get; set; }
        [SerializeField]
        public Vector3 Position { get; set; }

        public Point(GameObject GOPoint, Vector3 Position)
        {
            this.GOPoint = GOPoint;
            this.Position = Position;
        }

        public void UpdatePosition(out Vector3 position)
        {
            position = GOPoint.transform.position;
            //Position = GOPoint.transform.position;
        }

        public void UpdateGOPoint(Vector3 position)
        {
            GOPoint.transform.position = position;
            Position = position;
        }
    }
}
