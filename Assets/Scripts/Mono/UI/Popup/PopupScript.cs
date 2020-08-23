using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopupScript : MonoBehaviour
{
    public Animator animator;
    Text damageText;

    void Start()
    {
        AnimatorClipInfo[] clipInfo = animator.GetCurrentAnimatorClipInfo(0);
        Destroy(this.gameObject, clipInfo[0].clip.length);
    }

    public void SetText(string text)
    {
        damageText = animator.gameObject.GetComponent<Text>();
        damageText.text = text;
    }
}
