using BazaarInAppBilling;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(StoreHandler), true)]
public class StoreHandlerEditor : Editor {

    private readonly string version = "1.1";
    private SerializedProperty products, publicKey, payload, editorDummyResponse, validatePurchases, clientId, clientSecret, refreshToken;
    
    private StoreHandler storeHandler;

    private void OnEnable()
    {
        storeHandler = target as StoreHandler;

        products = serializedObject.FindProperty("products");
        publicKey = serializedObject.FindProperty("publicKey");
        payload = serializedObject.FindProperty("payload");
        editorDummyResponse = serializedObject.FindProperty("editorDummyResponse");
        validatePurchases = serializedObject.FindProperty("validatePurchases");
        clientId = serializedObject.FindProperty("clientId");
        clientSecret = serializedObject.FindProperty("clientSecret");
        refreshToken = serializedObject.FindProperty("refreshToken");
    }
    
    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        Settings();
        FooterInformation();

        serializedObject.ApplyModifiedProperties();
    }

    private void Settings()
    {
        EditorGUILayout.Space();
        EditorGUILayout.Space();

        EditorGUILayout.PropertyField(products, new GUIContent("products", "First define your products in CafeBazaar panel."), true);
        
        EditorGUILayout.PropertyField(publicKey, new GUIContent("Public Key", "RSA Key from CafeBazaar."));

        EditorGUILayout.PropertyField(payload, new GUIContent("Payload", "Arbitrary value to identify purchases."));

        EditorGUILayout.PropertyField(editorDummyResponse, new GUIContent("Editor Dummy Response", "If checked all the operations will call success events."));

        EditorGUILayout.PropertyField(validatePurchases, new GUIContent("Validate Purchases", "Validate purchases using CafeBazaar developer API."));
       
        if (storeHandler.validatePurchases)
        {
            EditorGUI.indentLevel++;
            EditorGUILayout.PropertyField(clientId, new GUIContent("Client Id"));
            EditorGUILayout.PropertyField(clientSecret, new GUIContent("Client Secret"));
            EditorGUILayout.PropertyField(refreshToken, new GUIContent("Refresh Token"));
            EditorGUI.indentLevel--;
        }
    }

    private void FooterInformation()
    {
        EditorGUILayout.Space();
        EditorGUILayout.Space();

        GUILayout.BeginVertical("HelpBox");

        GUIStyle style = new GUIStyle(EditorStyles.label);
        style.normal.textColor = Color.black;
        style.fontSize = 20;
        style.alignment = TextAnchor.MiddleLeft;
        GUILayout.Label("Cafebazaar IAB Plugin", style);

        EditorGUILayout.Space();
        style.normal.textColor = Color.gray;
        style.fontSize = 18;
        GUILayout.Label("Version: " + version, style);

        EditorGUILayout.Space();
        style.normal.textColor = Color.gray;
        GUILayout.Label("Author: Hojjat.Reyhane", style);
        GUILayout.EndVertical();
    }
    
}
