using UnityEngine;
using UnityEngine.Video;

public class TVInteraction : MonoBehaviour
{
    // Reference to the VideoPlayer component
    public VideoPlayer videoPlayer;

    // Array to hold the URLs of the videos
    public string[] videoURLs;
    private int currentVideoIndex = 0;

    // Reference to the UI elements for play, pause, and seek
    public GameObject playButton;
    public GameObject pauseButton;
    public GameObject seekBar;

    // Start is called before the first frame update
    void Start()
    {
        // Initialize the video player and UI elements
        videoPlayer = GetComponent<VideoPlayer>();
        playButton.SetActive(true);
        pauseButton.SetActive(false);
        seekBar.SetActive(false);

        // Load the first video
        LoadVideo(currentVideoIndex);
    }

    // Update is called once per frame
    void Update()
    {
        // Existing touch input logic...
    }

    // Method to load and play a video by index
    void LoadVideo(int index)
    {
        videoPlayer.url = System.IO.Path.Combine(Application.streamingAssetsPath, videoURLs[index]);
        videoPlayer.Play();
        playButton.SetActive(false);
        pauseButton.SetActive(true);
        seekBar.SetActive(true);
    }

    // Play the selected video
    void PlayVideo(string videoName)
    {
        // Find the index of the video name in the array
        int videoIndex = System.Array.IndexOf(videoURLs, videoName);
        if (videoIndex != -1)
        {
            LoadVideo(videoIndex);
        }
    }

    // Method to switch to the next video
    public void NextVideo()
    {
        currentVideoIndex = (currentVideoIndex + 1) % videoURLs.Length;
        LoadVideo(currentVideoIndex);
    }

    // Method to switch to the previous video
    public void PreviousVideo()
    {
        if (currentVideoIndex == 0)
        {
            currentVideoIndex = videoURLs.Length - 1;
        }
        else
        {
            currentVideoIndex--;
        }
        LoadVideo(currentVideoIndex);
    }

    // Existing methods for PauseVideo, ResumeVideo, and SeekVideo...
}
