using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Dating_Platform {
    public class LeaderboardController : MonoBehaviour
    {
        public GameObject leaderboardRowPrefab;
        public GameObject leaderboardObj;
        
        // Start is called before the first frame update
        void Start()
        {
            //TODO fix with new Game definition
            //getLeaderboardData(SingletonManager.Instance.currentPlayingGame.sceneName);

            //add info for this session to leaderboard SIMULATION
            GameObject thisLeaderboardRow =  GameObject.Instantiate(leaderboardRowPrefab);
            LeaderboardRow leaderboardRowData = thisLeaderboardRow.GetComponent<LeaderboardRow>();
            leaderboardRowData.scoreTxt.text = SingletonManager.Instance.playerScore.ToString();
            leaderboardRowData.teamTxt.text = $"{SingletonManager.Instance.Player.DisplayName} and {SingletonManager.Instance.playingWithOtherPlayer.DisplayName}";
            leaderboardRowData.dateTxt.text = "11/29/2021";
            leaderboardRowData.rankTxt.text = (3).ToString();
            thisLeaderboardRow.transform.SetParent(leaderboardObj.transform, false);

            sortLeaderboard();
        }

        public void sortLeaderboard()
        {
            //TODO 
            Debug.Log("");
        }

        public void getLeaderboardData(string minigameName)
        {
            StartCoroutine(getLeaderboardDataCoroutine(minigameName));
        }

        IEnumerator getLeaderboardDataCoroutine(string minigameName)
        {
            (int _score, string _team, string _date)[] scores = DatabaseConnection.leaderboardGameScoresRequest(minigameName);
            yield return scores;

            for(int i =0; i < scores.Length;i++)
            {
                (int score, string team, string date) row = scores[i];
                GameObject newScoreRowPrefab = GameObject.Instantiate(leaderboardRowPrefab);
                LeaderboardRow newScoreRow = newScoreRowPrefab.GetComponent<LeaderboardRow>();
                newScoreRow.scoreTxt.text = row.score.ToString();
                newScoreRow.teamTxt.text = row.team;
                newScoreRow.dateTxt.text = row.date;
                newScoreRow.rankTxt.text = (i + 1).ToString();
                newScoreRowPrefab.transform.SetParent(leaderboardObj.transform, false);
            }
        }
    }
}
