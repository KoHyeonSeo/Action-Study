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

    //���������� ���� �ϴ� �����̵� 100���� ����
    [SerializeField] private GameObject loadingGameObject1;

    //�������� �����ϴ� �����̵� 0���� ����
    [SerializeField] private GameObject loadingGameObject2;

    private float curTime = 0f;
    private Slider loadingSlider1;
    private Slider loadingSlider2;
    private float textTime;
    private float timeSet = 0;
    private int index = 0;

    private void Start()
    {
        //���� �ʱ�ȭ
        loadingSlider1 = loadingGameObject1.GetComponent<Slider>();
        loadingSlider2 = loadingGameObject2.GetComponent<Slider>();
        loadingSlider1.value = 100;
        loadingGameObject2.SetActive(false);

        //text�� ������ ������ �ð����� ���� �� �ֵ��� ���
        textTime = loadingTime / loadingTexts.Count;
        
        //�ڷ�ƾ ����
        StartCoroutine("Loading");
    }
    private IEnumerator Loading()
    {
        while(curTime < loadingTime)
        {
            curTime += Time.deltaTime;

            //Text ����
            timeSet += Time.deltaTime;
            loadingText.text = loadingTexts[index];
            if (timeSet >= textTime)
            {
                loadingText.text = "";
                index++;
                timeSet = 0;
            }

            //�ε��� ����
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
