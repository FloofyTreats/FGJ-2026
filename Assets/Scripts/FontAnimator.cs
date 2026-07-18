using TMPro;
using UnityEngine;
using System.Collections;

public class FontAnimator : MonoBehaviour
{
    public TextMeshProUGUI textObject;
    public TMP_FontAsset[] fonts;
    public float speed = 0.02f;

    private bool IsDone;
    private int currIndex;
    Coroutine m_CorotineAnim = null;
    // Update is called once per frame
    void Start()
    {
        Func_PlayFontAnim();
    }

    void OnEnable()
    {
        Func_PlayFontAnim();
    }

    private void Update()
    {
        if(textObject.text == "" && !IsDone)
        {
            IsDone = true;
            m_CorotineAnim = null;
            StopAllCoroutines();
        }
        else if (textObject.text != "" && IsDone)
        {
            if (m_CorotineAnim == null)
            {
                Func_PlayFontAnim();
            }
        }
    }

    public void Func_PlayFontAnim()
    {
        IsDone = false;
        m_CorotineAnim = StartCoroutine(Func_SwitchFonts());
    }

    IEnumerator Func_SwitchFonts()
    {
        yield return new WaitForSeconds(speed);
        if (currIndex >= fonts.Length)
        {
            currIndex = 0;
        }
        textObject.font = fonts[currIndex];
        currIndex += 1;
        if (IsDone == false)
            m_CorotineAnim = StartCoroutine(Func_SwitchFonts());
    }
}
