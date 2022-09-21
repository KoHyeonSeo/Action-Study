using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chapter02 : MonoBehaviour
{

    public float rotateSpeed = 1f;
    public float scrollSpeed = 200f;

    public Transform pivot;

    [System.Serializable]
    public class SphericalCoordinates
    {
        //���� ��Ƽ�� ���� ��� ����
        public float _radius, _azimuth, _elevation;

        /// <summary>
        /// radius(������) ������Ƽ / Clamp�� MinRadius, MaxRadius�� ���ѵȴ�.
        /// </summary>
        public float radius
        {
            get { return _radius; }
            private set
            {
                _radius = Mathf.Clamp(value, _minRadius, _maxRadius);
            }
        }
        /// <summary>
        /// azimuth(������) ������Ƽ / Repeat�� _maxAzimuth ~ _minAzimuth ���� ����
        /// </summary>
        public float azimuth
        {
            get { return _azimuth; }
            private set
            {
                _azimuth = Mathf.Repeat(value, _maxAzimuth - _minAzimuth);
            }
        }
        /// <summary>
        /// elevation(�Ӱ�) ������Ƽ / Clamp�� MinRadius, MaxRadius�� ���ѵȴ�.
        /// </summary>
        public float elevation
        {
            get { return _elevation; }
            private set
            {
                _elevation = Mathf.Clamp(value, _minElevation, _maxElevation);
            }
        }

        //=====================================
        //������ ���� ����

        public float _minRadius = 3f;
        public float _maxRadius = 20f;

        //=====================================
        //������ ���� ����

        //������ Degree min ����
        public float minAzimuth = 0f;
        //������ Raian min ����
        private float _minAzimuth;

        //������ Degree max ����
        public float maxAzimuth = 360f;
        //������ Radian max ����
        private float _maxAzimuth;

        //=====================================
        //�Ӱ� ���� ����

        //�Ӱ� Degree min ����
        public float minElevation = 0f;
        //�Ӱ� Radian min ����
        private float _minElevation;

        //�Ӱ� Degree max ����
        public float maxElevation = 90f;
        //�Ӱ� Radian max ����
        private float _maxElevation;

        //=====================================

        //�⺻ ������
        public SphericalCoordinates() { }
        public SphericalCoordinates(Vector3 cartesianCoordinate)
        {
            //�������� Degree���� Radian���� �ٲ�
            _minAzimuth = Mathf.Deg2Rad * minAzimuth;
            _maxAzimuth = Mathf.Deg2Rad * maxAzimuth;


            //�Ӱ��� Degree���� Radian���� �ٲ�
            _minElevation = Mathf.Deg2Rad * minElevation;
            _maxElevation = Mathf.Deg2Rad * maxElevation;

            //�������κ����� �Ÿ� -> ������
            radius = cartesianCoordinate.magnitude;

            //������ ���ϱ�
            azimuth = Mathf.Atan2(cartesianCoordinate.z, cartesianCoordinate.x);
            //�Ӱ� ���ϱ�
            elevation = Mathf.Asin(cartesianCoordinate.y / radius);
        }
        //������ǥ���� ������ǥ�� ��ȯ
        public Vector3 toCartesian
        {
            get
            {
                float t = radius * Mathf.Cos(elevation);

                return new Vector3(t * Mathf.Cos(azimuth), radius * Mathf.Sin(elevation), t * Mathf.Sin(azimuth));
            }
        }
        //���ο� �������� ������, �Ӱ��� ���� ������Ʈ �����ش�.
        public SphericalCoordinates Rotate(float newAzimuth, float newElevation)
        {
            azimuth += newAzimuth;
            elevation += newElevation;
            return this;
        }
        //�������̸鼭 ���� �������� ������(�������κ��� �Ÿ�) ���� ������Ʈ 
        public SphericalCoordinates TranslateRadius(float x)
        {
            radius += x;
            return this;
        }
    }

    public SphericalCoordinates sphericalCoordinates;

    private void Start()
    {
        sphericalCoordinates = new SphericalCoordinates(transform.position);
        //pivot �����κ��� ������ �Ÿ��� ���� ��ǥ��� ����Ͽ� ���� 
        transform.position = sphericalCoordinates.toCartesian + pivot.position;
    }

    private void Update()
    {
        float kh, kv, mh, mv, h, v;

        //����Ű �Է¿� ���� ������ ���� kh, kv�� �����
        kh = Input.GetAxis("Horizontal");
        kv = Input.GetAxis("Vertical");

        //���콺 ��ȣ�ۿ��� �� ��, �� ������ ���� mh�� mv�� �����
        bool anyMouseButton = Input.GetMouseButton(0) | Input.GetMouseButton(1) | Input.GetMouseButton(2);
        mh = anyMouseButton ? Input.GetAxis("Mouse X") : 0f;
        mv = anyMouseButton ? Input.GetAxis("Mouse Y") : 0f;

        //������ ��ȭ��: �� �� ū ���� Rotate�� �Ѵ�. (�׷��� �׷��� ���ÿ� �ϸ� �� �������� ū ������ ���ư�����) 
        h = kh * kh > mh * mh ? kh : mh;
        //�Ӱ� ��ȭ��: �� �� ū ���� Rotate�� �Ѵ�. (�׷��� �׷��� ���ÿ� �ϸ� �� �������� ū ������ ���ư�����) 
        v = kv * kv > mv * mv ? kv : mv;

        //�Ʊ���� �����ϴ� ������ ��ġ�� ������ 0�� �ƴ��� Ȯ���ϱ� ���ؼ�����.
        if (h * h > Mathf.Epsilon || v * v > Mathf.Epsilon)
        {
            //�������� �Ӱ��� ��ȭ���� �־� ������Ʈ �� �� ���� ��ǥ��� ��ȯ�Ͽ� pivot�� ��ġ�� �������� position ���� 
            transform.position = sphericalCoordinates.Rotate(h * rotateSpeed * Time.deltaTime, v * rotateSpeed * Time.deltaTime).toCartesian + pivot.position;
        }
        //�� �� / �� �ƿ�
        float sw = -Input.GetAxis("Mouse ScrollWheel");
        if(sw * sw > Mathf.Epsilon)
        {
            //�������� ��ȭ���� �־� ������Ʈ �� �� ���� ��ǥ��� ��ȯ�Ͽ� pivot�� ��ġ�� �������� position ����
            transform.position = sphericalCoordinates.TranslateRadius(sw * scrollSpeed * Time.deltaTime).toCartesian + pivot.position;
        }
        //�׻� ī�޶�� pivot�� �ٶ󺸵��� �Ѵ�.
        transform.LookAt(pivot.position);
    }
}
