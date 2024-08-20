using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

namespace _Scripts.Managers
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance;

        [HideInInspector] public PlayerController playerController;
        [HideInInspector] public Enemy enemy;
        [HideInInspector] public ResourceManager resourceManager;
        [HideInInspector] public UIManager UIManager;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }

            UIManager = GetComponentInChildren<UIManager>();
            resourceManager = GetComponentInChildren<ResourceManager>();
            playerController = FindObjectOfType<PlayerController>();
            enemy = FindObjectOfType<Enemy>();
        }

        void Update()
        {
            RestartGame();
        }

        private void EndGame()
        {

        }

        private void RestartGame()
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }
    }
}