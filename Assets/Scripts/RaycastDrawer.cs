using UnityEngine;

public class RaycastDrawer : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    [SerializeField] private int _layerNumber;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            DrawRay();
        }
    }

    private void DrawRay()
    {
        RaycastHit hit;
        Ray ray = _camera.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, _layerNumber))
        {
            if (hit.transform.gameObject.TryGetComponent(out ExplosionCube cube))
            {
                cube.TryExplose();
            }
        }
    }
}
