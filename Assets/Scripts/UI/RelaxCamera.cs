using UnityEngine;

[RequireComponent(typeof(Camera))]
public class RelaxCamera : MonoBehaviour
{
    #region Settings

    [SerializeField]
    public Transform focus = default;

    [SerializeField, Range(1f, 100f)]
    public float distance = 5f;

    [SerializeField, Min(0f)]
    public float focusRadius = 5f;
    #endregion


    private readonly Vector3 lookDirection = new Vector3(0, 0, 1);
    internal Vector3 focusPoint;


    private void LateUpdate()
    {
        if (focus == default) return;

        UpdateFocusPoint();

        transform.localPosition = focusPoint - lookDirection * distance;
    }

    private void UpdateFocusPoint()
    {
        Vector3 targetPoint = focus.position;

        float dist = Vector3.Distance(targetPoint, focusPoint);

        if (dist > focusRadius)
        {
            focusPoint = Vector3.Lerp(targetPoint, focusPoint, focusRadius / dist);
        }
    }
}