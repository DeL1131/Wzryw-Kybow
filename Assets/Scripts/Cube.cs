using System;
using UnityEngine;

public class Cube : MonoBehaviour
{
    [SerializeField] private float _chanceSeparation = 100;

    public event Action<Cube> OnDestroyed;
    private Renderer _renderer;

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
            Debug.Log(_chanceSeparation);
            Debug.Log(random);
            OnDestroyed?.Invoke(this);
        }

        Destroy(gameObject);
    }
}
