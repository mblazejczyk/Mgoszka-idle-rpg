using UnityEngine;

public class DownloadUpdate : MonoBehaviour
{
    public void UrlOpener(string url)
    {
        Application.OpenURL(url);
    }
}
