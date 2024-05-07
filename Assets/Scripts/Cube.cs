using System;
using UnityEngine;

[RequireComponent(typeof(Renderer))]
                                                    
public class Cube : MonoBehaviour
{
    [SerializeField] private float _chanceSeparation = 100;

    private Renderer _renderer;

    public event Action<Cube> OnDestroyed;
    public event Action<Cube> OnDetonation;

    private void Start()
    {
        _renderer = GetComponent<Renderer>();
        _renderer.material.color = new Color(UnityEngine.Random.value, UnityEngine.Random.value, UnityEngine.Random.value);
    }
    private void OnMouseDown()
    {
        int _coefficientChange = 2;
        int random = UnityEngine.Random.Range(1, 100);

        if (_chanceSeparation >= random)
        {
            _chanceSeparation /= _coefficientChange;
            OnDestroyed?.Invoke(this);
        }
        else
        {
            DetonationCube();
            Destroy(gameObject);
        }
    }

    private void DetonationCube()
    {
        OnDetonation?.Invoke(this);
    }
}
