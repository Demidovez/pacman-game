using System;
using GhostSpace;
using PacmanSpace;
using UnityEngine;

namespace GameManagerSpace
{
    public class GameManager : MonoBehaviour
    {
        public Ghost[] Ghosts;
        public Pacman Pacman;
        public GameObject[] Pellets;
        public float DelayTime = 2f;
        
        public int Score { get; private set; }
        public int Lives { get; private set; }

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
        }
        
        public void PacmanEaten()
        {
            Pacman.gameObject.SetActive(false);
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
