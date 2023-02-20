using UnityEngine;

public static class PlayerPrefsSaver<T>
{
    public static T Load(string id)
    {
        var jsonString = PlayerPrefs.GetString(id);
        return (T)JsonUtility.FromJson(jsonString, typeof(T));
    }

    public static void Save(string id, T data)
    {
        var jsonString = JsonUtility.ToJson(data);
        PlayerPrefs.SetString(id, jsonString);
    }
}
