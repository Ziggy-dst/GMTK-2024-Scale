using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace _Scripts.Managers
{
    public class UIManager : MonoBehaviour
    {
        [Header("Resource")]
        public List<TextMeshProUGUI> playerResource;

        public void UpdateResource(int resource)
        {
            foreach (var resourceText in playerResource)
            {
                resourceText.text = $"${resource}";
            }
        }
    }
}