using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json;

public class Leaderboard
{
    // Start is called before the first frame update
    public void sendScore(String game, int score)
    {
        var client = new HttpClient();

        // TODO: create game+score class
        var json = JsonConvert.SerializeObject(score);
        var data = new StringContent(json, Encoding.UTF8, "application/json");

        // url of server
        var url = "http://localhost:8080/DatingGameAPI";

        // send data to server
        var task = Task.Run(() => client.PostAsync(url,data));
        task.Wait();
        var response = task.Result;
        Debug.LogError(response);
    }
}
