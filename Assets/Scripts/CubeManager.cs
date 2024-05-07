using UnityEngine;

public class CubeManager : MonoBehaviour
{
    [SerializeField] private Transform[] _points;
    [SerializeField] private Cube _cubePrefab;
    [SerializeField] private Explosion _explosion;

    private void Start()
    {
        for (int i = 0; i < _points.Length; i++)
        {
            Cube cube = Instantiate(_cubePrefab, _points[i].transform.position, Quaternion.identity);
            cube.OnDestroyed += CreateCubes;
        }
    }

    private void CreateCubes(Cube cube)
    {
        int scaleDecreaseModifare = 2;
        int countCube = UnityEngine.Random.Range(2, 7);

        cube.transform.localScale /= scaleDecreaseModifare;

        for (int i = 0; i < countCube; i++)
        {
            Cube newCube = Instantiate(cube, cube.transform.position, Quaternion.identity);
            newCube.OnDestroyed += CreateCubes;
            newCube.OnDetonation += DetonationCube;
        }

        cube.OnDestroyed -= CreateCubes;
    }

    private void DetonationCube(Cube cube)
    {
        _explosion.Explode(cube);
    }
}