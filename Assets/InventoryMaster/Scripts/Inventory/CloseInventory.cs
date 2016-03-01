using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CloseInventory : MonoBehaviour, IPointerDownHandler
{

    Inventory inv;
    public GameObject characterSystem;
    private Inventory characterSystemInventory;

    void Start()
    {
        inv = transform.parent.GetComponent<Inventory>();

        if (characterSystem != null)
            characterSystemInventory = characterSystem.GetComponent<Inventory>();

    }
    public void OnPointerDown(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            inv.closeInventory();
            characterSystemInventory.closeInventory();
        }
    }
}
