using GhostSpace;
using PacmanSpace;
using PelletSpace;
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

        private int _ghostMultiplayer = 1;
        
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

        public void PelletEaten(Pellet pellet)
        {
            pellet.gameObject.SetActive(false);
            
            SetScore(Score + pellet.Points);

            if (!HasRemainingPellets())
            {
                Pacman.gameObject.SetActive(false);
                Invoke(nameof(NewRound), 3f);
            }
        }
        
        public void PowerPelletEaten(PowerPellet pellet)
        {
            foreach (var ghost in Ghosts)
            {
                ghost.Frightened.Enable(pellet.Duration);
            }
            
            PelletEaten(pellet);
            CancelInvoke(nameof(ResetGhost));
            Invoke(nameof(ResetGhost), pellet.Duration);
        }

        private void ResetGhost()
        {
            _ghostMultiplayer = 1;
        }

        private bool HasRemainingPellets()
        {
            foreach (var pellet in Pellets)
            {
                if (pellet.activeSelf)
                {
                    return true;
                }
            }

            return false;
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
            _gameOverText.enabled = false;
            
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
