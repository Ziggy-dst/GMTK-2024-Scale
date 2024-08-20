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
        [HideInInspector] public AudioManager audioManager;

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
            audioManager = GetComponentInChildren<AudioManager>();
            playerController = FindObjectOfType<PlayerController>();
            enemy = FindObjectOfType<Enemy>();
        }

        private void Start()
        {
            currentGameState = GameState.Menu;
            onGameStateChanged?.Invoke(GameState.Menu);
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

            if (Input.GetKeyDown(KeyCode.R)) RestartGame();
        }

        private void OnEnable()
        {
            SceneManager.sceneLoaded += OnSceneLoaded;
        }

        private void OnDisable()
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
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
                    audioManager.PlayMusic("InGameBGM");
                    currentGameState = GameState.InGame;
                    onGameStateChanged?.Invoke(GameState.InGame);
                    break;
                case GameState.Win:
                    audioManager.StopMusic();
                    currentGameState = GameState.Win;
                    onGameStateChanged?.Invoke(GameState.Win);
                    break;
                case GameState.Lose:
                    audioManager.StopMusic();
                    audioManager.PlayMusic("GameOver");
                    currentGameState = GameState.Lose;
                    onGameStateChanged?.Invoke(GameState.Lose);
                    break;
            }
        }

        private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            Debug.Log("Scene Loaded: " + scene.name);
            Debug.Log("Load Mode: " + mode);

            playerController = FindObjectOfType<PlayerController>();
            enemy = FindObjectOfType<Enemy>();
        }

        private void RestartGame()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}