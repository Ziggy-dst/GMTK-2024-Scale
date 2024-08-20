using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _Scripts.Managers
{
    public class UIManager : MonoBehaviour
    {
        [Header("Resource")]
        public List<Image> playerResource;

        [Header("Pages")]
        public GameObject menu;
        public GameObject win;
        public GameObject lose;

        public void UpdateResource(ResourceType resourceType, float resourceAmount)
        {
            foreach (var resourceText in playerResource)
            {
                if (resourceText.name.ToUpper().Equals(resourceType.ToString().ToUpper()))
                {
                    resourceText.fillAmount = resourceAmount / 20;
                    // resourceText.text = $"{resourceText.name}: {resourceAmount:F2}";
                    return;
                }
            }
        }

        private void OnEnable()
        {
            GameManager.onGameStateChanged += ShowPage;
        }

        private void OnDisable()
        {
            GameManager.onGameStateChanged -= ShowPage;
        }

        private void ShowPage(GameState currentState)
        {
            switch (currentState)
            {
                case GameState.Menu:
                    ShowMenu();
                    break;
                case GameState.InGame:
                    HideMenu();
                    break;
                case GameState.Win:
                    ShowWinPage();
                    break;
                case GameState.Lose:
                    ShowLosePage();
                    break;
            }
        }

        private void HideMenu()
        {
            menu?.SetActive(false);
            print("Hide menu");
        }

        private void ShowWinPage()
        {
            win?.SetActive(true);
            lose?.SetActive(false);
            menu?.SetActive(false);
            print("show win");
        }

        private void ShowLosePage()
        {
            win?.SetActive(false);
            lose?.SetActive(true);
            menu?.SetActive(false);
            print("show lose");
        }

        private void ShowMenu()
        {
            win?.SetActive(false);
            lose?.SetActive(false);
            menu?.SetActive(true);
            print("show menu");
        }
    }
}