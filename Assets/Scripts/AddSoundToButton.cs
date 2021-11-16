using UnityEngine.UI;
using UnityEngine;

public class AddSoundToButton : MonoBehaviour
{
    [SerializeField] private Button[] clickButton;
    [SerializeField] private Button[] clickOffButton;


    private void Start()
    {
        foreach (var button in clickButton)
        {
            button.onClick.AddListener(() =>
            {
                SoundsManager.instance.Play("Click");
            });
        }

        foreach (var button in clickOffButton)
        {
            button.onClick.AddListener(() => { SoundsManager.instance.Play("ClickOff"); });
        }
    }
}
