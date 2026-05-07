using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContainerCounterVisual : MonoBehaviour
{
    private const string OPEN_CLOSE = "OpenClose";

    [SerializeField] private ContainerCounter containerCounter;

    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();

        if (animator == null)
            Debug.LogError("❌ Animator NOT FOUND di " + gameObject.name);
        else
            Debug.Log("✅ Animator ditemukan");

        if (containerCounter == null)
        {
            containerCounter = GetComponentInParent<ContainerCounter>();
            Debug.Log("🔍 Mencoba GetComponentInParent...");
        }

        if (containerCounter == null)
            Debug.LogError("❌ ContainerCounter STILL NULL - drag manual di Inspector!");
        else
            Debug.Log("✅ ContainerCounter ditemukan: " + containerCounter.gameObject.name);
    }

    private void Start()
    {
        containerCounter.OnPlayerGrabbedObject += ContainerCounter_OnPlayerGrabbedObject;
        Debug.Log("✅ Event subscribed");
    }

    private void ContainerCounter_OnPlayerGrabbedObject(object sender, System.EventArgs e)
    {
        Debug.Log("🎯 Event fired! Trigger: " + OPEN_CLOSE);
        animator.SetTrigger(OPEN_CLOSE);
    }
}