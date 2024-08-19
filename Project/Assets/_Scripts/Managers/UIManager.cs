using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace _Scripts.Managers
{
    public class UIManager : MonoBehaviour
    {
        [Header("Resource")]
        public List<TextMeshProUGUI> playerResource;

        public void UpdateResource(ResourceType resourceType, float resourceAmount)
        {
            foreach (var resourceText in playerResource)
            {
                if (resourceText.name.ToUpper().Equals(resourceType.ToString().ToUpper()))
                {
                    resourceText.text = $"{resourceText.name}: {resourceAmount}";
                    return;
                }
            }
        }
    }
}