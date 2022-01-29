using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class SMAdvertisment : MonoBehaviour
{
    private const string LINK = "http://effingogames.com/linksredirect.php";

    public void FollowTT()
    {
        StartCoroutine(OpenLink("tt"));
    }

    public void FollowYT()
    {
        StartCoroutine(OpenLink("yt"));
    }

    public void FollowIG()
    {
        StartCoroutine(OpenLink("ig"));
    }

    IEnumerator OpenLink(string sm)
    {
        WWWForm form = new WWWForm();
        form.AddField("sm", sm);

        using (UnityWebRequest www = UnityWebRequest.Post(LINK, form))
        {
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(www.error);
            }
            else
            {
                Application.OpenURL(www.downloadHandler.text);
                Debug.Log(www.downloadHandler.text);
            }
        }
    }
}
