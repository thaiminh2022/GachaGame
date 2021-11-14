using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;
using TMPro;

public class PopCat : MonoBehaviour
{
    public int ClickCounter;
    [SerializeField] float switchImageDelay;

    [Header("Images")]
    [SerializeField] private Sprite popCatHold;
    [SerializeField] private Sprite popCatOpen;
    [SerializeField] private Image popCatDisplayImage;

    [Header("Text and Audio")]
    [SerializeField] TextMeshProUGUI clickTextCounter;
    [SerializeField] GameObject[] audioSourcesGameObbject;

    [Header("Effects")]
    [SerializeField] GameObject particlesOnTap;


    public void OnClick()
    {
        StartCoroutine(OnClickHandeler());

    }

    IEnumerator OnClickHandeler()
    {
        ClickCounter++;
        popCatDisplayImage.sprite = popCatOpen;

        OnTapHandeler();
        OnTapAudioHandeler();

        yield return new WaitForSeconds(switchImageDelay);

        popCatDisplayImage.sprite = popCatHold;
    }



    public void EndPopCatGame()
    {
        MoneyManager.instance?.ChangeMoneyByAmmout(ClickCounter);
    }
    public void SwitchToMain(int mainSceneIndex)
    {
        SceneManager.LoadScene(mainSceneIndex);
    }


    private GameObject ChooseRandomAdudioGameObject()
    {
        int index = Random.Range(0, audioSourcesGameObbject.Length);
        return audioSourcesGameObbject[index];
    }

    private void OnTapAudioHandeler()
    {
        GameObject go = Instantiate(ChooseRandomAdudioGameObject(), transform.position, Quaternion.identity);
        Destroy(go, 1f);
    }
    private void OnTapHandeler()
    {
        if (Input.touchCount > 0)
        {
            Touch tap = Input.GetTouch(0);
            Vector2 tapPositionWorld = Camera.main.ScreenToWorldPoint(tap.position);

            GameObject go = Instantiate(particlesOnTap, tapPositionWorld, Quaternion.identity);
            Destroy(go, 1.5f);
        }
    }


    private void Update()
    {
        ClickCounter = ClickCounter < int.MaxValue ? ClickCounter : int.MaxValue;

        clickTextCounter.text = "Tap: " + ClickCounter.ToString();
    }
}