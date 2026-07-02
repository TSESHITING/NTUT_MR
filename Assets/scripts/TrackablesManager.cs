
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using Meta.XR.MRUtilityKit;
using static OVRAnchor;
using System.Collections;
using System.Collections.Generic;

public class TrackablesManager : MonoBehaviour
{
    public enum Language
    {
        Chinese,
        English
    }

    [System.Serializable]
    public class QRPrefabPair
    {
        public string qrID;
        public GameObject chinesePrefab;
        public GameObject englishPrefab;
        public int dotIndex;
    }

    [Header("QR Prefabs")]
    [SerializeField] private List<QRPrefabPair> qrPrefabPairs;

    [Header("Language UI")]
    [SerializeField] private Button buttonCH;
    [SerializeField] private Button buttonENG;
    [SerializeField] private GameObject languageCanvas;
    [SerializeField] private GameObject mainCanvas;

    [Header("Guide Canvas")]
    [SerializeField] private GameObject guideCanvas;
    [SerializeField] private GameObject guideCanvasCH;
    [SerializeField] private GameObject guideCanvasEN;
    [SerializeField] private Button nextButton;
    [SerializeField] private TMP_Text nextButtonText;

    [Header("Scanning Indicator")]
    [SerializeField] private GameObject scanningIndicator;

    [Header("Arrow Hint")]
    [SerializeField] private GameObject arrowHint;
    [SerializeField] private GameObject arrowHintCH;
    [SerializeField] private GameObject arrowHintEN;
    [SerializeField] private float prefabViewDuration = 30f;

    [Header("Progress Dots")]
    [SerializeField] private Transform dotsParent;
    [SerializeField] private Color dotInactiveColor = Color.gray;
    [SerializeField] private Color dotActiveColor = Color.green;

    [Header("Complete Button")]
    [SerializeField] private Button completeButton;
    [SerializeField] private TMP_Text completeButtonText;

    [Header("Helmet Canvas")]
    [SerializeField] private GameObject helmetCanvas;
    [SerializeField] private GameObject helmetCanvasCH;
    [SerializeField] private GameObject helmetCanvasEN;

    private GameObject currentActiveObject;

    private Language currentLanguage = Language.Chinese;

    private bool languageSelected = false;
    private bool scanningStarted = false;
    private bool scanningComplete = false;
    private bool inputEnabled = false;

    private Coroutine arrowTimerCoroutine;
    private Coroutine nextButtonCoroutine;
    private Coroutine completeButtonCoroutine;
    private Coroutine resetCoroutine;

    private string currentQRID = "";

    private MRUKTrackable lastSeenTrackable = null;

    private HashSet<string> scannedQRIDs = new HashSet<string>();

    private Image[] dots;

    private const int totalQR = 10;

    private void Start()
    {
        languageCanvas.SetActive(true);

        mainCanvas.SetActive(false);

        guideCanvas.SetActive(false);

        helmetCanvas.SetActive(false);

        if (arrowHint != null)
            arrowHint.SetActive(false);

        if (scanningIndicator != null)
            scanningIndicator.SetActive(false);

        if (completeButton != null)
            completeButton.gameObject.SetActive(false);

        if (nextButton != null)
            nextButton.gameObject.SetActive(false);

        dots = dotsParent.GetComponentsInChildren<Image>();

        foreach (Image dot in dots)
        {
            dot.color = dotInactiveColor;
        }

        Debug.Log("TrackablesManager Start");
    }

    public void OnMRUKInitialized()
    {
        Debug.Log("MRUK 初始化完成");

        languageCanvas.SetActive(true);
    }

