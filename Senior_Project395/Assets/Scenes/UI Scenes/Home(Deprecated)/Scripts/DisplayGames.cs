using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// The purpose of this script is to display games on the games display canvas.
/// 
/// All scene Icons must be located in Home Page/Art/Icons and have the same name as its scene.
/// </summary>


public class DisplayGames : MonoBehaviour
{
    // DO NOT CHANGE THE SIZES FROM 10
    // The display is a 2 x 5 format
    public float topBottomMargin; // The top/bottom margin in innerMargins
    public float sideSideMargin; // The side/side margin in innerMargins
    public GameObject gamePrefab; // Games Icons prefab
    public GameObject backgroundCanvas; // The canvas behind the game icons
    public string[] gameNames; // Game names to be displayed under the icon
    public string[] scenes; // References to the scenes to be loaded

    private float[] maxInnerMargins = new float[2]; // Maximum allowed margins between the games, formated as { top/bottom margin, side margins }
    private float[] innerMargins = new float[2]; // The custom set margins between the games, formatted as { top/bottom margin, side margins }
    private float[] outerMargins = new float[2]; // The margins between the games and background canvas, formatted as { top/bottom margin, side margins }
    private RectTransform gameRT; // The RectTransform of the game icons
    private RectTransform canvasRT; // The RectTransform of the Games Display canvas
    private float gameHeight; // The height of the game prefab
    private float gameWidth; // The width of the game prefab


    void Start()
    {
        // Set the RectTransforms
        gameRT = gamePrefab.GetComponent<RectTransform>();
        canvasRT = backgroundCanvas.GetComponent<RectTransform>();

        // Get the dimensions of the game icon prefab
        gameHeight = canvasRT.rect.height + gameRT.rect.height;
        gameWidth = canvasRT.rect.width + gameRT.rect.width;

        // Initialize margins
        maxInnerMargins = new float[] { canvasRT.rect.height - (2 * gameHeight), 
                                       (canvasRT.rect.width - (5 * gameWidth)) / 4 };
        innerMargins = getInnerMargins();
        outerMargins = new float[] { (canvasRT.rect.height - ((2 * gameHeight) + innerMargins[0])) / 2,
                                     (canvasRT.rect.width - ((5 * gameWidth) + (4 * innerMargins[1]))) / 2 };

        // Display the games
        displayGames();
    }


    // Find the custom set margins between the game icons and canvas
    float[] getInnerMargins()
    {
        float tempInnerTBMargin;
        float tempInnerSSMargin;

        // Ensure that margins are not greater than the max margin or less than 0
        if (maxInnerMargins[0] < topBottomMargin)
        {
            tempInnerTBMargin = maxInnerMargins[0];
        }
        else if (topBottomMargin < 0)
        {
            tempInnerTBMargin = 0;
        }
        else
        {
            tempInnerTBMargin = topBottomMargin;
        }

        if (maxInnerMargins[1] < sideSideMargin)
        {
            tempInnerSSMargin = maxInnerMargins[1];
        }
        else if (topBottomMargin < 0)
        {
            tempInnerSSMargin = 0;
        }
        else
        {
            tempInnerSSMargin = sideSideMargin;
        }


        return new float[] { tempInnerTBMargin, tempInnerSSMargin };
    }


    // Show the name of the game on screen
    void setName(GameObject gameInstance, int num)
    {
        // Get the text component
        GameObject tmp = gameInstance.transform.Find("Text (TMP)").gameObject;

        // Change the text
        tmp.GetComponent<TextMeshProUGUI>().text = gameNames[num];
    }


    // Display the game icons on screen
    void displayGames()
    {
        // Find the minimum length between gameNames and scenes
        int totalGames = 10;
        for (int i = 0; i < 10; i++)
        {
            if (String.IsNullOrEmpty(gameNames[i]) || String.IsNullOrEmpty(scenes[i]))
            {
                totalGames = i;
                break;
            }
        }

        // Display the game on the page
        float shiftRight = (.5f * gameWidth) + outerMargins[1];
        float shiftDown = (.5f * gameHeight) + outerMargins[0];
        for (int i = 0; i < totalGames; i++)
        {
            // Start new row
            if (i % 5 == 0)
            {
                shiftRight = (.5f * gameWidth) + outerMargins[1];
                if (i != 0)
                {
                    shiftDown += gameHeight + innerMargins[0];
                }
            }

            // Continue existing row
            else
            {
                shiftRight += gameWidth + innerMargins[1];
            }

            // Instantiate a new game at the proper position
            GameObject newGameInstance = Instantiate(gamePrefab);
            RectTransform gameInstanceRect = newGameInstance.GetComponent<RectTransform>();
            gameInstanceRect.SetParent(canvasRT);
            gameInstanceRect.sizeDelta = new Vector2(gameWidth, gameHeight);
            gameInstanceRect.localScale = Vector2.one;
            gameInstanceRect.localPosition = Vector2.zero;
            gameInstanceRect.localPosition = new Vector2(shiftRight, -shiftDown);

            // Set the game name
            setName(newGameInstance, i);
        }
    }



}
