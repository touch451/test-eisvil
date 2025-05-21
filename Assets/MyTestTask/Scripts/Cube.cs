using System;
using Types;
using UnityEngine;

namespace Scripts
{
    [RequireComponent(typeof(Rigidbody2D), typeof(SpriteRenderer))]
    public class Cube : MonoBehaviour
    {
        [SerializeField] private CubeType cubeType;
        [SerializeField] private float moveSpeed;

        private SpriteRenderer _sr;
        private Rigidbody2D _rb;

        private Vector2 _cubeSize;
        private Bounds _moveBounds;
        private bool _isMoving;

        public static event Action<CubeType> OnCubeKilled;

        private void OnMouseDown()
        {
            Destroy(gameObject);
            OnCubeKilled?.Invoke(cubeType);
        }

        public void InitMoving(Bounds cameraWorldBounds)
        {
            _moveBounds = cameraWorldBounds;

            _sr = GetComponent<SpriteRenderer>();
            _cubeSize.x = _sr.bounds.extents.x;
            _cubeSize.y = _sr.bounds.extents.y;

            Vector2 startDir = UnityEngine.Random.insideUnitCircle.normalized;
            _rb = GetComponent<Rigidbody2D>();
            _rb.velocity = startDir * moveSpeed;

            _isMoving = true;
        }

        private void Update()
        {
            if (!_isMoving)
                return;

            Vector3 pos = transform.position;
            Vector3 min = _moveBounds.min;
            Vector3 max = _moveBounds.max;
            Vector2 vel = _rb.velocity;

            // Отскок от границ экрана
            if (pos.x - _cubeSize.x < min.x || pos.x + _cubeSize.x > max.x)
            {
                vel.x *= -1;
                pos.x = Mathf.Clamp(pos.x, min.x + _cubeSize.x, max.x - _cubeSize.x);
                transform.position = pos;
            }

            if (pos.y - _cubeSize.y < min.y || pos.y + _cubeSize.y > max.y)
            {
                vel.y *= -1;
                pos.y = Mathf.Clamp(pos.y, min.y + _cubeSize.y, max.y - _cubeSize.y);
                transform.position = pos;
            }

            _rb.velocity = vel.normalized * moveSpeed;
        }
    }
}