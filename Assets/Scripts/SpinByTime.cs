using UnityEngine;

public class SpinByTime : MonoBehaviour
{
    [SerializeField] private float speed = 100f;

    void Update()
    {
        transform.Rotate(Vector3.up, speed * Time.deltaTime);
    }
}