using UnityEngine;
using UnityEngine.Events;
using Unity.Netcode;

public class Fire : NetworkBehaviour
{
    #region Singleton pattern
    public static Fire instance;
    public static Fire Instance
    {
        get
        {
            if(instance == null)
            {
                instance = FindObjectOfType<Fire>();
            }
            return instance;
        }
    }
    #endregion Singleton pattern
    public UnityAction ShowTextDone;
    public UnityAction ShowNotice;
    public UnityAction HideNotice;
    [SerializeField] private ParticleSystem[] fireParticleSystem = new ParticleSystem[0];
    [SerializeField] private VRPlayerController vrPlayerController;
    private AudioSource campFireAudioSources;
    private float currentIntensity = 1f;
    private float[] startIntensities = new float[0];
    private float timeLastWatered = 0;
    private bool isLit = true;
    private void Start()
    {
        Init();
    }
    private void Update()
    {
        RegenFire();
    }
    private void Init()
    {
        SetArrayParticleSystem();
        campFireAudioSources = GetComponent<AudioSource>();
        campFireAudioSources.Play();
    }
    private void SetArrayParticleSystem()
    {
        startIntensities = new float[fireParticleSystem.Length];
        for (int i = 0; i < fireParticleSystem.Length; i++)
        {
            startIntensities[i] = fireParticleSystem[i].emission.rateOverTime.constant;
        }
    }
    private void ChangeIntensity()
    {
        for (int i = 0; i < fireParticleSystem.Length; i++)
        {
            var emission = fireParticleSystem[i].emission;
            emission.rateOverTime = currentIntensity * startIntensities[i];
        }
    }
    public void TryExtinguishLocal(float amount)
    {
        timeLastWatered = Time.time;
        if (IsHost)
        {
            amount -= 0.4f * amount;
            Debug.Log("coan");
        }
        currentIntensity -= amount;
        ChangeIntensity();
        if (currentIntensity <= 0)
        {
            isLit = false;
            campFireAudioSources.Stop();
            ShowTextDone();
        }
    }

    public void TryExtinguish(float amount)
    {
        TryExtinguishLocal(amount);
        if (IsHost)
        {
            TryExtinguishClientRPC(amount);
        }
        else
            TryExtinguishServerRPC(amount);
    }

    [ServerRpc(RequireOwnership = false)]
    public void TryExtinguishServerRPC(float amount)
    {
        TryExtinguishLocal(amount);
    }
    [ClientRpc]
    public void TryExtinguishClientRPC(float amount)
    {
        TryExtinguishLocal(amount);
    }

    private void RegenFire()
    {
        if (isLit && currentIntensity < 1.0f && Time.time - timeLastWatered >= ExperienceConfig.fireRegenDelay)
        {
            currentIntensity += ExperienceConfig.fireRegenRate * Time.deltaTime;
            ChangeIntensity();
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.GetComponentInParent<VRPlayerController>())
        {
            vrPlayerController = other.gameObject.GetComponentInParent<VRPlayerController>();
            Debug.Log("co va cham");
            ShowNotice();
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.GetComponentInParent<VRPlayerController>())
        {
            vrPlayerController = null;
            Debug.Log("het va cham");
            HideNotice();
        }
    }
}
