using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RotationText : MonoBehaviour
{
    public GameObject m_Target;
    private Text m_Text;

    private void Awake()
    {
        m_Text = this.gameObject.GetComponent<Text>();
    }
    // Start is called before the first frame update
    void Start()
    {
        

    }


    // Update is called once per frame
    void Update()
    {
        PrintRotation();
    }

    private void PrintRotation()
    {
        string print = "x : " + m_Target.transform.rotation.eulerAngles.x + " , y :" + m_Target.transform.rotation.eulerAngles.y;

        m_Text.text = print;
    }
        
}
