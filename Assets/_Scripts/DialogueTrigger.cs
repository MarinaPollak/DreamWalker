using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    [Header("Visual Cue")]
    [SerializeField] private GameObject visualCue;

    [Header("Ink JSON")]
    [SerializeField] private TextAsset inkJSON;

    private bool playerInRange;

    private void Awake()
    {
        playerInRange = false;
        if (visualCue != null)
        {
            visualCue.SetActive(false);
        }
    }

    private void Update()
    {
        if (playerInRange && DialogueManager.GetInstance() != null && !DialogueManager.GetInstance().IsDialoguePlaying())
        {
            if (visualCue != null)
            {
                visualCue.SetActive(true);
            }

            // Check for interaction input (E key or Space)
            if (Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.Space))
            {
                Debug.Log("Triggering dialogue!");
                DialogueManager.GetInstance().EnterDialogueMode(inkJSON);
            }
        }
        else
        {
            if (visualCue != null)
            {
                visualCue.SetActive(false);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
        }
    }

    // For 2D games, use OnTriggerEnter2D and OnTriggerExit2D instead
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player entered dialogue trigger zone!");
            playerInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player exited dialogue trigger zone!");
            playerInRange = false;
        }
    }
}
