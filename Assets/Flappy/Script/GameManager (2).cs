using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Button jumpButton;
    [SerializeField] private Image birdImage;
    [SerializeField] private Image flagImage;

    public bool isGameActive = true;
    public float jumpForce = 75f;
    public float gravity = -0.2f;
    public float pipeSpeed = -2f;


    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Start called.");
        jumpButton.onClick.AddListener(Clicked);
    }

    void Clicked()
    {
        Debug.Log("Button was clicked");
        // jump the bird using movetowards
        // birdImage.transform.position = Vector3.MoveTowards(birdImage.transform.position, birdImage.transform.position + new Vector3(0, jumpForce, 0), jumpForce);
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

        foreach (var pipe in pipes)
        {
            if (AreRectsOverlapping(pipe.GetComponent<RectTransform>(), birdImage.GetComponent<RectTransform>()))
            {
                Debug.Log("Images are overlapping!");
                isGameActive = false;
            }
        }
    }

    bool AreRectsOverlapping(RectTransform rect1, RectTransform rect2)
    {
        // Convert rects to world-space Rects
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