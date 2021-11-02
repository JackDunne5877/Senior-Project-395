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
            if (ConfirmPlayerPassword(myPlayer.PlayerID, password) && ConfirmPlayerHasConnection(myPlayer, connectionId))
            {
                //TODO all values should really be acquired from DB
                Player resPlayer = new Player();
                resPlayer.DisplayName = "samplePlayerName";
                resPlayer.ProfileImg = samplePlayerImage;
                resPlayer.PlayerImages = new Sprite[] { samplePlayerImage, samplePlayerImage };
                resPlayer.age = 18;
                resPlayer.bio = "sample Player Bio";
                resPlayer.genderIdentity = GenderOption.Male;
                resPlayer.genderPreferences = new GenderOption[] { GenderOption.Male, GenderOption.Female, GenderOption.NonBinary };
                resPlayer.PlayerID = connectionId;
                return resPlayer;
            }
            else
            {
                return new Player()
                {
                    PlayerID = "NOTAUTHORIZED",
                    bio = "not authorized to get this connection info"
                };
            }

            
        }

        public static bool ConfirmPlayerPassword(string myPlayerId, string password)
        {
            string samplepassword = "1234";
            string samplePlayerID = "abc";
            return (myPlayerId == samplePlayerID && password == samplepassword);
        }

        public static bool ConfirmPlayerHasConnection(Player myPlayer, string connectionPlayerId)
        {
            //TODO get results from DB
            return (Array.IndexOf(myPlayer.connectionIds, connectionPlayerId) > -1);
        }
    }
}
