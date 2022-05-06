using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

namespace Animations.Console_Menu
{
    public class PlayerScores : MonoBehaviour
    {
        private Highscores leaderboard;
        public static String currentPlayer;

        private void Awake()
        {
            if (PlayerPrefs.HasKey("LB"))
            {
                LoadLeaderboard();    
            }
            else
            {
                PlayerPrefs.SetString("LB",JsonUtility.ToJson(new Highscores()));
                LoadLeaderboard();
            }
            
            DontDestroyOnLoad(this.gameObject);
        }

        public void AddPlayer(String pname, int score = 0)
        {
            leaderboard.playerScoresList.Add(new PlayerScore {name = pname, score = score});
            SaveLeaderboard();
        }


        public void SaveLeaderboard()
        {
            string json = JsonUtility.ToJson(leaderboard);
            PlayerPrefs.SetString("LB", json);
            PlayerPrefs.Save();
        }

        void LoadLeaderboard()
        {
            String json = PlayerPrefs.GetString("LB");
            Highscores highscores = (Highscores) JsonUtility.FromJson(json, typeof(Highscores));
            leaderboard = highscores;
        }

        public void SelectPlayer(String name)
        {
            currentPlayer = name;
        }

        public List<string> GetScoreList()
        {
            List<String> outputScores = new List<string>();
            foreach (PlayerScore entry in leaderboard.playerScoresList)
            {
                outputScores.Add(entry.name + ": " + entry.score);
            }

            return outputScores;
        }

        public bool PlayerExists(String name)
        {
            foreach (PlayerScore entry in leaderboard.playerScoresList)
            {
                if (entry.name == name) return true;
            }

            return false;
        }

        public int? GetPlayerScore(String pname)
        {
            foreach (PlayerScore entry in leaderboard.playerScoresList)
            {
                if (pname == entry.name)
                {
                    return entry.score;
                }
            }

            return null;
        }

        public void UpdatePlayerScore(int givenScore)
        {
            
            foreach (PlayerScore entry in leaderboard.playerScoresList)
            {
                if (entry.name == currentPlayer)
                {
                    entry.score = givenScore;
                }
            }
            SaveLeaderboard();
        }
    }

    class Highscores
    {
        public List<PlayerScore> playerScoresList;
    }


    /*
     * Entry
     */
    [Serializable]
    class PlayerScore
    {
        public String name;
        public int score;
    }
}