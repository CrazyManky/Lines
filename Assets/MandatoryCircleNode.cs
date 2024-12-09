using _Project.Scripts.GameElements;
using UnityEngine;

public class MandatoryCircleNode : CircleNode
{
    [SerializeField] private SpriteRenderer _sprite;

    private bool isMandatoryVisited;

    public bool IsMandatoryVisited => isMandatoryVisited;

    public override void MarkAsVisited()
    {
        base.MarkAsVisited();
        isMandatoryVisited = true;
        _sprite.color = ColorAcitve;
    }

    public override void ResetVisit()
    {
        base.ResetVisit();
        isMandatoryVisited = false;
        _sprite.color = DefaultColor;
    }
}