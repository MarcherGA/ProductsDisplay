using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class ProductProps
{
    public string name;
    public string description;
    public float price;
}

[System.Serializable]
public class ProductPropsList
{
    public List<ProductProps> products;
}

public class ProductService : MonoBehaviour
{

    public void GetProducts(System.Action<List<ProductProps>> callback)
    {
        string apiUrl = JsonConfigReader.Instance.Config.productsApi;
        StartCoroutine(GetProductsRoutine(apiUrl, callback));
    }

    IEnumerator GetProductsRoutine(string apiUrl, System.Action<List<ProductProps>> callback)
    {
        UnityWebRequest request = UnityWebRequest.Get(apiUrl);
        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            string jsonResponse = request.downloadHandler.text;
            ProductPropsList productList = JsonUtility.FromJson<ProductPropsList>(jsonResponse);

            callback?.Invoke(productList.products);
        }
        else
        {
            Debug.LogError("Error fetching products: " + request.error);
        }
    }
    
}
