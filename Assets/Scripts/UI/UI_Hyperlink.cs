using UnityEngine;

public class UI_Hyperlink : MonoBehaviour
{
    [SerializeField] private string url;

    public void OpenUrl() => Application.OpenURL(url);
}
