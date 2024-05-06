using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Transform[] _points;
    [SerializeField] private Cube _cubePrefab;
    [SerializeField] private float _explosionRadius;
    [SerializeField] private float _explosionForce;

    private void Start()
    {
        for (int i = 0; i < _points.Length; i++)
        {
            Cube cube = Instantiate(_cubePrefab, _points[i].transform.position, Quaternion.identity);
            cube.OnDestroyed += CreateCubes;
            cube.OnDetonation += DetonationCube;
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
           // newCube.GetComponent<Rigidbody>().AddExplosionForce(_explosionForce, transform.position, _explosionRadius);
        }
    }

    private void DetonationCube(Cube cube)
    {
        Explode(cube);
    }

    private void Explode(Cube cube)
    {
        foreach (Rigidbody explodableObject in GetExplodableObjects(cube))
            explodableObject.AddExplosionForce(_explosionForce, cube.transform.position, _explosionRadius);
    }

    private List<Rigidbody> GetExplodableObjects(Cube cube)
    {
        Collider[] hits = Physics.OverlapSphere(cube.transform.position, _explosionRadius);

        List<Rigidbody> cubes = new();

        foreach (Collider hit in hits)
        {
           if(hit.attachedRigidbody != null)
                cubes.Add(hit.attachedRigidbody);
        }

        return cubes;
    }
}