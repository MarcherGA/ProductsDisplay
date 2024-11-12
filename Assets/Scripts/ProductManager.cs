using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using System.Collections.Generic;

public class ProductManager : MonoBehaviour
{
    [SerializeField] private ProductFocus _productFocus;
    [SerializeField] private Transform _parentTransform;  // Reference to the shelf to place products
    [SerializeField] private GameObject _productPrefab;  // Prefab to represent each product on the shelf
    [SerializeField] private float _gap = 0.4f; // Gap between products
    private string _apiUrl; // Your API endpoint
    private List<Product> _products = new List<Product>();


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


    void Start()
    {
        _apiUrl = JsonConfigReader.Instance.Config.productsApi;
        StartCoroutine(GetProductsFromAPI());
    }

    IEnumerator GetProductsFromAPI()
    {
        UnityWebRequest request = UnityWebRequest.Get(_apiUrl);
        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            string jsonResponse = request.downloadHandler.text;
            ProductPropsList productList = JsonUtility.FromJson<ProductPropsList>(jsonResponse);

            DisplayProducts(productList.products);
        }
        else
        {
            Debug.LogError("Error fetching products: " + request.error);
        }
    }

    void DisplayProducts(List<ProductProps> products)
    {
        foreach (Transform child in _parentTransform)
        {
            child.GetComponent<Product>().productClicked -= _productFocus.FocusProduct;
            Destroy(child.gameObject);  // Clear existing products
        }

        if (products.Count == 0) return;

        float firstXPosition = -(products.Count - 1) * (_gap / 2);
        Transform[] PriceTags =  new Transform[products.Count];

        foreach (var product in products)
        {
            Product productObj = Instantiate(_productPrefab, _parentTransform).GetComponent<Product>();
            productObj.Index = products.IndexOf(product);
            productObj.productClicked +=_productFocus.FocusProduct;

            PriceTags[productObj.Index] = productObj.ProductTag.transform;

            productObj.transform.localPosition = new Vector3(firstXPosition + productObj.Index * _gap, 0.0f, 0.0f);
            
            productObj.ProductName = product.name;
            productObj.Description = product.description;
            productObj.Price = product.price;

            productObj.CanEdit = false;

            _products.Add(productObj);
        }

        _productFocus.SetCameraTargets(PriceTags);
        _productFocus.productFocused.AddListener((index) => { _products[index].ProductFocused(); });
        _productFocus.productUnfocused.AddListener((index) => { _products[index].ProductUnfocused(); });
    }
}
