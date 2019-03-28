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

    private float m_XMaxAngle = 25f;
    private float m_YMaxAngle = 25f;

    private float m_XCurrentAngle = 0f;
    private float m_YCurrentAngle = 0f;

    // Start is called before the first frame update
    private void Awake()
    {
#if UNITY_EDITOR

#elif UNITY_ANDROID
        GyroEnable(true);
#endif
    }
    void Start()
    {
        GetComponents();
    }
    private void GyroEnable(bool ok)
    {
        Input.gyro.enabled = ok;
    }
    private void GetComponents()
    {
        m_Image = this.gameObject.GetComponent<Image>();
        m_RT = this.gameObject.GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
#if UNITY_EDITOR
        CheckInside();
#elif UNITY_ANDROID
        GyroUpdate();
#endif

        transform.rotation = Quaternion.Euler(m_TargetAngle);
    }


    #region EditorInput
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
    #endregion

    #region Gyro
    private void GyroUpdate()
    {
        GetRotation();
    }

    private void GetRotation()
    {
        Rotation_X();
        Rotation_Y();
        m_TargetAngle = new Vector3(
            m_XCurrentAngle,
            m_YCurrentAngle,
            0
            );
    }
    private void Rotation_X()
    {
        m_XCurrentAngle += Input.gyro.rotationRateUnbiased.x;

        m_XCurrentAngle = Mathf.Clamp(m_XCurrentAngle, -m_XMaxAngle, m_XMaxAngle);
    }

    private void Rotation_Y()
    {
        m_YCurrentAngle += Input.gyro.rotationRateUnbiased.y;

        m_YCurrentAngle = Mathf.Clamp(m_YCurrentAngle, -m_YMaxAngle, m_YMaxAngle);
    }
    #endregion
}
