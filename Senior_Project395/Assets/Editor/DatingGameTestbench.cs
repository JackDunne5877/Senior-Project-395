using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Dating_Platform;

public class DatingGameTestbench : EditorWindow
{

    // Opens our GUI window when we use the menu entry
    [MenuItem("DatingGameTestbench/Open test GUI")]
    public static void OpenWindow()
    {
        GetWindow<DatingGameTestbench>();
    }

    // text values to keep between tests
    private static string testUserId;
    private static string password;

    void OnEnable()
    {
        // cache any data you need here.
        // if you want to persist values used in the inspector, you can use eg. EditorPrefs
    }

    // runs every time you interact with the GUI
    void OnGUI()
    {
        //
        // inputs for test
        //

        testUserId = EditorGUILayout.TextField("User ID to test", testUserId);
        password = EditorGUILayout.TextField("password", password);

        //
        // buttons for individual tests
        //

        // DatabaseConnection.GetNameRequest
        if (GUILayout.Button("test get name"))
        {
            Debug.Log("User ID to test: " + testUserId);

            DatabaseConnection.HttpResponse res = DatabaseConnection.GetNameRequest(testUserId);

            Debug.Log("Status: " + res.statusCode + "; Content: " + res.content);
        }

        // DatabaseConnection.GetBioRequest
        if (GUILayout.Button("test get bio"))
        {
            Debug.Log("User ID to test: " + testUserId);

            DatabaseConnection.HttpResponse res = DatabaseConnection.GetBioRequest(testUserId);

            Debug.Log("Status: " + res.statusCode + "; Content: " + res.content);
        }

        // ConfirmPlayerPasswordRequest
        if (GUILayout.Button("test ConfirmPlayerPasswordRequest"))
        {
            Debug.Log("User ID to test: " + testUserId);

            DatabaseConnection.HttpResponse res = DatabaseConnection.ConfirmPlayerPasswordRequest(testUserId, password);

            Debug.Log("Status: " + res.statusCode + "; Content: " + res.content);
        }

        // ConfirmPlayerPassword
        if (GUILayout.Button("test ConfirmPlayerPassword"))
        {
            Debug.Log("User ID to test: " + testUserId);

            bool res = DatabaseConnection.ConfirmPlayerPassword(testUserId, password);

            Debug.Log("ConfirmPlayerPassword: " + res);
        }
    }
}
