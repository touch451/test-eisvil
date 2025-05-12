using Types;
using UnityEngine;

namespace Scripts
{
    public class CubeSpawner : MonoBehaviour
    {
        [SerializeField] private Cube redCubePrefab;
        [SerializeField] private Cube greenCubePrefab;
        [SerializeField] private int allCount = 20;

        Bounds bounds = new Bounds();

        private void Start()
        {
            SpawnCubes();
        }

        private void SpawnCubes()
        {
            var mainCamera = Camera.main;

            bounds.SetMinMax(
                mainCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)),
                mainCamera.ViewportToWorldPoint(new Vector3(1, 1, 0)));

            for (int i = 0; i < allCount; i++)
            {
                CubeType type = i % 2 == 0 ? CubeType.Red : CubeType.Green;

                float x = Random.Range(bounds.min.x + 0.5f, bounds.max.x - 0.5f);
                float y = Random.Range(bounds.min.y + 0.5f, bounds.max.y - 0.5f);
                Vector3 spawnPos = new Vector3(x, y, 0);

                var cube = InstantiateCube(type);
                cube.transform.position = spawnPos;

                cube.InitMoving(bounds);
            }
        }

        private Cube InstantiateCube(CubeType cubeType)
        {
            Cube prefab = null;

            switch (cubeType)
            {
                case CubeType.Red:
                    prefab = redCubePrefab;
                    break;
                case CubeType.Green:
                    prefab = greenCubePrefab;
                    break;
                case CubeType.None:
                    Debug.LogWarning("Cube type is None.");
                    prefab = redCubePrefab;
                    break;
            }

            return Instantiate(prefab, transform);
        }
    }
}