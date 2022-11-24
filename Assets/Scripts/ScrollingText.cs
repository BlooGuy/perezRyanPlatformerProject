using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScrollingText : MonoBehaviour
{
    [Header("Text Setting")]
    [SerializeField][TextArea] private string[] itemInfo;
    [SerializeField] private float textSpeed = 0.01f;

    [Header("UI Elements")]
    [SerializeField] private TextMeshProUGUI itemInfoText;
    private int currentlyDisplayingText = 0;
  //  public AudioClip typeSound;
    public void Start()
    {
        StartCoroutine(AnimateText());
    }

    IEnumerator AnimateText()
    {

        for (int i = 0; i < itemInfo[currentlyDisplayingText].Length + 1; i++) 
        {
            itemInfoText.text = itemInfo[currentlyDisplayingText].Substring(0, i);
            yield return new WaitForSeconds(textSpeed);
          //  AudioSource.PlayClipAtPoint(typeSound, transform.position);
        }
    }
}
