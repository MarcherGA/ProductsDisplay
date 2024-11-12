using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ProductTag : MonoBehaviour
{
    public UnityAction<string> OnNameChange;
    public UnityAction<string> OnPriceChange;
    
    public bool CanEdit { get; set; }

    [field: SerializeField] public TMP_InputField PriceInputField { get; private set; }
    [field: SerializeField] public TMP_InputField NameInputField { get; private set; }
    [field: SerializeField] public TMP_Text DescriptionText { get; private set; }
    [field: SerializeField] public Button EditButton { get; private set; }
    [field: SerializeField] private GameObject _editSubmitPanel;
    [field: SerializeField] private GameObject _finalConfirmPanel;
    [field: SerializeField] private TMP_Text _finalConfirmText;
    [field: SerializeField] private GameObject _editConfirmedPanel;

    private TMP_InputField _currentHoveredInput;
    private bool isInEditMode = false;
    private string _initialEditValue;
    private float _currentPosY;


    public void OnInputPointerEnter(BaseEventData eventData)
    {
        if (isInEditMode) return;

        PointerEventData pointerEventData = (PointerEventData)eventData;
        EditButton.gameObject.SetActive(true);
        RectTransform targetInput = pointerEventData.pointerEnter.transform.parent.parent.GetComponent<RectTransform>();
        _currentPosY = targetInput.anchoredPosition.y;
        RectTransform EditButtonRectTransform = EditButton.GetComponent<RectTransform>();
        Vector3 btnPos = EditButtonRectTransform.anchoredPosition;
        btnPos.Set(btnPos.x, _currentPosY, btnPos.z);
        EditButtonRectTransform.anchoredPosition = btnPos;
        
        _currentHoveredInput = pointerEventData.pointerEnter.transform.parent.parent.gameObject.GetComponent<TMP_InputField>();
    }

    public void OnInputPointerExit(BaseEventData eventData)
    {
        if (isInEditMode) return;

        PointerEventData pointerEventData = (PointerEventData)eventData;
        if(pointerEventData.pointerCurrentRaycast.gameObject == EditButton.gameObject) return;
        EditButton.gameObject.SetActive(false);
        
    }

    public void OnEditButtonPointerExit(BaseEventData eventData)
    {
        EditButton.gameObject.SetActive(false);
    }

    public void OnEditButtonClicked()
    {
        if(_currentHoveredInput == null || !CanEdit) return;

        EnableInput(_currentHoveredInput);

        EditButton.gameObject.SetActive(false);

        _editSubmitPanel.SetActive(true);
        RectTransform EditSubmitPanelRectTransform = _editSubmitPanel.GetComponent<RectTransform>();
        Vector3 panelPos = EditSubmitPanelRectTransform.anchoredPosition;
        panelPos.Set(panelPos.x, _currentPosY, panelPos.z);
        EditSubmitPanelRectTransform.anchoredPosition = panelPos;
        
        isInEditMode = true;
        
    }

    private void EnableInput(TMP_InputField targetInput)
    {
        targetInput.interactable = true;
        targetInput.Select();
        _initialEditValue = targetInput.text;
    }

    public void CancelEdit()
    {
        _currentHoveredInput.text = _initialEditValue;
        ResetEdit();
    }



    public void OpenFinalConfirmPanel()
    {
        if(_currentHoveredInput.text == _initialEditValue) 
        {
            CloseFinalConfirmPanel();
            return;
        }
        
        _finalConfirmPanel.SetActive(true);
        string initialName = _currentHoveredInput == NameInputField ? _initialEditValue : NameInputField.text;
        _finalConfirmText.text = "Are you sure you want to change " + initialName  + " " + GetInputFieldName(_currentHoveredInput).ToLower() + " to " + _currentHoveredInput.text + "?";
        
    }

    public void CloseFinalConfirmPanel()
    {
        _finalConfirmPanel.SetActive(false);
        CancelEdit();
    }

    public void OpenEditConfirmedPanel()
    {
        _finalConfirmPanel.SetActive(false);
        _editConfirmedPanel.SetActive(true);
        ConfirmEdit(_currentHoveredInput);
    }

    public void CloseEditConfirmedPanel()
    {
        _editConfirmedPanel.SetActive(false);
        ResetEdit();
    }

    public void ResetTag()
    {
        if(!isInEditMode) return;

        if(_editConfirmedPanel.activeSelf)
        {
            CloseEditConfirmedPanel();
        }
        else if(_finalConfirmPanel.activeSelf)
        {
            CloseFinalConfirmPanel();
        }
        else
        {
            CancelEdit();
        }
    }

    private void ResetEdit()
    {
        _currentHoveredInput.interactable = false;
        _editSubmitPanel.SetActive(false);
        isInEditMode = false;
    }

    private string GetInputFieldName(TMP_InputField inputField)
    {
        return inputField.name.Substring(0, inputField.name.IndexOf("InputField"));
    }

    private void ConfirmEdit(TMP_InputField inputField)
    {
        if (inputField == NameInputField)
        {
            OnNameChange?.Invoke(inputField.text);
        }
        else
        {
            OnPriceChange?.Invoke(inputField.text);
        }

    }


}
