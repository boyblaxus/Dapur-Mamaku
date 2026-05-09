using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuttingCounterVisual : MonoBehaviour
{
    private const string CUT = "Cut";

    [SerializeField] private CuttingCounter cuttingCounter;

    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();

        if (animator == null)
            Debug.LogError("❌ Animator NOT FOUND di " + gameObject.name);
        else
            Debug.Log("✅ Animator ditemukan");

        if (cuttingCounter == null)
        {
            cuttingCounter = GetComponentInParent<CuttingCounter>();
            Debug.Log("🔍 Mencoba GetComponentInParent...");
        }

        if (cuttingCounter == null)
            Debug.LogError("❌ CuttingCounter STILL NULL - drag manual di Inspector!");
        else
            Debug.Log("✅ CuttingCounter ditemukan: " + cuttingCounter.gameObject.name);
    }

    private void Start()
    {
        cuttingCounter.OnCut += CuttingCounter_OnCut;
        Debug.Log("✅ Event subscribed");
    }

    private void CuttingCounter_OnCut(object sender, System.EventArgs e)
    {
        animator.SetTrigger(CUT);
    }
}