using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class IconSlot : MonoBehaviour
{
    private ItemScript Item;

    private Button ItemButton;
    private TMP_Text ItemText;

    [SerializeField] private ItemSlotAmountCanvas AmountWidget;
    [SerializeField] private ItemSlotEquippedCanvas EquippedWidget;
    [SerializeField] private Image icon;
    private AudioSource audioSource;
    void Awake()
    {
        ItemButton = GetComponent<Button>();
        ItemText = GetComponentInChildren<TMP_Text>();
        audioSource = GetComponent<AudioSource>();
    }

    public void Initialize(ItemScript item)
    {
        Item = item;
        ItemText.text = Item.name;
        AmountWidget.Initialize(item);
        EquippedWidget.Initialize(item);
        icon.sprite = item.icon.sprite;
        ItemButton.onClick.AddListener(UseItem);
        Item.OnItemDestroyed += OnItemDestroyed;
    }

    public void UseItem()
    {
        Debug.Log($"{Item.name} used!");
        Item.UseItem(Item.controller);
        if (!audioSource.isPlaying)
            audioSource.Play();
    }

    private void OnItemDestroyed()
    {
        Item = null;
        Destroy(gameObject);
    }

    private void OnDisable()
    {
        if (Item) Item.OnItemDestroyed -= OnItemDestroyed;
    }
}
