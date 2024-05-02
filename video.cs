// Import the necessary Unity libraries
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.Video;

// Define a class VideoProgressBar that inherits from MonoBehaviour and implements the interfaces IDragHandler and IPointerDownHandler
public class VideoProgressBar : MonoBehaviour, IDragHandler, IPointerDownHandler
{
    // Declare a serialized private field of type VideoPlayer
    [SerializeField]
    private VideoPlayer videoPlayer;

    // Declare a private field of type Image to represent the progress bar
    private Image progress;
    
    // Awake is called when the script instance is being loaded
    private void Awake()
    {
        // Get the Image component attached to the same GameObject this script is attached to
        progress = GetComponent<Image>();
    }

    // Update is called once per frame
    private void Update()
    {
        // If the video has frames, update the fill amount of the progress bar image
        if (videoPlayer.frameCount > 0)
            progress.fillAmount = (float)videoPlayer.frame / (float)videoPlayer.frameCount;
    }

    // OnDrag is called when the user has clicked on a GameObject and is still holding down the mouse
    public void OnDrag(PointerEventData eventData)
    {
        // Try to skip to a different point in the video based on the drag
        TrySkip(eventData);
    }

    // OnPointerDown is called when the user has clicked on a GameObject
    public void OnPointerDown(PointerEventData eventData)
    {
        // Try to skip to a different point in the video based on the initial click
        TrySkip(eventData);
    }

    // TrySkip attempts to change the current video time based on user input
    private void TrySkip(PointerEventData eventData)
    {
        Vector2 localPoint;
        // If the click/drag event happened within the progress bar
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(
            progress.rectTransform, eventData.position, null, out localPoint))
        {
            // Calculate the percentage of the progress bar that was clicked/dragged on
            float pct = Mathf.InverseLerp(progress.rectTransform.rect.xMin, progress.rectTransform.rect.xMax, localPoint.x);
            // Skip to the corresponding point in the video
            SkipToPercent(pct);
        }
    }

    // SkipToPercent skips to a certain percentage in the video
    private void SkipToPercent(float pct)
    {
        // Calculate the frame that corresponds to the desired percentage
        var frame = videoPlayer.frameCount * pct;
        // Set the video to the calculated frame
        videoPlayer.frame = (long)frame;
    }
}