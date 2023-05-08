using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIHandler : MonoBehaviour
{

    [SerializeField] TMP_Text speedText;
    [SerializeField] Rigidbody rb;

    void LateUpdate()
    {
        speedText.text = Vector3.Magnitude(rb.velocity).ToString();
    }
}
