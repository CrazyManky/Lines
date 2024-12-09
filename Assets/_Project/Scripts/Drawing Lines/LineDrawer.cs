using System.Collections.Generic;
using _Project.Scripts.GameElements;
using UnityEngine;

namespace _Project.Scripts.Drawing_Lines
{
    public class LineDrawer : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer _spriteRenderer;
        [SerializeField] private LineRenderer lineRenderer;
        [SerializeField] private CircleNode startNode;
        [SerializeField] private CircleNode endNode;
        [SerializeField] private List<CircleNode> _nodesAll;
        [SerializeField] private List<MandatoryCircleNode> _mandatoryNodesAll;
        [SerializeField] private List<ZeroNode> _zeroNodes;
 
        public CircleNode StartNode => startNode;
        public CircleNode EndNode => endNode;

        private readonly List<Vector3> points = new();

        public void StartDrawing(Vector2 startPoint)
        {
            points.Clear();
            AddPoint(startPoint);
        }

        public void AddSegment(Vector2 startPoint, Vector2 endPoint)
        {
            if (!points.Contains(startPoint))
                AddPoint(startPoint);

            AddPoint(endPoint);
        }

        public void FinishDrawing()
        {
            points.Clear();
            lineRenderer.positionCount = 0;
        }

        private void AddPoint(Vector2 point)
        {
            points.Add(point);
            lineRenderer.positionCount = points.Count;
            lineRenderer.SetPositions(points.ToArray());
        }

        public List<CircleNode> GetAllNodes()
        {
            return _nodesAll;
        }

        public List<MandatoryCircleNode> GetMandatoryCircleNodes()
        {
            return _mandatoryNodesAll;
        }

        public void LevelComplited()
        {
            _spriteRenderer.enabled = false;
            _nodesAll.ForEach((e) => Destroy(e.gameObject));
            _zeroNodes.ForEach((e) => Destroy(e.gameObject));
        }
    }
}