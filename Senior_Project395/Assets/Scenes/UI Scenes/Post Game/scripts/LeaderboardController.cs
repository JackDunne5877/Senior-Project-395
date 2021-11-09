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
            getLeaderboardData(SingletonManager.Instance.currentPlayingGame.sceneName);
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
