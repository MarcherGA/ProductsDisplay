using UnityEngine;

public class SpinByTime : MonoBehaviour
{
    [SerializeField] private float speed = 100f;

    void FixedUpdate()
    {
        transform.Rotate(Vector3.up, speed * Time.fixedDeltaTime);
    }
}
