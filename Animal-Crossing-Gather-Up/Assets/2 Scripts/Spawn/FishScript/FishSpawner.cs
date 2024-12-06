using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting.Antlr3.Runtime.Collections;
using UnityEngine;

public class FishSpawner : MonoBehaviour
{
    [SerializeField] private List<FishInfo> fishList;
    [SerializeField] private Transform spawnPoint;

    public OceanFish CurrentFish { get; private set; }

    public void Initialize()
    {
        if (spawnPoint == null)
            spawnPoint = transform.Find("SpawnPoint"); 
        
    }
    public bool TrySpawnFish()
    {
        if (CurrentFish != null || fishList == null || fishList.Count == 0) return false;

        var selectedFish = SelectRandomFish();
        if(selectedFish == null)return false;

        SpawnFish(selectedFish);
        return true;
    }

    private FishInfo SelectRandomFish()
    {
        float totalWeight = fishList.Sum(fish => fish.spawnWeight);
        float random = Random.Range(0f, totalWeight);

        foreach (var fishInfo in fishList)
        {
            random -= fishInfo.spawnWeight;
            if (random < 0f)
                return fishInfo;
        }
        return fishList[0];
    }

    private void SpawnFish(FishInfo fishInfo)
    {
        var fishObject = Instantiate(fishInfo.prefab, spawnPoint.position, Quaternion.identity, transform);

        if (!fishObject.TryGetComponent<OceanFish>(out OceanFish oceanFish))
        {
            Debug.LogError($"Fish component not found on prefab: {fishInfo.itemName}");
            Destroy(fishObject);
            return;
        }

        CurrentFish = oceanFish;
        CurrentFish.Initialize(fishInfo);
        BaseIslandManager.Instance.AddFish();
    }
}
