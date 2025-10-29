using UnityEngine;

public class CameraPInput : MonoBehaviour
{
    [SerializeField] private Vector3 _cameraOffset = new Vector3(0,2,-10);
    [SerializeField] private Transform target;
    [SerializeField] private float _speedCamera = 2f;

    public static CameraPInput instance;
    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
    }
    public void LateUpdate()
    {
        if (target == null) return;
      
        Vector3 targetPosition = target.position + _cameraOffset;
        transform.position = Vector3.Lerp(transform.position, targetPosition, _speedCamera * Time.deltaTime);
    }
    public void SetTarget(Transform newTarget)
    {
        target = newTarget;
    }
}
