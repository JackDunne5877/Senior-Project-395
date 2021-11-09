using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json;

namespace Dating_Platform
{

    public static class DatabaseConnection
    {
        private static HttpClient Client = new HttpClient();
        public static Sprite samplePlayerImage = Sprite.Create(new Texture2D(200, 200), new Rect(), Vector2.one);

        #region gettingPlayerInfo
        public static User getOwnedPlayerInfo(string playerId, string password)
        {
            //TODO check if password works for the given playerId
            return new User();
        }
        public static User getPublicPlayerInfo(string playerId)
        {
            User resPlayer = new User();
            resPlayer.DisplayName = "samplePlayerName"; //TODO should be acquired from DB
            resPlayer.ProfileImg = samplePlayerImage; //TODO should be acquired from DB
            resPlayer.PlayerID = playerId;
            return resPlayer;
        }

        public static User getConnectionPlayerInfo(User myPlayer, string password, string connectionId)
        {
            //TODO check if password works for the given playerId
            if (ConfirmPlayerPassword(myPlayer.PlayerID, password))
            {
                if (ConfirmPlayerHasConnection(myPlayer, connectionId))
                {
                    //TODO all values should really be acquired from DB
                    User resPlayer = new User();
                    resPlayer.DisplayName = "samplePlayerName";
                    resPlayer.ProfileImg = samplePlayerImage;
                    resPlayer.PlayerImages = new Sprite[] { samplePlayerImage, samplePlayerImage };
                    resPlayer.age = 18;
                    resPlayer.bio = "sample Player Bio";
                    resPlayer.genderIdentity = SingletonManager.GenderOption.Male;
                    resPlayer.genderPreferences = new SingletonManager.GenderOption[] { SingletonManager.GenderOption.Male, SingletonManager.GenderOption.Female, SingletonManager.GenderOption.NonBinary };
                    resPlayer.PlayerID = connectionId;
                    return resPlayer;
                }
                else
                {
                    Debug.LogWarning($"Get Connection Player Info: connection with {connectionId} does not exist");
                    return new User()
                    {
                        PlayerID = "NOTAUTHORIZED",
                        bio = "not authorized to get this connection info"
                    };
                }
            }
            else
            {
                Debug.LogWarning("Get Connection Player Info: password incorrect");
                return new User()
                {
                    PlayerID = "NOTAUTHORIZED",
                    bio = "not authorized to get this connection info"
                };
            }


        }

        public static bool ConfirmPlayerPassword(string myPlayerId, string password)
        {
            //TODO make legit database password check OR convert this to a token system
            string samplepassword = "12345";
            string samplePlayerID = "abc";
            return (myPlayerId == samplePlayerID && password == samplepassword);
        }

        public static bool ConfirmPlayerHasConnection(User myPlayer, string connectionPlayerId)
        {
            //TODO get results from DB
            return (Array.IndexOf(myPlayer.connectionIds, connectionPlayerId) > -1);
        }

        #endregion

        #region accountOperations

        public static void setNewAccountEmail(string myPlayerId, string password, string newEmailAddress)
        {
            if (ConfirmPlayerPassword(myPlayerId, password))
            {
                //TODO send new email to the database
            }
        }

        public static void setNewAccountPassword(string myPlayerId, string password, string newPassword)
        {
            if (ConfirmPlayerPassword(myPlayerId, password))
            {
                //TODO send new password to the database
            }
        }

        public static bool disableAccount(string myPlayerId, string password)
        {
            if (ConfirmPlayerPassword(myPlayerId, password))
            {
                bool success = false;//TODO delete player's account
                Debug.LogWarning("player account deletion not implemented");
                return success;
            }
            else
            {
                return false;
            }
        }

        public static (bool, int, string) login(string username, string password)
        {

            HttpResponse response = LoginUserRequest(username, password);

            return (response.statusCode < 400, response.statusCode, response.content);
        }

        public static (bool, int, string) createNewUser(string username, string password, string firstname, string lastname, string birthdate)
        {

            HttpResponse response = CreateUserRequest(username, password, firstname, lastname, birthdate);

            return (response.statusCode < 400, response.statusCode, response.content);

            //if (response.statusCode >= 400)
            //{
            //    Debug.LogWarning($"{response.statusCode}: {response.content}");
            //    return (false, response.content);
            //}
            //else
            //{
            //    return (true, response.content);//success
            //}
        }

        public static void logout()
        {
            //TODO more stuff
            SingletonManager.Instance.Player = null;
        }

        // save response data
        public class HttpResponse
        {
            public int statusCode { get; set; }
            public string content { get; set; }

            public HttpResponse(int statusCode, string content)
            {
                this.statusCode = statusCode;
                this.content = content;
            }
        }

        // used to create a new user, returns response string
        public static HttpResponse CreateUserRequest(string username, string password, string first_name, string last_name, string birthday)
        {

            // creating JSON with user data
            User.NewUser newUser = new User.NewUser(username, password, first_name, last_name, birthday);
            var json = JsonConvert.SerializeObject(newUser);
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            // url of server
            var url = "https://mira-csds395-server-el7.gigabyteproductions.net/DatingGameAPI/newuser";

            // send data to server
            var task = Task.Run(() => Client.PostAsync(url, data));
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
        public static HttpResponse LoginUserRequest(string username, string password)
        {

            // creating JSON with user data
            User.LoginUser loginUser = new User.LoginUser(username, password);
            var json = JsonConvert.SerializeObject(loginUser);
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            // url of server
            var url = "https://mira-csds395-server-el7.gigabyteproductions.net/DatingGameAPI/login";

            // send data to server
            var task = Task.Run(() => Client.PostAsync(url, data));
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
        #endregion

        #region Scores And Likes
        public static (bool, int, string) sendScore(string playerIdA, string playerIdB, string game, int score)
        {
            Debug.LogError("DBConnection.sendScore() not implemented");
            return (false, 000, "not implemented");

            // TODO: create game+score class
            var json = JsonConvert.SerializeObject(score);
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            // url of server
            var url = "http://localhost:8080/DatingGameAPI";

            // send data to server
            var task = Task.Run(() => Client.PostAsync(url, data));
            task.Wait();
            var response = task.Result;
            Debug.LogError(response);
        }

        public static (int score, string team, string date)[] leaderboardGameScoresRequest(string game)
        {
            //TODO
            Debug.LogError("DBConnection.leaderboardGameScoresRequest() not implemented");
            return new (int, string, string)[] { (000, "not implemented", "not implemented") };
        }

        public static (bool, int, string) likePlayer(User playerThatIsLiked)
        {
            //TODO
            Debug.LogError("DBConnection.likePlayer() not implemented");
            User playerWhoIsSending = SingletonManager.Instance.Player;
            return (false, 000, "not implemented");
            //return (successful?, statuscode, msg);
        }

        public static (bool, int, string) dislikePlayer(User playerThatIsDisliked)
        {
            //TODO
            Debug.LogError("DBConnection.dislikePlayer() not implemented");
            User playerWhoIsSending = SingletonManager.Instance.Player;
            return (false, 000, "not implemented");
            //return (successful?, statuscode, msg);
        }

        public static (bool, int, string) reportPlayer(User playerThatIsReported)
        {
            //TODO
            Debug.LogError("DBConnection.reportPlayer() not implemented");
            return (false, 000, "not implemented");
            //return (successful?, statuscode, msg);
        }
        #endregion
    }
}
