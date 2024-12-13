using System;
using System.Collections.Generic;
using System.Linq;
using _Project.Scripts.Drawing_Lines;
using _Project.Scripts.GameElements;
using Project.Screpts.Screens;
using SO;
using TMPro;
using UnityEngine;

namespace _Project.Scripts.Screens
{
    public class GameScreen : BaseScreen
    {
        [SerializeField] private LevelsConfig _levelsConfig;
        [SerializeField] private LineDrawer lineDrawerPrefab;
        [SerializeField] private GameObject _buttons;
        [SerializeField] private TextMeshProUGUI _levelID;

        private LineDrawer lineDrawerInstance;
        private NodeManager nodeManager;
        private bool isDrawing;

        private void Awake()
        {
            _buttons.gameObject.SetActive(false);
        }

        public override void Init()
        {
            base.Init();
            var prefabLevel = _levelsConfig.GetLevel();
            lineDrawerInstance = Instantiate(prefabLevel);
            nodeManager = new NodeManager(lineDrawerInstance);
            _levelID.text = $" <    {_levelsConfig.LevelIndex + 1}";
        }

        private void Update() => HandleInput();

        private void HandleInput()
        {
            if (Input.GetMouseButtonDown(0))
            {
                TryStartDrawing();
            }
            else if (Input.GetMouseButton(0))
            {
                if (isDrawing)
                    ContinueDrawing();
            }
            else if (Input.GetMouseButtonUp(0))
            {
                FinishDrawing();
            }
        }

        private void TryStartDrawing()
        {
            Vector2 touchPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            CircleNode nodeUnderTouch = nodeManager.GetNodeUnderTouch(touchPosition);

            if (nodeUnderTouch != null && nodeManager.CanStartFrom(nodeUnderTouch))
            {
                isDrawing = true;
                nodeManager.StartDrawing(nodeUnderTouch);
            }
        }

        private void ContinueDrawing()
        {
            Vector2 touchPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            CircleNode nodeUnderTouch = nodeManager.GetNodeUnderTouch(touchPosition);

            if (nodeUnderTouch != null)
            {
                nodeManager.VisitNode(nodeUnderTouch);
            }
        }

        private void FinishDrawing()
        {
            isDrawing = false;

            if (nodeManager.IsLevelComplete())
            {
                _buttons.gameObject.SetActive(true);
                lineDrawerInstance.LevelComplited();
                return;
            }

            nodeManager.ResetLevel();
        }

        public void ShowMenu()
        {
            Dialog.ShowMenuScreen();
            AudioManager.PlayButtonClick();
            Destroy(lineDrawerInstance.gameObject);
        }

        public void LoadNextLevel()
        {
            _levelsConfig.LevelComplited();
            Dialog.ShowGameScreen();
            AudioManager.PlayButtonClick();
            Destroy(lineDrawerInstance.gameObject);
        }

        public void LoadBackLevel()
        {
            _levelsConfig.SetBackLevel();
            Dialog.ShowGameScreen();
            AudioManager.PlayButtonClick();
            Destroy(lineDrawerInstance.gameObject);
        }
    }
}

public class NodeManager
{
    private readonly LineDrawer lineDrawer;
    private readonly List<CircleNode> visitedNodes = new();
    private readonly List<MandatoryCircleNode> mandatoryNodes = new();
    private CircleNode currentNode;

    public NodeManager(LineDrawer lineDrawer)
    {
        this.lineDrawer = lineDrawer;

        mandatoryNodes.AddRange(lineDrawer.GetMandatoryCircleNodes());
        mandatoryNodes.Sort((a, b) => a.transform.GetSiblingIndex().CompareTo(b.transform.GetSiblingIndex()));
    }

    public CircleNode GetNodeUnderTouch(Vector2 touchPosition)
    {
        return GameObject.FindObjectsOfType<CircleNode>()
            .FirstOrDefault(node => Vector2.Distance(node.Position, touchPosition) < 0.5f);
    }


    public bool CanStartFrom(CircleNode node)
    {
        return node != null && node == lineDrawer.StartNode && !node.IsVisited;
    }

    public void StartDrawing(CircleNode node)
    {
        currentNode = node;
        VisitNode(node);
        lineDrawer.StartDrawing(node.Position);
    }

    public bool CanVisitNode(CircleNode node)
    {
        if (node == null)
        {
            return false;
        }

        if (node.IsVisited)
        {
            return false;
        }

        if (!IsVerticalOrHorizontalMove(currentNode, node))
        {
            return false;
        }

        return true;
    }

    public void VisitNode(CircleNode node)
    {
        if (node == null || !CanVisitNode(node))
            return;

        Vector2 start = currentNode.Position;
        Vector2 end = node.Position;

        lineDrawer.AddSegment(start, end);

        node.MarkAsVisited();
        visitedNodes.Add(node);

        currentNode = node;
    }

    public bool IsLevelComplete()
    {
        if (currentNode != lineDrawer.EndNode)
            return false;


        if (!mandatoryNodes.All(node => visitedNodes.Contains(node)))
            return false;

        if (lineDrawer.GetAllNodes().Any(node => !node.IsVisited))
            return false;


        return true;
    }

    public void ResetLevel()
    {
        visitedNodes.Clear();
        currentNode = null;

        foreach (CircleNode node in GameObject.FindObjectsOfType<CircleNode>())
        {
            node.ResetVisit();
        }

        lineDrawer.FinishDrawing();
    }

    private bool IsVerticalOrHorizontalMove(CircleNode from, CircleNode to)
    {
        float tolerance = 0.01f; // Допустимая погрешность

        // Проверяем, что координаты x или y одинаковы с учетом погрешности
        return Mathf.Abs(from.Position.x - to.Position.x) < tolerance ||
               Mathf.Abs(from.Position.y - to.Position.y) < tolerance;
    }
}