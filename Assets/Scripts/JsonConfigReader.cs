using UnityEngine;

[System.Serializable]
public class Config
{
    public string productsApi;
}

public class JsonConfigReader : MonoBehaviour
{
    public static JsonConfigReader Instance { get; private set; }

    public Config Config { get { return _config; } }
    [SerializeField] private TextAsset _jsonFile;
    private Config _config;





    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);

        _config = JsonUtility.FromJson<Config>(_jsonFile.text);
    }


}
