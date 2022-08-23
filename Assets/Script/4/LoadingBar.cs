using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadingBar : MonoBehaviour
{
    [SerializeField] private float loadingTime = 5f;
    [SerializeField] private float speed = 5f;
    [SerializeField] private Text loadingText;

    [SerializeField] private List<string> loadingTexts = new List<string>();

    //오른쪽으로 감소 하는 슬라이드 100부터 시작
    [SerializeField] private GameObject loadingGameObject1;

    //왼쪽으로 감소하는 슬라이드 0부터 시작
    [SerializeField] private GameObject loadingGameObject2;

    private float curTime = 0f;
    private Slider loadingSlider1;
    private Slider loadingSlider2;
    private float textTime;
    private float timeSet = 0;
    private int index = 0;

    private void Start()
    {
        //시작 초기화
        loadingSlider1 = loadingGameObject1.GetComponent<Slider>();
        loadingSlider2 = loadingGameObject2.GetComponent<Slider>();
        loadingSlider1.value = 100;
        loadingGameObject2.SetActive(false);

        //text의 갯수를 일정한 시간마다 나올 수 있도록 계산
        textTime = loadingTime / loadingTexts.Count;
        
        //코루틴 시작
        StartCoroutine("Loading");
    }
    private IEnumerator Loading()
    {
        while(curTime < loadingTime)
        {
            curTime += Time.deltaTime;

            //Text 관련
            timeSet += Time.deltaTime;
            loadingText.text = loadingTexts[index];
            if (timeSet >= textTime)
            {
                loadingText.text = "";
                index++;
                timeSet = 0;
            }

            //로딩바 관련
            if (loadingGameObject1.activeSelf)
            {
                loadingSlider1.value -= speed * Time.deltaTime;
                if(loadingSlider1.value <= 0)
                {
                    loadingGameObject2.SetActive(true);
                    loadingSlider2.value = 0;
                    loadingGameObject1.SetActive(false);
                }
            }
            else if(loadingGameObject2.activeSelf)
            {
                loadingSlider2.value += speed * Time.deltaTime;
                if (loadingSlider2.value >= 100)
                {
                    loadingGameObject1.SetActive(true);
                    loadingSlider1.value = 100;
                    loadingGameObject2.SetActive(false);
                }
            }

            yield return null;  
        }
    }

}
