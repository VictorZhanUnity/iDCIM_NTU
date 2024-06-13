using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class LineIndicator : MonoBehaviour
{
    [Header(">>>Line高度")]
    [SerializeField] private float lineHeight;
    [SerializeField] private float startPosY;

    [Header(">>>UI組件")]
    [SerializeField] private Canvas  canvas;
    [SerializeField] private LineRenderer lineRenderer;

    private void Start()
    {
        
    }


    private void OnValidate()
    {
        lineRenderer ??= GetComponent<LineRenderer>();
        canvas ??= transform.GetChild(0).GetComponent<Canvas>();

        Vector3 pos = canvas.transform.localPosition;
        pos.y = lineHeight;
        canvas.transform.localPosition = pos; 
        lineRenderer.SetPosition(0, new Vector3(0, startPosY, 0));
        lineRenderer.SetPosition(1, canvas.transform.localPosition);
    }
 }
