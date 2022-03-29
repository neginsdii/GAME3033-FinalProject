using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class HealthInfoUI : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI healthText;
    [SerializeField]
    private TextMeshProUGUI MaxHealthText;
    HealthComponent playerHealthComponent;

    private void OnEnable()
    {
        PlayerEvents.OnHealthInitialized += OnHealthInitialized;
    }

    private void OnDisable()
    {
        PlayerEvents.OnHealthInitialized -= OnHealthInitialized;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnHealthInitialized(HealthComponent healthComponent)
    {
        playerHealthComponent = healthComponent;
    }
    public void updateHealthText()
    {
        healthText.text = playerHealthComponent.health.ToString();
        MaxHealthText.text = playerHealthComponent.MaxHealth.ToString();
    }


    // Update is called once per frame
    void Update()
    {
        updateHealthText();
    }
}
