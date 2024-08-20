using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

namespace _Scripts.Managers
{
    public enum GameState
    {
        Menu,
        InGame,
        Win,
        Lose
    }
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance;

        public static Action<GameState> onGameStateChanged;

        public GameState currentGameState = GameState.Menu;

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

        private void Start()
        {
            ChangeGameState(GameState.Menu);
        }

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (currentGameState == GameState.Win | currentGameState == GameState.Lose)
                {
                    ChangeGameState(GameState.Menu);
                }
            }
            RestartGame();
        }

        public  void ChangeGameState(GameState newState)
        {
            switch (newState)
            {
                case GameState.Menu:
                    RestartGame();
                    currentGameState = GameState.Menu;
                    onGameStateChanged?.Invoke(GameState.Menu);
                    break;
                case GameState.InGame:
                    currentGameState = GameState.InGame;
                    onGameStateChanged?.Invoke(GameState.InGame);
                    break;
                case GameState.Win:
                    currentGameState = GameState.Win;
                    onGameStateChanged?.Invoke(GameState.Win);
                    break;
                case GameState.Lose:
                    currentGameState = GameState.Lose;
                    onGameStateChanged?.Invoke(GameState.Lose);
                    break;
            }
        }

        public void StartGame()
        {

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