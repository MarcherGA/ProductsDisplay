using UnityEngine;
using UnityEngine.Events;

public class Product : MonoBehaviour
{
    public UnityAction<int> productClicked;

    public int Index { get; set; }
    public string ProductName { 
        get {return _productName;}
        set
        {
            _productName = value;
            ProductTag.NameInputField.text = value;
        } 
    }
    public float Price { 
        get {return _price;}
        set
        {
            _price = value;
            ProductTag.PriceInputField.text = value.ToString();
        } 
    }
    public string Description { 
        get {return _description;}
        set
        {
            _description = value;
            ProductTag.DescriptionText.text = value;
        } 
    }
    public bool CanEdit { 
        get {return _canEdit;}
        set
        {
            _canEdit = value;
            ProductTag.CanEdit = value;
        } 
    }

    [field: SerializeField] public ProductTag ProductTag { get; set; }


    private string _productName;
    private float _price;
    private string _description;
    private bool _canEdit;


    private Collider _productTagCollider;
    void Start()
    {
        _productTagCollider = ProductTag.GetComponent<Collider>();
        ProductTag.OnNameChange += (name) => { ProductName = name; };
        ProductTag.OnPriceChange += (price) => { Price = float.Parse(price); };
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (_productTagCollider.Raycast(ray, out RaycastHit hit, 100))
            {
                productClicked?.Invoke(Index);
            }
        }
    }

    public void ProductFocused()
    {
        CanEdit = true;
    }
    public void ProductUnfocused()
    {
        CanEdit = false;
        ProductTag.ResetTag();
    }
}
