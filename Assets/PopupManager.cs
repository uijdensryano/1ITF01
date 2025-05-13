using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;


public class PopupManager : MonoBehaviour
{
    public LineDrawScript lineDrawer;
    public DriveCar2D carScript;
    public GameObject panel1;
    public Button openPopupButton1;
    public TMP_InputField inputField_coord1_1;
    public TMP_InputField inputField_coord1_2;

    public GameObject panel2;
    public Button openPopupButton2;
    public TMP_InputField inputField_coord2_1;
    public TMP_InputField inputField_coord2_2;

    public GameObject functionInputPanel;
    public TMP_InputField functionInputField;

    [SerializeField] private int checkX_coord1 = 2;
    [SerializeField] private int checkY_coord1 = 5;
    [SerializeField] private int checkX_coord2 = 2;
    [SerializeField] private int checkY_coord2 = 5;

    private bool coord1Correct = false;
    private bool coord2Correct = false;
    private bool functionInputCorrect = false;

    void Start()
    {
        panel1.SetActive(false);
        panel2.SetActive(false);
        functionInputPanel.SetActive(false);

        inputField_coord1_1.onSubmit.AddListener(delegate { ValidateAndSubmitCoord1(); });
        inputField_coord1_2.onSubmit.AddListener(delegate { ValidateAndSubmitCoord1(); });
        inputField_coord2_1.onSubmit.AddListener(delegate { ValidateAndSubmitCoord2(); });
        inputField_coord2_2.onSubmit.AddListener(delegate { ValidateAndSubmitCoord2(); });
        functionInputField.onSubmit.AddListener(delegate { SubmitFunctionValue(); });
    }

    public void OpenPopupCoord1()
    {
        if (!coord1Correct)
        {
            panel1.SetActive(!panel1.activeSelf);
            if (panel1.activeSelf)
            {
                panel2.SetActive(false); // Sluit panel2 als deze open is
                inputField_coord1_1.text = "";
                inputField_coord1_2.text = "";
                inputField_coord1_1.ActivateInputField();
            }
        }
    }

    public void OpenPopupCoord2()
    {
        if (!coord2Correct)
        {
            panel2.SetActive(!panel2.activeSelf);
            if (panel2.activeSelf)
            {
                panel1.SetActive(false); // Sluit panel1 als deze open is
                inputField_coord2_1.text = "";
                inputField_coord2_2.text = "";
                inputField_coord2_1.ActivateInputField();
            }
        }
    }

    private void ValidateAndSubmitCoord1()
    {
        if (string.IsNullOrWhiteSpace(inputField_coord1_1.text))
        {
            inputField_coord1_2.ActivateInputField();
            return;
        }
        if (string.IsNullOrWhiteSpace(inputField_coord1_2.text))
        {
            inputField_coord1_2.ActivateInputField();
            return;
        }
        SubmitValueCoord1();
    }

    private void ValidateAndSubmitCoord2()
    {
        if (string.IsNullOrWhiteSpace(inputField_coord2_1.text))
        {
            inputField_coord2_2.ActivateInputField();
            return;
        }
        if (string.IsNullOrWhiteSpace(inputField_coord2_2.text))
        {
            inputField_coord2_2.ActivateInputField();
            return;
        }
        SubmitValueCoord2();
    }

    public void SubmitValueCoord1()
    {
        if (int.TryParse(inputField_coord1_1.text, out int parsedX) && int.TryParse(inputField_coord1_2.text, out int parsedY))
        {
            if (parsedX == checkX_coord1 && parsedY == checkY_coord1)
            {
                panel1.SetActive(false);
                coord1Correct = true;
                openPopupButton1.image.color = Color.green;
                CheckAllCorrect();
            }
            else
            {
                StartCoroutine(ShakeEffect(panel1.GetComponent<RectTransform>()));
            }
        }
    }

    public void SubmitValueCoord2()
    {
        if (int.TryParse(inputField_coord2_1.text, out int parsedX) && int.TryParse(inputField_coord2_2.text, out int parsedY))
        {
            if (parsedX == checkX_coord2 && parsedY == checkY_coord2)
            {
                panel2.SetActive(false);
                coord2Correct = true;
                openPopupButton2.image.color = Color.green;
                CheckAllCorrect();
            }
            else
            {
                StartCoroutine(ShakeEffect(panel2.GetComponent<RectTransform>()));
            }
        }
    }

    private void CheckAllCorrect()
    {
        if (coord1Correct && coord2Correct)
        {
            functionInputPanel.SetActive(true);
            functionInputField.ActivateInputField();
        }
    }

    private void SubmitFunctionValue()
    {
        if (!string.IsNullOrWhiteSpace(functionInputField.text))
        {
            functionInputCorrect = true;
            Debug.Log("Function input is valid: " + functionInputField.text);
            lineDrawer.DrawLine(functionInputField.text);
            carScript.StartCar();
            functionInputPanel.SetActive(false);
            
        }
        else
        {
            Debug.Log("Function input is invalid.");
        }
    }

    IEnumerator ShakeEffect(RectTransform rect)
    {
        Vector3 originalPos = rect.anchoredPosition;
        float duration = 0.3f;
        float magnitude = 10f;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            float xOffset = Random.Range(-magnitude, magnitude);
            rect.anchoredPosition = new Vector3(originalPos.x + xOffset, originalPos.y, originalPos.z);
            elapsed += Time.deltaTime;
            yield return null;
        }

        rect.anchoredPosition = originalPos;
    }
}
