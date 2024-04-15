using UnityEngine;

public class CubeSpawner : MonoBehaviour
{
    [SerializeField] private ExplosionCube _prefabCube;

    public void SpawnCubes(ExplosionCube receivedCube)
    {
        int minCubesCount = 2;
        int maxCubesCount = 7;
        int cubesCount = Random.Range(minCubesCount, maxCubesCount);
        float sizeCoefficent = 0.5f;
        Vector3 cubesScale = receivedCube.transform.localScale * sizeCoefficent;

        for (int i = 0; i < cubesCount; i++)
        {
            ExplosionCube newCube = Instantiate(_prefabCube, transform.position, Quaternion.identity);
            MeshRenderer currentRenderer = newCube.GetComponent<MeshRenderer>();
            ChangeColor(currentRenderer);
            newCube.SpawnChance = receivedCube.SpawnChance * sizeCoefficent;
            newCube.SetSpawner(gameObject.GetComponent<CubeSpawner>());
            newCube.transform.localScale = cubesScale;
        }
    }

    private void ChangeColor(MeshRenderer renderer)
    {
        string colorName = "_Color";
        float colorMaxValue = 1f;
        Color newColor = new Color(Random.Range(0f, colorMaxValue), Random.Range(0f, colorMaxValue), Random.Range(0f, colorMaxValue));
        renderer.material.SetColor(colorName, newColor);
    }
}
