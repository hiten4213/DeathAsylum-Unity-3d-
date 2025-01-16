using UnityEngine;

public class DoorScript : MonoBehaviour
{
    public float openAngleX = 0f; // The angle to which the door will open
    public float openAngleY = 90f;
    public float openAngleZ = 0f;
    public float openSpeed = 2f;  // How quickly the door opens
    public Transform doorTransform; // Assign the door's Transform in the Inspector

    private bool isPlayerNear = false;
    private bool isOpen = false;
    private Quaternion closedRotation;
    private Quaternion openRotation;
    [SerializeField]AudioClip Doorcreak;
    void Start()
    {
        // Save the initial rotation of the door (closed state)
        closedRotation = doorTransform.rotation;
        // Calculate the target rotation for the open state
        openRotation = closedRotation * Quaternion.Euler(openAngleX, openAngleY, openAngleZ);
    }

    void Update()
    {
        if (isPlayerNear && Input.GetKeyDown(KeyCode.E))
        {
            // Toggle the door's state
            isOpen = !isOpen;
            GetComponent<AudioSource>().PlayOneShot(Doorcreak);
        }

        // Smoothly rotate the door to the target rotation
        doorTransform.rotation = Quaternion.Slerp(doorTransform.rotation, isOpen ? openRotation : closedRotation, Time.deltaTime * openSpeed);
    }

    void OnTriggerEnter(Collider other)
    {
        // Check if the player enters the trigger
        if (other.CompareTag("Player"))
        {
            isPlayerNear = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        // Check if the player exits the trigger
        if (other.CompareTag("Player"))
        {
            isPlayerNear = false;
        }
    }
}

