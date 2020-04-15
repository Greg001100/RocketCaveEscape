using UnityEngine;
using UnityEngine.SceneManagement;

public class rocketflight : MonoBehaviour
{
    Rigidbody rigidbody;
    AudioSource audioSource;
    [SerializeField] float rcsThrust = 100f;
    [SerializeField] float mThrust = 100f;
    [SerializeField] AudioClip mainEngine;
    [SerializeField] AudioClip lvlComplete;
    [SerializeField] AudioClip death;
    [SerializeField] ParticleSystem explosion;
    [SerializeField] ParticleSystem jetfire;
    [SerializeField] ParticleSystem success;

    enum State { alive, dying, transcending }
    State state = State.alive;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (state == State.alive)
        {
            Rotate();
            Thrust();
        }
    }   

    private void Rotate()
    {
        rigidbody.freezeRotation = true;     
        
        float rotationPerFrame = rcsThrust * Time.deltaTime;
        

        if (Input.GetKey(KeyCode.A))
        {            
            transform.Rotate(Vector3.forward * rotationPerFrame);
        }

        else if (Input.GetKey(KeyCode.D))
        {            
            transform.Rotate(-Vector3.forward * rotationPerFrame);
        }

        rigidbody.freezeRotation = false;
    }

    private void Thrust()
    {
        float thrustPerFrame = mThrust * Time.deltaTime;

        if (Input.GetKey(KeyCode.Space))
        {
            rigidbody.AddRelativeForce(Vector3.up * thrustPerFrame);
            if (audioSource.isPlaying == false)
            {
                audioSource.PlayOneShot(mainEngine);
                jetfire.Play();
            }
        }

        else if (Input.GetKeyUp(KeyCode.Space))
        {
            audioSource.Stop();
            jetfire.Stop();
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (state != State.alive)
        {
            return;
        }
        switch (collision.gameObject.tag)
        {
            case "Friendly":
                print("safe");
                break;

            case "Finish":
                state = State.transcending;
                success.Play();
                audioSource.PlayOneShot(lvlComplete);               
                Invoke("LoadNextLevel", 1f);
                break;

            default:
                state = State.dying;
                explosion.Play();
                audioSource.PlayOneShot(death);
                Invoke("Death", 1f);
                break;
        }
    }

    private void Death()
    {
        SceneManager.LoadScene(0);
    }

    private void LoadNextLevel()
    {
        SceneManager.LoadScene(1);
    }
}
