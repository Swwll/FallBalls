using UnityEngine;

public class Raycaster : MonoBehaviour
{
    [SerializeField] private Camera _camera;

    public bool TryRaycastComponent<T>(out T component) where T : MonoBehaviour
    {
        component = null;

        if (TryCastRay(out RaycastHit hit))
        {
            if (hit.transform.TryGetComponent(out T hitComponent))
            {
                component = hitComponent;
                return true;
            }
        }

        return false;
    }

    public bool TryCastRay(out RaycastHit hit)
    {
        hit = new RaycastHit();
        Ray ray = _camera.ScreenPointToRay(UnityEngine.Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit raycastHit, Mathf.Infinity))
        {
            hit = raycastHit;
            return true;
        }

        return false;
    }
}