using UnityEngine;
using UnityEngine.SceneManagement;
public class Rocket : MonoBehaviour {
	
    [SerializeField] float rcsT = 100f;
    [SerializeField] float mainT = 100f;
    [SerializeField] float levelLoadDelay = 1f;
    [SerializeField] AudioClip mainEngine;
    [SerializeField] AudioClip death;
    [SerializeField] AudioClip success;
    [SerializeField] ParticleSystem mainEngineParticles;
    [SerializeField] ParticleSystem deathParticles;
    [SerializeField] ParticleSystem successParticles;
	Rigidbody rigidBody;
	AudioSource audioSource;
    enum State {Alive, Dead, Transcend};
    State state = State.Alive;
	// Use this for initialization
	void Start () {
		rigidBody = GetComponent<Rigidbody>();
		audioSource = GetComponent <AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
       
        if (state==State.Alive)  
		{RespondToThrustInput();
		RespondToRotateInput();}
	}
    void OnCollisionEnter(Collision collision)
    {
        if (state !=State.Alive) {return;} // ignore collisions when dead

        switch (collision.gameObject.tag)
        {
            case "Friendly":
                //do nothing
                break;

            case "Finish":
                StartSuccessSequence();
                break;
            default:
            case "Dead":
                StartDeathSequence();
                break;
        }

    }

    private void StartSuccessSequence()
    {
        state = State.Transcend;
        audioSource.Stop();
        audioSource.PlayOneShot(success);
        successParticles.Play();
        Invoke("LoadNextLevel", levelLoadDelay); 
    }
      private void StartDeathSequence()
    {
        state = State.Dead;
        audioSource.Stop();
        audioSource.PlayOneShot(death);
        deathParticles.Play();
        Invoke("LoadFirstLevel", levelLoadDelay);
    }

    private void LoadFirstLevel()
    {SceneManager.LoadScene(0);
    } 
    private void LoadNextLevel()
    {SceneManager.LoadScene(1);// todo: allow for more than 2 levels
    } 
private void RespondToThrustInput()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            ApplyThrust();
        }
        else
        { audioSource.Stop();
        mainEngineParticles.Stop(); }
        

	}

    private void ApplyThrust()
    {
        rigidBody.AddRelativeForce(Vector3.up * mainT * Time.deltaTime);
        if (!audioSource.isPlaying) //so it doesn't layer
        { audioSource.PlayOneShot(mainEngine);} 
        mainEngineParticles.Play();
    }

    private void RespondToRotateInput()   //can T while rotating
  
    {
        rigidBody.freezeRotation = true;//take manual control of the rotation
        float rotationThisFrame = rcsT * Time.deltaTime;
        if (Input.GetKey(KeyCode.A)) {transform.Rotate(Vector3.forward * rotationThisFrame);}

        else if (Input.GetKey(KeyCode.D)) { transform.Rotate(-Vector3.forward* rotationThisFrame);}
        
       rigidBody.freezeRotation = false; // resume physics control of rotation  
    }
    
}
