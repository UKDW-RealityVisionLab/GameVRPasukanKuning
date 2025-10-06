using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
using UnityEngine.XR.Interaction.Toolkit.Interactors;

[DisallowMultipleComponent]
public class VRSellSocket : MonoBehaviour
{
    [SerializeField] private XRSocketInteractor socket;
    [SerializeField] private SellerOffer sellerOffer;

    // track last selection to avoid repeated processing every frame
    private IXRSelectInteractable lastSelected;

    void Reset()
    {
        // try auto-assign if not set in inspector
        if (socket == null) socket = GetComponent<XRSocketInteractor>();
    }

    void Update()
    {
        if (socket == null || sellerOffer == null) return;

        // If there's at least one selected interactable, use the first (oldest) one
        if (socket.interactablesSelected.Count > 0)
        {
            IXRSelectInteractable selected = socket.interactablesSelected[0];

            // only act when selection changed
            if (selected != lastSelected)
            {
                lastSelected = selected;

                // IXRSelectInteractable implementations in the XR toolkit are Components (XRBaseInteractable)
                // so we can cast to Component to access GameObject/Transform
                Component comp = selected as Component;
                if (comp != null)
                {
                    GameObject selectedGO = comp.gameObject;
                    SellableItem item = selectedGO.GetComponent<SellableItem>();
                    if (item != null)
                    {
                        sellerOffer.GetOfferPrice(item);
                    }
                }
            }
        }
        else
        {
            // nothing in socket -> reset
            lastSelected = null;
        }
    }
}
