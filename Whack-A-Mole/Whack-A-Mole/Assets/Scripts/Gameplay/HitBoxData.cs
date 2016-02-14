using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.EventSystems;

public class HitBoxData : MonoBehaviour, IHitable, IPointerDownHandler {

    //Unity Lifecycle-----------------------------------------------------------------
    public void Awake()
    {
        //attach method to event listener
        // GetComponent<Button>().OnPointerDown.AddListener(() => ButtonPressed());

        LevelManager.OnInitialiseLevel += Initialise;
    }


    private void Initialise()
    {
        StopAllCoroutines();
        GetComponent<Image>().color = Color.white;
    }
    //=================================================================================

    public void Spawned(float t)
    {
        StartCoroutine(Duration(t));
    }

    private IEnumerator Duration(float t)
    {
        yield return new WaitForSeconds(t);

        if(gameObject.activeInHierarchy)
           GetComponent<Image>().color = Color.white;
    }


    public void OnPointerDown(PointerEventData eventData)
    {
        if (GetComponent<Image>().color == Color.green)
        {
            //ParticleEffector.InitEditSpeed(true);
            LevelManager.InitEditFuel(0.1f);
        }
        else
        {
            //ParticleEffector.InitEditSpeed(false);
            LevelManager.InitEditFuel(-0.1f);
        }

        StopAllCoroutines();
        GetComponent<Image>().color = Color.white;
    }
}
