using UnityEngine;

[ExecuteInEditMode]
public class MobileUIValidator : MonoBehaviour
{
    [SerializeField] private GameObject mobileUIPanel;
    private void OnValidate()
    {
#if UNITY_ANDROID
        mobileUIPanel.SetActive(true);
#else
        mobileUIPanel.SetActive(false);
#endif
    }
}
