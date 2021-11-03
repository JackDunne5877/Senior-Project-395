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
    // used to make login user objects to convert to JSON
    public class LoginUser
    {
        public string username {get; set;}
        public string password {get; set;}

        public LoginUser(string username, string password)
        {
            this.username = username;
            this.password = password;
        }
    }

    // save response data
    public class HttpResponse
    {
        public int statusCode {get; set;}
        public string content {get; set;}

        public HttpResponse(int statusCode, string content)
        {
            this.statusCode = statusCode;
            this.content = content;
        }
    }

    // used to create a new user, returns response string
    public static HttpResponse createUser(string username, string password, string first_name, string last_name, string birthday)
    {
        var client = new HttpClient();

        // creating JSON with user data
        NewUser newUser = new NewUser(username, password, first_name, last_name, birthday);
        var json = JsonConvert.SerializeObject(newUser);
        var data = new StringContent(json, Encoding.UTF8, "application/json");

        // url of server
        var url = "https://mira-csds395-server-el7.gigabyteproductions.net/DatingGameAPI/newuser";

        // send data to server
        var task = Task.Run(() => client.PostAsync(url,data));
        task.Wait();
        var response = task.Result;

        // read data from server
        var task2 = Task.Run(() => response.Content.ReadAsStringAsync());
        task2.Wait();
        var content = task2.Result;

        // save reponse data from server
        HttpResponse httpResponse = new HttpResponse((int)response.StatusCode, content);

        return httpResponse;
    }

    // used to create a new user, returns response string
    public static HttpResponse loginUser(string username, string password)
    {
        var client = new HttpClient();

        // creating JSON with user data
        LoginUser loginUser = new LoginUser(username, password);
        var json = JsonConvert.SerializeObject(loginUser);
        var data = new StringContent(json, Encoding.UTF8, "application/json");

        // url of server
        var url = "https://mira-csds395-server-el7.gigabyteproductions.net/DatingGameAPI/login";

        // send data to server
        var task = Task.Run(() => client.PostAsync(url,data));
        task.Wait();
        var response = task.Result;

        // read data from server
        var task2 = Task.Run(() => response.Content.ReadAsStringAsync());
        task2.Wait();
        var content = task2.Result;

        // save reponse data from server
        HttpResponse httpResponse = new HttpResponse((int)response.StatusCode, content);

        return httpResponse;
    }
}
