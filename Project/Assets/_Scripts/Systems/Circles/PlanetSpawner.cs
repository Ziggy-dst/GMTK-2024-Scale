using _Scripts.Managers;
using _Scripts.Utilities;
using UnityEngine;

public class PlanetSpawner : MonoBehaviour
{
    public class CircleSpawnProbability
    {
        public ResourceType ResourceType;
        public float SpawnProbability;
    }

    [Header("General Settings")]
    public GameObject planetCircle;
    public Vector2 innerCircleSpawnScaleRange;
    public Vector2 adjacentScaleRatioDifferenceRange;

    [Header("Initial Planet Spawn Settings")]
    public Vector2Int initialLayerAmountRange;

    [Header("Inner Circle Spawn Settings")]
    // the current inner circle scale that would trigger new circle spawns (must bigger than min innerCircleSpawnScale)
    public Vector2 innerCircleSpawnScaleThresholdRange;

    private GameObject _currentInnerCircle;
    private ResourceType _lastResourceType;


    void Start()
    {
        SpawnInitialPlanet();
    }

    void Update()
    {
        // TODO: BUG: repeat color, should be called when inner circle is expanded
        if (_currentInnerCircle.transform.localScale.x >= Random.Range(innerCircleSpawnScaleThresholdRange.x, innerCircleSpawnScaleThresholdRange.y)) SpawnInnerCircle();
    }

    private void GenerateCircleColor(SpriteRenderer spriteRenderer, ResourceType resourceType)
    {
        switch (resourceType)
        {
            case ResourceType.Red:
                spriteRenderer.color = new Color(125f / 255f, 14f / 255f, 14f / 255f);
                break;
            case ResourceType.Green:
                spriteRenderer.color = new Color(14f / 255f, 125f / 255f, 14f / 255f);
                break;
            case ResourceType.Blue:
                spriteRenderer.color = new Color(14f / 255f, 14f / 255f, 125f / 255f);
                break;
        }
    }

    private GameObject SpawnCircle()
    {
        var spawnedCircle = Instantiate(planetCircle, transform);

        // set resource type
        var resourceType = spawnedCircle.GetComponent<CircleArea>().resourceType = Utils.GetRandomEnumValue(_lastResourceType);
        _lastResourceType = resourceType;

        GenerateCircleColor(spawnedCircle.GetComponent<SpriteRenderer>(), resourceType);

        return spawnedCircle;
    }

    private void SpawnInnerCircle()
    {
        // set sorting layer
        int lastLargestSortingLayer;
        if (_currentInnerCircle != null)
            lastLargestSortingLayer = _currentInnerCircle.GetComponent<SpriteRenderer>().sortingOrder;
        else lastLargestSortingLayer = 99;

        _currentInnerCircle = SpawnCircle();
        // initial scale
        _currentInnerCircle.transform.localScale *= Random.Range(innerCircleSpawnScaleRange.x, innerCircleSpawnScaleRange.y);
        _currentInnerCircle.GetComponent<SpriteRenderer>().sortingOrder = lastLargestSortingLayer + 1;
    }

    private void SpawnInitialPlanet()
    {
        // random amount of layer
        int initialLayerAmount = Random.Range(initialLayerAmountRange.x, initialLayerAmountRange.y + 1);

        if (initialLayerAmount == 0) return;

        SpawnInnerCircle();

        if (initialLayerAmount == 1) return;

        Vector3 lastCircleScale = _currentInnerCircle.transform.localScale;
        int lastSortingLayer = _currentInnerCircle.GetComponent<SpriteRenderer>().sortingOrder;
        // spawn outer circles with different scale
        for (int i = 0; i < initialLayerAmount - 1; i++)
        {
            Vector3 currentCircleScale =
                Random.Range(adjacentScaleRatioDifferenceRange.x, adjacentScaleRatioDifferenceRange.y) * lastCircleScale;
            var spawnedCircle = SpawnCircle();

            // set the current circle with bigger scale
            lastCircleScale = currentCircleScale;
            spawnedCircle.transform.localScale = currentCircleScale;

            // set the current bigger circle at the bottom of the last one
            spawnedCircle.GetComponent<SpriteRenderer>().sortingOrder = lastSortingLayer - 1;
            lastSortingLayer--;
        }
    }
}
