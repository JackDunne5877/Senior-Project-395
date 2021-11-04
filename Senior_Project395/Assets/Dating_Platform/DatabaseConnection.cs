using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Dating_Platform
{

    public static class DatabaseConnection
    {
        public static Sprite samplePlayerImage = Sprite.Create(new Texture2D(200, 200), new Rect(), Vector2.one);
        public static Player getOwnedPlayerInfo(string playerId, string password)
        {
            //TODO check if password works for the given playerId
            return new Player();
        }
        public static Player getPublicPlayerInfo(string playerId)
        {
            Player resPlayer = new Player();
            resPlayer.DisplayName = "samplePlayerName"; //TODO should be acquired from DB
            resPlayer.ProfileImg = samplePlayerImage; //TODO should be acquired from DB
            resPlayer.PlayerID = playerId;
            return resPlayer;
        }



        public static Player getConnectionPlayerInfo(Player myPlayer, string password, string connectionId)
        {
            //TODO check if password works for the given playerId
            if (ConfirmPlayerPassword(myPlayer.PlayerID, password))
            {
                if (ConfirmPlayerHasConnection(myPlayer, connectionId))
                {
                    //TODO all values should really be acquired from DB
                    Player resPlayer = new Player();
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
                    return new Player()
                    {
                        PlayerID = "NOTAUTHORIZED",
                        bio = "not authorized to get this connection info"
                    };
                }
            }
            else
            {
                Debug.LogWarning("Get Connection Player Info: password incorrect");
                return new Player()
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

        public static bool ConfirmPlayerHasConnection(Player myPlayer, string connectionPlayerId)
        {
            //TODO get results from DB
            return (Array.IndexOf(myPlayer.connectionIds, connectionPlayerId) > -1);
        }

        public static void setNewAccountEmail(string myPlayerId, string password, string newEmailAddress)
        {
            if(ConfirmPlayerPassword(myPlayerId, password))
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

        public static void logoutClicked()
        {
            SingletonManager.Instance.Player = null;
        }
    }
}
