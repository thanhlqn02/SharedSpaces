using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;
using System.Collections.Generic;


public class ExperienceManager : MonoBehaviour
{
    [SerializeField] private Button choiceButton;
    [SerializeField] private Button resetButton;
    [SerializeField] private Button agreeButton;
    [SerializeField] private TMP_Text contentTextNotice;
    [SerializeField] private TMP_Text titleText;
    [SerializeField] private TMP_Text contentText;
    [SerializeField] private Transform npcS;
    [SerializeField] private GameObject noticeNearFire;
    [SerializeField]  private GameObject uiComponent;
    [SerializeField] private Camera vrPlayerCamera;
    private IEnumerator coroutine;
    private IEnumerator coroutine1;
    private void Start()
    {
        Debug.Log("2222222222222222222222");
        Init();
    }
    private void Update()
    {
        SetRotationCamera();
    }
    private IEnumerator WaitAndPrint(float waitTime)
    {
        while (true)
        {
            yield return new WaitForSeconds(waitTime);
            if (Fire.Instance != null)
            {
                choiceButton.onClick.AddListener(SetTextStepOne);
                resetButton.onClick.AddListener(ResetScene);
                agreeButton.onClick.AddListener(DisableNoticeNearFire);
                Fire.Instance.ShowTextDone += SetTextDone;
                Fire.Instance.ShowNotice += SetTextNoticeNearFire;
                Fire.Instance.HideNotice += DisableNoticeNearFire;
                break;
            }               
        }
    }

    private IEnumerator FindCamera(float waitTime)
    {
        while (true)
        {
            yield return new WaitForSeconds(waitTime);
            print("Starting " + Time.time);
            if (GameObject.Find("VRPlayerCamera(Clone)") != null)
            {
                vrPlayerCamera = GameObject.Find("VRPlayerCamera(Clone)").GetComponent<Camera>();
                break;
            }
        }
    }


    private void OnEnable()
    {
        coroutine = WaitAndPrint(0.5f);
        StartCoroutine(coroutine);
    }
    private void OnDisable()
    {
        choiceButton.onClick.RemoveAllListeners();
        resetButton.onClick.RemoveAllListeners();
        agreeButton.onClick.RemoveAllListeners();
        Fire.Instance.ShowTextDone -= SetTextDone;
        Fire.Instance.ShowNotice -= SetTextNoticeNearFire;
        Fire.Instance.HideNotice -= DisableNoticeNearFire;
    }
    private void Init()
    {
        Debug.Log("3333333333333333333333");
        coroutine1 = FindCamera(0.5f);
        StartCoroutine(coroutine1);
        uiComponent = GameObject.Find(ExperienceConfig.nameUIComponent);
        SetTextWelcome();
        resetButton.interactable = false;
        noticeNearFire.gameObject.SetActive(false);
    }
    private void SetRotationCamera()
    {
        if (uiComponent != null && vrPlayerCamera != null)
        {
            uiComponent.transform.rotation = vrPlayerCamera.transform.rotation;
            noticeNearFire.transform.rotation = vrPlayerCamera.transform.rotation;
        }
            
    }
    private void SetTextWelcome()
    {
        titleText.text = ExperienceConfig.textWelcome;
        contentText.text = ExperienceConfig.textContentWelcome;
    }
    private void SetTextStepOne()
    {
        titleText.text = ExperienceConfig.textStepOne;
        contentText.text = ExperienceConfig.textContentStepOne;
        choiceButton.onClick.RemoveAllListeners();
        choiceButton.onClick.AddListener(SetTextStepTwo);
    }
    private void SetTextStepTwo()
    {
        titleText.text = ExperienceConfig.textStepTwo;
        contentText.text = ExperienceConfig.textContentStepTwo;
        choiceButton.onClick.RemoveAllListeners();
        choiceButton.interactable = false;
    }
    private void SetTextDone()
    {
        titleText.text = ExperienceConfig.textDone;
        contentText.text = ExperienceConfig.textContentDone;
        resetButton.interactable = true;
        SetAnimationNPCS();
    }
    private void SetTextNoticeNearFire()
    {
        noticeNearFire.gameObject.SetActive(true);
        contentTextNotice.text = ExperienceConfig.textNoticeNearFire;
    }
    private void SetTextNoticeReleasePin()
    {
        noticeNearFire.gameObject.SetActive(true);
        contentTextNotice.text = ExperienceConfig.textNoticeDontReleasePin;
    }
    private void DisableNoticeNearFire()
    {
        noticeNearFire.gameObject.SetActive(false);
    }
    private void ResetScene()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(ExperienceConfig.nameSceneExperience);
    }
    private void SetAnimationNPCS()
    {
        foreach(var animator in npcS.GetComponentsInChildren<Animator>())
        {
            animator.SetBool("IsLit", false);
        }
    }
}
