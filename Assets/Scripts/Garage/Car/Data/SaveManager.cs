using UnityEngine;

public static class SaveManager
{
    public static void Save<T>(string key, T saveData)
    {
        string jsonDataString = JsonUtility.ToJson(saveData, true);
        string encryptJsonData = jsonDataString.Encrypt();
        Debug.Log(jsonDataString);
        PlayerPrefs.SetString(key, encryptJsonData);
    }

    public static T Load<T>(string key) where T : new()
    {
        if (PlayerPrefs.HasKey(key))
        {
            string loadedString = PlayerPrefs.GetString(key);
            string descryptedJsonData = loadedString.Decrypt();
            Debug.Log(descryptedJsonData);
            return JsonUtility.FromJson<T>(descryptedJsonData);
        }
        else
        {
            return new T();
        }
    }
}