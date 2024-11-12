using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using Vector3 = UnityEngine.Vector3;

public class ProductFocus : MonoBehaviour
{
    public UnityEvent<int> productFocused = new UnityEvent<int>();
    public UnityEvent<int> productUnfocused = new UnityEvent<int>();

    public Transform[] _cameraTargets { get; set; }

    [SerializeField] private Transform _targetCamera;
    [SerializeField] private float _transitionDuration = 1f;
    private Vector3 _initialPosition;
    private int _targetIndex = -1;
    private bool _isFocused = false;
    private bool _isTransitioning = false;

    private void Start()
    {
        _initialPosition = _targetCamera.position;
    }


    public void FocusProduct(int index)
    {
        if((_targetIndex == index && _isFocused) || _isTransitioning) return;

        if (_isFocused)
        {
            productUnfocused.Invoke(_targetIndex);
        }
        

        _targetIndex = index;
        Vector3 targetPosition = _cameraTargets[_targetIndex].position;
        targetPosition.z -= 0.55f;
        StartCoroutine(Transition(targetPosition, () => { _isFocused = true; productFocused.Invoke(_targetIndex);}));

    }

    public void UnfocusProduct()
    {
        if (_isTransitioning) return;
        if (_isFocused)
        {
            StartCoroutine(Transition(_initialPosition));
            _isFocused = false;
            productUnfocused.Invoke(_targetIndex);
        }
    }

    public void SetCameraTargets(Transform[] targets)
    {
        _cameraTargets = targets;
    }

    IEnumerator Transition(Vector3 targetPosition, Action onComplete = null)
    {
        float t = 0.0f;
        Vector3 startingPosition = _targetCamera.position;
        _isTransitioning = true;
        while (t < _transitionDuration)
        {
            t += Time.deltaTime;

            _targetCamera.position = Vector3.Lerp(startingPosition, targetPosition, t);
            yield return 0;
        }
        _targetCamera.position = targetPosition;
        onComplete?.Invoke();
        _isTransitioning = false;
    }

}
