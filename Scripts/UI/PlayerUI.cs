using UnityEngine;

public class PlayerUI : MonoBehaviour
{
    [SerializeField] private GameObject heartsHolder;
    [SerializeField] private GameObject heartIcon;

    public void SetUI(PlayerStats stats)
    {
        print("Set UI");
        HandleHearts(stats);
    }

    private void HandleHearts(PlayerStats stats)
    {
        // Clear current hearts
        foreach (Transform child in heartsHolder.transform)
        {
            Destroy(child.gameObject);
        }

        int heartsToSpawn = (int)stats.health;

        // Spawn hearts
        for (int i = 0; i < heartsToSpawn; i++)
        {
            Instantiate(heartIcon, heartsHolder.transform);
        }
    }
}
