using UnityEngine;
using UnityEngine.UI;

public class PlayAudioButton : MonoBehaviour
{
    private Button button;
    [SerializeField] AudioRecorderVR audioRecorderVR;
    private void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(OnButtonClick);
    }

    private void OnButtonClick()
    {
        Debug.Log("Play Audio!");
        audioRecorderVR.PlayRecordedAudio();
    }
}
