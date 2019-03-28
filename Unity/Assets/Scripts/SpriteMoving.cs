using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class SpriteMoving : MonoBehaviour
{
    private Image m_Image;
    private RectTransform m_RT;

    private Vector3 m_TargetAngle;

    private float m_DeltaHalfSize_X;
    private float m_DeltaHalfSize_Y;

    private float m_XMaxAngle = 15f;
    private float m_YMaxAngle = 15f;

    // Start is called before the first frame update
    void Start()
    {
        GetComponents();
    }

    private void GetComponents()
    {
        m_Image = this.gameObject.GetComponent<Image>();
        m_RT = this.gameObject.GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckInside();
    }

    private void CheckInside()
    {
        Vector2 diff = (Vector2)transform.position - (Vector2)Input.mousePosition;

        if(CheckDiff_X(diff.x) && CheckDiff_Y(diff.y))
        {

            float xrotation = m_XMaxAngle * (-Mathf.Clamp(diff.y / m_DeltaHalfSize_Y, -1, 1));
            float yrotation = m_YMaxAngle * Mathf.Clamp(diff.x / m_DeltaHalfSize_X, -1, 1);

            m_TargetAngle = new Vector3(
                xrotation,
                yrotation,
                0
            );

            transform.rotation = Quaternion.Euler(m_TargetAngle);
        }
    }

    private bool CheckDiff_X(float x)
    {
        m_DeltaHalfSize_X = (m_RT.sizeDelta.x / 2f);
        return (Mathf.Abs(x) <= m_DeltaHalfSize_X);
    }

    private bool CheckDiff_Y(float y)
    {
        m_DeltaHalfSize_Y = (m_RT.sizeDelta.y / 2f);
        return (Mathf.Abs(y) <= m_DeltaHalfSize_Y);
    }
}
