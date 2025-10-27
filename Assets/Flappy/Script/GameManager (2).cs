using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private Button jumpButton;
    [SerializeField] private Button restartButton;
    [SerializeField] private Image birdImage;
    [SerializeField] private Image flagImage;
    [SerializeField] private GameObject GameOverPanel;

    [Header("Game Settings")]
    public bool isGameActive = true;
    public float jumpForce = 75f;
    public float gravity = -0.2f;
    public float pipeSpeed = -2f;

    void Start()
    {
        Debug.Log("Start called.");
        jumpButton.onClick.AddListener(Clicked);
        restartButton.onClick.AddListener(RestartGame);

        // Hide Game Over UI initially
        GameOverPanel.gameObject.SetActive(false);
        restartButton.gameObject.SetActive(false);
    }

    void Clicked()
    {
        if (!isGameActive) return;

        Debug.Log("Button was clicked");
        birdImage.transform.position += new Vector3(0, jumpForce, 0);
    }

    void Update()
    {
        if (!isGameActive) return;

        // move all pipes to the left
        GameObject[] pipes = GameObject.FindGameObjectsWithTag("Pipe");
        foreach (var pipe in pipes)
            pipe.transform.position += new Vector3(pipeSpeed, 0, 0);
        flagImage.transform.position += new Vector3(pipeSpeed, 0, 0);

        // apply gravity to the bird
        birdImage.transform.position += new Vector3(0, gravity, 0);

        // collision detection
        foreach (var pipe in pipes)
        {
            if (AreRectsOverlapping(pipe.GetComponent<RectTransform>(), birdImage.GetComponent<RectTransform>()))
            {
                GameOver();
            }
        }
    }

    void GameOver()
    {
        Debug.Log("Game Over!");
        isGameActive = false;
        GameOverPanel.gameObject.SetActive(true);
        restartButton.gameObject.SetActive(true);
    }

    void RestartGame()
    {
        Debug.Log("Restarting game...");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    bool AreRectsOverlapping(RectTransform rect1, RectTransform rect2)
    {
        Rect r1 = GetWorldRect(rect1);
        Rect r2 = GetWorldRect(rect2);

        return r1.Overlaps(r2);
    }

    Rect GetWorldRect(RectTransform rt)
    {
        Vector3[] corners = new Vector3[4];
        rt.GetWorldCorners(corners);
        Vector3 bottomLeft = corners[0];
        Vector3 topRight = corners[2];
        return new Rect(bottomLeft.x, bottomLeft.y,
                        topRight.x - bottomLeft.x,
                        topRight.y - bottomLeft.y);
    }
}
