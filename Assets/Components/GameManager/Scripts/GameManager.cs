using System;
using GhostSpace;
using PacmanSpace;
using TMPro;
using UnityEngine;

namespace GameManagerSpace
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance { get; private set; }
        public Ghost[] Ghosts;
        public Pacman Pacman;
        public GameObject[] Pellets;
        public float DelayTime = 2f;
        
        [SerializeField] private TextMeshProUGUI _scoreText;
        [SerializeField] private TextMeshProUGUI _livesText;
        [SerializeField] private TextMeshProUGUI _gameOverText;
        
        public int Score { get; private set; }
        public int Lives { get; private set; }

        private void Awake()
        {
            if (Instance != null)
            {
                DestroyImmediate(gameObject);
            }
            else
            {
                Instance = this;
            }
        }

        private void Start()
        {
            NewGame();
        }

        private void Update()
        {
            if (Lives <= 0 && Input.anyKeyDown)
            {
                NewGame();
            }
        }

        private void OnDestroy()
        {
            if (Instance == this)
            {
                Instance = null;
            }
        }

        private void NewGame()
        {
            SetScore(0);
            SetLives(3);
            
            NewRound();
        }
        
        public void PacmanEaten()
        {
            Pacman.DeathSequence();
            SetLives(Lives - 1);

            if (Lives > 0)
            {
                Invoke(nameof(ResetStart), DelayTime);
            }
            else
            {
                GameOver();
            }
        }

        public void GhostEaten(Ghost ghost)
        {
            SetScore(Score + ghost.ScorePoint);
        }

        private void GameOver()
        {
            _gameOverText.enabled = true;
            
            foreach (var ghost in Ghosts)
            {
                ghost.gameObject.SetActive(false);
            }

            Pacman.gameObject.SetActive(false);
        }

        private void ResetStart()
        {
            foreach (var ghost in Ghosts)
            {
                ghost.gameObject.SetActive(true);
            }

            Pacman.gameObject.SetActive(true);
        }

        private void NewRound()
        {
            foreach (var pellet in Pellets)
            {
                pellet.SetActive(true);
            }

            ResetStart();
        }

        private void SetScore(int score)
        {
            Score = score;
        }
        
        private void SetLives(int lives)
        {
            Lives = lives;
        }
    }
}
