using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CanvasManager : MonoBehaviour
{
    public TextMeshProUGUI health;
    public TextMeshProUGUI armor;
    public TextMeshProUGUI ammo;

    public Image healthIndicator;

    public Sprite Health1; //Full health sprite
    public Sprite Health2;
    public Sprite Health3;  
    public Sprite Health4; //dead sprite

    public GameObject redkey;
    public GameObject bluekey;
    public GameObject greenkey; 

    private const float DamageFlashDuration = 0.18f;

    private static readonly Color NormalTextColor = new Color(0.92f, 0.92f, 0.86f, 1f);
    private static readonly Color HealthyColor = new Color(0.44f, 1f, 0.48f, 1f);
    private static readonly Color WarningColor = new Color(1f, 0.78f, 0.2f, 1f);
    private static readonly Color DangerColor = new Color(1f, 0.2f, 0.12f, 1f);
    private static readonly Color ArmorColor = new Color(0.35f, 0.82f, 1f, 1f);
    private static readonly Color MutedColor = new Color(0.45f, 0.45f, 0.45f, 1f);
    private static readonly Color DamageOverlayColor = new Color(1f, 0f, 0f, 0.24f);
    private static readonly Color TransparentColor = new Color(1f, 0f, 0f, 0f);

    private static CanvasManager _instance;
    private Coroutine damageFeedbackCoroutine;
    private Image damageOverlay;

    public static CanvasManager Instance
    {
        get { return _instance; }
    }

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
            ConfigureHudText(health);
            ConfigureHudText(armor);
            ConfigureHudText(ammo);
            CreateDamageOverlay();
        }
    }

    //Methods to update the UI elements

    public void UpdateHealth(int healthValue)
    {
        healthValue = Mathf.Max(healthValue, 0);
        if (health == null)
        {
            return;
        }

        health.text = healthValue.ToString() + "%";
        health.color = GetPercentColor(healthValue);
        UpdateHealthIndicator(healthValue);
    }

    public void UpdateArmor(int armorValue)
    {
        armorValue = Mathf.Max(armorValue, 0);
        if (armor == null)
        {
            return;
        }

        armor.text = armorValue.ToString() + "%";
        armor.color = armorValue > 0 ? ArmorColor : MutedColor;
    }

    public void UpdateAmmo(int ammoValue)
    {
        UpdateAmmo(ammoValue, -1);
    }

    public void UpdateAmmo(int ammoValue, int maxAmmo)
    {
        ammoValue = Mathf.Max(ammoValue, 0);
        if (ammo == null)
        {
            return;
        }

        ammo.text = maxAmmo > 0 ? ammoValue.ToString() + "/" + maxAmmo.ToString() : ammoValue.ToString("00");
        ammo.color = GetAmmoColor(ammoValue, maxAmmo);
    }


    public void UpdateHealthIndicator(int healthValue)
    {
        if (healthIndicator == null)
        {
            return;
        }

        healthIndicator.color = Color.white;

        // Update health indicator sprite based on current health
        if (healthValue >= 66)
        {
            healthIndicator.sprite = Health1; // Full health
        }
        else if (healthValue < 66 && healthValue > 33)
        {
            healthIndicator.sprite = Health2; // Medium health
        }
        else if (healthValue <= 33 && healthValue > 0)
        {
            healthIndicator.sprite = Health3; // Low health
        }
        else
        {
            healthIndicator.sprite = Health4; // Dead
        }
    }

    public void ShowDamageFeedback()
    {
        if (!isActiveAndEnabled)
        {
            return;
        }

        if (damageFeedbackCoroutine != null)
        {
            StopCoroutine(damageFeedbackCoroutine);
        }

        damageFeedbackCoroutine = StartCoroutine(DamageFeedback());
    }

    public void UpdateKeyUI(string keyColor)
    {
        if(keyColor == "red" && redkey != null)
        {
            redkey.SetActive(true);
        }

        if(keyColor == "blue" && bluekey != null)
        {
            bluekey.SetActive(true);
        }

        if(keyColor == "green" && greenkey != null)
        {
            greenkey.SetActive(true);
        }
        
    }

    public void ClearKeyUI()
    {
        if (redkey != null) redkey.SetActive(false);
        if (bluekey != null) bluekey.SetActive(false);
        if (greenkey != null) greenkey.SetActive(false);
    }

    private void OnDestroy()
    {
        if (_instance == this)
        {
            _instance = null;
        }
    }

    private static void ConfigureHudText(TextMeshProUGUI hudText)
    {
        if (hudText == null)
        {
            return;
        }

        hudText.raycastTarget = false;
        hudText.fontStyle = FontStyles.Bold;
        hudText.color = NormalTextColor;
    }

    private static Color GetPercentColor(int value)
    {
        if (value <= 25)
        {
            return DangerColor;
        }

        if (value <= 50)
        {
            return WarningColor;
        }

        return HealthyColor;
    }

    private static Color GetAmmoColor(int ammoValue, int maxAmmo)
    {
        if (ammoValue <= 0)
        {
            return DangerColor;
        }

        if (maxAmmo > 0 && ammoValue <= Mathf.CeilToInt(maxAmmo * 0.25f))
        {
            return WarningColor;
        }

        if (maxAmmo <= 0 && ammoValue <= 5)
        {
            return WarningColor;
        }

        return NormalTextColor;
    }

    private IEnumerator DamageFeedback()
    {
        Color originalHealthColor = health != null ? health.color : NormalTextColor;
        Color originalIndicatorColor = healthIndicator != null ? healthIndicator.color : Color.white;

        if (health != null)
        {
            health.color = DangerColor;
        }

        if (healthIndicator != null)
        {
            healthIndicator.color = DangerColor;
        }

        float elapsed = 0f;
        while (elapsed < DamageFlashDuration)
        {
            float alpha = Mathf.Lerp(DamageOverlayColor.a, 0f, elapsed / DamageFlashDuration);
            SetDamageOverlayAlpha(alpha);

            elapsed += Time.deltaTime;
            yield return null;
        }

        if (health != null)
        {
            health.color = originalHealthColor;
        }

        if (healthIndicator != null)
        {
            healthIndicator.color = originalIndicatorColor;
        }

        SetDamageOverlayAlpha(0f);
        damageFeedbackCoroutine = null;
    }

    private void CreateDamageOverlay()
    {
        GameObject overlayObject = new GameObject("DamageOverlay", typeof(RectTransform), typeof(CanvasRenderer), typeof(Image));
        overlayObject.transform.SetParent(transform, false);
        overlayObject.transform.SetAsLastSibling();

        RectTransform overlayTransform = overlayObject.GetComponent<RectTransform>();
        overlayTransform.anchorMin = Vector2.zero;
        overlayTransform.anchorMax = Vector2.one;
        overlayTransform.offsetMin = Vector2.zero;
        overlayTransform.offsetMax = Vector2.zero;

        damageOverlay = overlayObject.GetComponent<Image>();
        damageOverlay.color = TransparentColor;
        damageOverlay.raycastTarget = false;
    }

    private void SetDamageOverlayAlpha(float alpha)
    {
        if (damageOverlay == null)
        {
            return;
        }

        damageOverlay.color = new Color(DamageOverlayColor.r, DamageOverlayColor.g, DamageOverlayColor.b, alpha);
    }
}
