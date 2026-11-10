using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EEGDashboard : MonoBehaviour
{
    public EEGInputManager inputManager;

    public Toggle automaticToggle;

    public Slider deltaSlider;
    public Slider thetaSlider;
    public Slider alphaSlider;
    public Slider betaSlider;
    public Slider gammaSlider;

    public TMP_Text deltaText;
    public TMP_Text thetaText;
    public TMP_Text alphaText;
    public TMP_Text betaText;
    public TMP_Text gammaText;

    public TMP_Text modeText;

    void Start()
    {
        automaticToggle.onValueChanged.AddListener(
            inputManager.SetAutomaticMode);

        deltaSlider.onValueChanged.AddListener(
            v => inputManager.SetManualFeature("delta", v));

        thetaSlider.onValueChanged.AddListener(
            v => inputManager.SetManualFeature("theta", v));

        alphaSlider.onValueChanged.AddListener(
            v => inputManager.SetManualFeature("alpha", v));

        betaSlider.onValueChanged.AddListener(
            v => inputManager.SetManualFeature("beta", v));

        gammaSlider.onValueChanged.AddListener(
            v => inputManager.SetManualFeature("gamma", v));

        automaticToggle.SetIsOnWithoutNotify(
            inputManager.automaticMode);
    }

    void Update()
    {
        EEGFeatures f = inputManager.CurrentFeatures;

        UpdateSlider(deltaSlider, deltaText, f.delta);
        UpdateSlider(thetaSlider, thetaText, f.theta);
        UpdateSlider(alphaSlider, alphaText, f.alpha);
        UpdateSlider(betaSlider, betaText, f.beta);
        UpdateSlider(gammaSlider, gammaText, f.gamma);

        bool automatic = inputManager.automaticMode;

        modeText.text = automatic
            ? "Mode: Automatic"
            : "Mode: Manual";

        deltaSlider.interactable = !automatic;
        thetaSlider.interactable = !automatic;
        alphaSlider.interactable = !automatic;
        betaSlider.interactable = !automatic;
        gammaSlider.interactable = !automatic;
    }

    void UpdateSlider(
        Slider slider,
        TMP_Text label,
        float value)
    {
        slider.SetValueWithoutNotify(value);
        label.text = value.ToString("F2");
    }
}