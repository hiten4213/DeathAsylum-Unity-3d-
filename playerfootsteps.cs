using UnityEngine;

public class PlayerFootsteps : MonoBehaviour
{
    public AudioSource footstepAudioSource; // Assign the AudioSource in the Inspector
    public AudioClip[] footstepClips; // Drag and drop footstep clips into this array
    public float walkStepInterval = 0.5f; // Time between steps when walking
    public float runStepInterval = 0.3f;  // Time between steps when running
    public float runSpeedMultiplier = 1.5f; // Speed multiplier when running
    [Range(0f, 1f)]
    public float footstepVolume = 1f; // Volume control for footsteps

    private CharacterController characterController;
    private float stepTimer = 0f;
    private bool isMoving = false;
    private bool isRunning = false;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        if (footstepAudioSource == null)
        {
            footstepAudioSource = GetComponent<AudioSource>();
        }
        footstepAudioSource.volume = footstepVolume; // Set initial volume
    }

    void Update()
    {
        // Check if the player is running
        isRunning = Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift);
        
        // Adjust movement speed if running
        float speedMultiplier = isRunning ? runSpeedMultiplier : 1f;

        // Check if the player is moving
        bool isPlayerMoving = characterController.isGrounded && characterController.velocity.magnitude > 0.2f * speedMultiplier;

        if (isPlayerMoving && !isMoving)
        {
            // Play a footstep sound immediately when the player starts moving
            PlayFootstep();
            isMoving = true;
            stepTimer = 0f; // Reset the step timer
        }

        if (isPlayerMoving)
        {
            stepTimer += Time.deltaTime;

            // Determine the current step interval based on whether the player is running
            float currentStepInterval = isRunning ? runStepInterval : walkStepInterval;

            // Play footstep sound after the step interval has passed
            if (stepTimer > currentStepInterval)
            {
                PlayFootstep();
                stepTimer = 0f;
            }
        }
        else
        {
            isMoving = false;
        }

        // Update the footstep volume in case it was adjusted in the Inspector
        footstepAudioSource.volume = footstepVolume;
    }

    void PlayFootstep()
    {
        if (footstepClips.Length > 0)
        {
            // Select a random footstep sound
            int index = Random.Range(0, footstepClips.Length);
            footstepAudioSource.clip = footstepClips[index];
            footstepAudioSource.Play();
        }
    }
}
