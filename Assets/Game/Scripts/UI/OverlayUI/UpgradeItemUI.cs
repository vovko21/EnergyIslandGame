using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeItemUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _text;
    [SerializeField] private Button _buyButton;

    private BuildingStat _buildingStat;

    public BuildingStat BuildingStat => _buildingStat;
    public Button BuyButton => _buyButton;

    public void Initialize(BuildingStat buildingStat)
    {
        _buildingStat = buildingStat;

        if(_buildingStat == null)
        {
            _text.text = "MAX";
        }
        else
        {
            DisplayText();
        }
    }

    private void DisplayText()
    {
        _text.text = _buildingStat.Price + " " + _buildingStat.Value.ToString();
    }
}
