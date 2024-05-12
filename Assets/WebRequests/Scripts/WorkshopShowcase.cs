using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using CodeMonkey;
using CodeMonkey.Utils;

public class WorkshopShowcase : MonoBehaviour {
    
    private List<Texture2D> workshopThumbnailList = new List<Texture2D>();
    private Transform container;
    private Transform thumbnailTemplate;

    private void Awake() {
        container = transform.Find("containerMask").Find("container");
        thumbnailTemplate = container.Find("thumbnailTemplate");
    }

    private void Start() {
        DownloadWorkshopShowcase();
    }

    private void PrintThumbnails()
    {
        // Clear Previous Thumbnails
        foreach (Transform child in container)
        {
            if (child == thumbnailTemplate) continue;
            Destroy(child.gameObject);
        }

        // Create Thumbnails
        for (int i = 0; i < workshopThumbnailList.Count; i++)
        {
            Transform thumbnailTransform = Instantiate(thumbnailTemplate, container);
            thumbnailTransform.gameObject.SetActive(true);

            Vector2 startingPos = new Vector2(108 * i, 0);
            RectTransform rectTransform = thumbnailTransform.GetComponent<RectTransform>();
            rectTransform.anchoredPosition = startingPos;
            thumbnailTransform.GetComponent<RawImage>().texture = workshopThumbnailList[i];
        }
    }


    private void RandomizeList() {
        if (workshopThumbnailList == null || workshopThumbnailList.Count == 0) return;
        // Randomize list
        for (int i=0; i<50; i++) {
            int rnd = UnityEngine.Random.Range(0, workshopThumbnailList.Count);
            Texture2D tmp = workshopThumbnailList[0];
            workshopThumbnailList[0] = workshopThumbnailList[rnd];
            workshopThumbnailList[rnd] = tmp;
        }
    }

    private void DownloadWorkshopShowcase()
    {
            string imageUrl = "http://localhost/unity/Discs/" + DBManager.ringID + ".png";
        Debug.Log(DBManager.ringID);
            Debug.Log(imageUrl);

            GetTexture(imageUrl, (string error) => {
                Debug.Log("Failed to download thumbnail");
                Debug.Log("Error: " + error);
            }, (Texture2D texture) => {
                workshopThumbnailList.Add(texture);
                RandomizeList();
                PrintThumbnails();
                Debug.Log("Workshop showcase amount: " + workshopThumbnailList.Count);
            });
    }


    public void Get(string url, Action<string> onError, Action<string> onSuccess) {
        WebRequests.Get(url, onError, onSuccess);
    }

    public void GetTexture(string url, Action<string> onError, Action<Texture2D> onSuccess) {
        WebRequests.GetTexture(url, onError, onSuccess);
    }

}