    private void Update()
    {
        // Debug 狀態
        Debug.Log("Input Enabled : " + inputEnabled);

        if (!inputEnabled)
            return;

        // 左控制器 X 鍵
        if (OVRInput.GetDown(OVRInput.RawButton.X) && scanningStarted)
        {
            Debug.Log("X Button Pressed");

            TriggerHelmetFlow();
        }

        // 左控制器 Y 鍵
        if (OVRInput.GetDown(OVRInput.RawButton.Y))
        {
            Debug.Log("Y Button Pressed");

            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    public void SetLanguageChinese()
    {
        currentLanguage = Language.Chinese;

        languageCanvas.SetActive(false);

        ShowGuide();
    }

    public void SetLanguageEnglish()
    {
        currentLanguage = Language.English;

        languageCanvas.SetActive(false);

        ShowGuide();
    }

    private void ShowGuide()
    {
        guideCanvas.SetActive(true);

        if (currentLanguage == Language.Chinese)
        {
            guideCanvasCH.SetActive(true);
            guideCanvasEN.SetActive(false);

            nextButtonText.text = "下一步";
        }
        else
        {
            guideCanvasCH.SetActive(false);
            guideCanvasEN.SetActive(true);

            nextButtonText.text = "Next";
        }

        if (nextButtonCoroutine != null)
            StopCoroutine(nextButtonCoroutine);

        nextButtonCoroutine = StartCoroutine(ShowNextButtonAfterDelay());
    }

    private IEnumerator ShowNextButtonAfterDelay()
    {
        yield return new WaitForSeconds(10f);

        if (nextButton != null)
            nextButton.gameObject.SetActive(true);
    }

    public void OnNextButtonPressed()
    {
        Debug.Log("Next Button Pressed");

        guideCanvas.SetActive(false);

        nextButton.gameObject.SetActive(false);

        mainCanvas.SetActive(true);

        StartCoroutine(EnableScanningAfterDelay());
    }

    private IEnumerator EnableScanningAfterDelay()
    {
        yield return new WaitForSeconds(1f);

        languageSelected = true;

        scanningStarted = true;

        inputEnabled = true;

        Debug.Log("掃描已啟用");

        if (lastSeenTrackable != null)
        {
            currentQRID = "";

            HandleTrackable(lastSeenTrackable);
        }
    }

    private void ShowArrow()
    {
        if (arrowHint == null)
            return;

        arrowHint.SetActive(true);

        if (currentLanguage == Language.Chinese)
        {
            arrowHintCH.SetActive(true);
            arrowHintEN.SetActive(false);
        }
        else
        {
            arrowHintCH.SetActive(false);
            arrowHintEN.SetActive(true);
        }
    }

    private void HideArrow()
    {
        if (arrowHint == null)
            return;

        arrowHint.SetActive(false);
    }

    private void StartArrowTimer()
    {
        if (arrowTimerCoroutine != null)
            StopCoroutine(arrowTimerCoroutine);

        arrowTimerCoroutine = StartCoroutine(ShowArrowAfterPrefabView());
    }

    private void StopArrowTimer()
    {
        if (arrowTimerCoroutine != null)
        {
            StopCoroutine(arrowTimerCoroutine);

            arrowTimerCoroutine = null;
        }
    }

    private IEnumerator ShowArrowAfterPrefabView()
    {
        yield return new WaitForSeconds(prefabViewDuration);

        ShowArrow();
    }

    public void OnTrackableAdded(MRUKTrackable trackable)
    {
        if (trackable.TrackableType != TrackableType.QRCode)
            return;

        lastSeenTrackable = trackable;

        currentQRID = "";

        HandleTrackable(trackable);
    }

    public void OnTrackableRemoved(MRUKTrackable trackable)
    {
        if (trackable.TrackableType != TrackableType.QRCode)
            return;

        if (currentActiveObject != null)
        {
            Destroy(currentActiveObject);

            currentActiveObject = null;
        }

        HideArrow();

        StopArrowTimer();

        lastSeenTrackable = null;

        currentQRID = "";

        Debug.Log("QR Code 離開視野");
    }

    private void HandleTrackable(MRUKTrackable trackable)
    {
        if (trackable.TrackableType != TrackableType.QRCode)
            return;

        if (!languageSelected)
            return;

        if (scanningComplete)
            return;

        string qrID = trackable.MarkerPayloadString;

        if (qrID == currentQRID)
            return;

        currentQRID = qrID;

        Debug.Log("QR Code detected : " + qrID);

        HideArrow();

        StopArrowTimer();

        QRPrefabPair pair = qrPrefabPairs.Find(p => p.qrID == qrID);

        if (pair == null)
        {
            Debug.Log("Unknown QR : " + qrID);

            return;
        }

        if (currentActiveObject != null)
        {
            Destroy(currentActiveObject);
        }

        GameObject prefabToSpawn =
            currentLanguage == Language.Chinese
            ? pair.chinesePrefab
            : pair.englishPrefab;

        GameObject go = Instantiate(prefabToSpawn, trackable.transform);

        go.transform.localPosition = Vector3.zero;

        go.transform.localRotation = Quaternion.Euler(90, 0, 0);

        UnityEngine.Video.VideoPlayer vp =
            go.GetComponent<UnityEngine.Video.VideoPlayer>();

        if (vp != null)
        {
            vp.Play();
        }

        currentActiveObject = go;

        StartArrowTimer();

        if (!scannedQRIDs.Contains(qrID))
        {
            scannedQRIDs.Add(qrID);

            UpdateDots(pair.dotIndex);

            CheckAllComplete();
        }
    }

    private void UpdateDots(int dotIndex)
    {
        if (dotIndex >= 0 && dotIndex < dots.Length)
        {
            dots[dotIndex].color = dotActiveColor;
        }
    }

    private void CheckAllComplete()
    {
        if (scannedQRIDs.Count >= totalQR)
        {
            scanningComplete = true;

            if (completeButtonCoroutine != null)
                StopCoroutine(completeButtonCoroutine);

            completeButtonCoroutine =
                StartCoroutine(ShowCompleteButtonAfterDelay());
        }
    }

    private IEnumerator ShowCompleteButtonAfterDelay()
    {
        yield return new WaitForSeconds(3f);

        if (completeButton != null)
        {
            completeButton.gameObject.SetActive(true);

            completeButtonText.text =
                currentLanguage == Language.Chinese
                ? "完成"
                : "Finish";
        }
    }

    public void OnCompleteButtonPressed()
    {
        TriggerHelmetFlow();
    }

    private void TriggerHelmetFlow()
    {
        Debug.Log("Trigger Helmet Flow");

        if (resetCoroutine != null)
            StopCoroutine(resetCoroutine);

        languageSelected = false;

        scanningStarted = false;

        inputEnabled = false;

        mainCanvas.SetActive(false);

        guideCanvas.SetActive(false);

        HideArrow();

        StopArrowTimer();

        if (scanningIndicator != null)
            scanningIndicator.SetActive(false);

        if (currentActiveObject != null)
        {
            Destroy(currentActiveObject);

            currentActiveObject = null;
        }

        helmetCanvas.SetActive(true);

        if (currentLanguage == Language.Chinese)
        {
            helmetCanvasCH.SetActive(true);
            helmetCanvasEN.SetActive(false);
        }
        else
        {
            helmetCanvasCH.SetActive(false);
            helmetCanvasEN.SetActive(true);
        }

        resetCoroutine = StartCoroutine(AutoResetAfterDelay());
    }

    private IEnumerator AutoResetAfterDelay()
    {
        yield return new WaitForSeconds(10f);

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}

