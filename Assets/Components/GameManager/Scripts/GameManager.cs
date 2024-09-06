using GhostSpace;
using PacmanSpace;
using PelletSpace;
using TMPro;
using UnityEngine;
using Zenject;

namespace GameManagerSpace
{
    public class GameManager : MonoBehaviour
    {
        private Ghost[] _ghosts;
        private Pacman _pacman;
        public Transform PelletsContainer;
        public float DelayTime = 2f;
        
        [SerializeField] private TextMeshProUGUI _scoreText;
        [SerializeField] private TextMeshProUGUI _livesText;
        [SerializeField] private TextMeshProUGUI _gameOverText;

        private int _ghostMultiplayer = 1;

        private int Score { get; set; }
        private int Lives { get; set; }

        [Inject]
        public void Construct(Pacman pacman, Ghost[] ghosts)
        {
            _pacman = pacman;
            _ghosts = ghosts;
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

        private void NewGame()
        {
            SetScore(0);
            SetLives(3);
            
            NewRound();
            _gameOverText.gameObject.SetActive(false);
        }
        
        public void PacmanEaten()
        {
            _pacman.DeathSequence();
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
            int points = ghost.ScorePoint * _ghostMultiplayer;
            SetScore(Score + points);

            _ghostMultiplayer++;
        }

        public void PelletEaten(Pellet pellet)
        {
            pellet.gameObject.SetActive(false);
            
            SetScore(Score + pellet.Points);

            if (!HasRemainingPellets())
            {
                _pacman.gameObject.SetActive(false);
                Invoke(nameof(NewRound), 3f);
            }
        }
        
        public void PowerPelletEaten(PowerPellet pellet)
        {
            foreach (var ghost in _ghosts)
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
            for (int i = 0; i < PelletsContainer.childCount; i++)
            {
                if (PelletsContainer.GetChild(i).gameObject.activeSelf)
                {
                    return true;
                }
            }

            return false;
        }

        private void GameOver()
        {
            _gameOverText.gameObject.SetActive(true);
            
            foreach (var ghost in _ghosts)
            {
                ghost.gameObject.SetActive(false);
            }

            _pacman.gameObject.SetActive(false);
        }

        private void ResetStart()
        {
            foreach (var ghost in _ghosts)
            {
                ghost.gameObject.SetActive(true);
            }
            
            _pacman.ResetStart();
        }

        private void NewRound()
        {
            for (int i = 0; i < PelletsContainer.childCount; i++)
            {
                PelletsContainer.GetChild(i).gameObject.SetActive(true);
            }

            ResetStart();
        }

        private void SetScore(int score)
        {
            Score = score;
            _scoreText.SetText(Score.ToString());
        }
        
        private void SetLives(int lives)
        {
            Lives = lives;
            _livesText.SetText($"x{Mathf.Max(0, Lives).ToString()}");
        }
    }
}
