using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrontendUIManager : MonoBehaviour
{
    [SerializeField] private Animator IntroductionPanelAnimator;
    [SerializeField] private Animator OptionsPanelAnimator;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnOptionsButtonPressed()
    {
        StartCoroutine(AnimateOnePanelMoveAfterTheOther(IntroductionPanelAnimator, OptionsPanelAnimator));
    }

    public void OnOptionsPanelBackButtonPressed()
    {
        StartCoroutine(AnimateOnePanelMoveAfterTheOther(OptionsPanelAnimator, IntroductionPanelAnimator));
    }

    private IEnumerator AnimateOnePanelMoveAfterTheOther(Animator panelToMoveOut, Animator panelToMovIn)
    {
        panelToMoveOut.Play("Panel Move Out");

        yield return new WaitForSeconds(0.5f);
        
        panelToMovIn.Play("Panel Move In");
    }
}
