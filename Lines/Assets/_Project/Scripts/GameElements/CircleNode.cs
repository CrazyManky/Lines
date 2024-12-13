using Services;
using UnityEngine;


namespace _Project.Scripts.GameElements
{
    public abstract class CircleNode : MonoBehaviour
    {
        [SerializeField] protected SpriteRenderer Sprite;
        [SerializeField] protected ParticleSystem ParticleSystem;
        [SerializeField] protected Color ColorAcitve;
        [SerializeField] protected Color DefaultColor;
        [SerializeField] protected bool _isVisited = false;

        private AudioManager _audioManager;
        public Vector2 Position => transform.position;

        public bool IsVisited => _isVisited;

        private void Awake()
        {
            ParticleSystem.Stop();
            _audioManager = ServiceLocator.Instance.GetService<AudioManager>();
        }

        public virtual void MarkAsVisited()
        {
            _audioManager.PlayColisionActive();
            ParticleSystem.Play();
            Sprite.color = ColorAcitve;
            _isVisited = true;
        }

        public virtual void ResetVisit()
        {
            Sprite.color = DefaultColor;
            _isVisited = false;
        }
    }
}