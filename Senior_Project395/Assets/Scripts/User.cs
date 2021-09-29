using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json;

public class User : MonoBehaviour
{
    // used to make user objects to convert to JSON
    public class NewUser
    {
        public string username {get; set;}
        public string password {get; set;}
        public string first_name {get; set;}
        public string last_name {get; set;}
        public string birthday {get; set;}

        public NewUser(string username, string password, string first_name, string last_name, string birthday)
        {
            this.username = username;
            this.password = password;
            this.first_name = first_name;
            this.last_name = last_name;
            this.birthday = birthday;
        }
    }

    // used to create a new user
    public void createUser(string username, string password, string first_name, string last_name, string birthday)
    {
        var client = new HttpClient();

        // creating JSON with user data
        NewUser newUser = new NewUser(username, password, first_name, last_name, birthday);
        var json = JsonConvert.SerializeObject(newUser);
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
