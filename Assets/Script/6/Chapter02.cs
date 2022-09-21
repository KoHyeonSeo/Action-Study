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
        //프로 퍼티를 위한 멤버 변수
        public float _radius, _azimuth, _elevation;

        /// <summary>
        /// radius(반지름) 프로퍼티 / Clamp로 MinRadius, MaxRadius가 제한된다.
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
        /// azimuth(방위각) 프로퍼티 / Repeat로 _maxAzimuth ~ _minAzimuth 범위 유지
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
        /// elevation(앙각) 프로퍼티 / Clamp로 MinRadius, MaxRadius가 제한된다.
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
        //반지름 제한 변수

        public float _minRadius = 3f;
        public float _maxRadius = 20f;

        //=====================================
        //방위각 제한 변수

        //방위각 Degree min 변수
        public float minAzimuth = 0f;
        //방위각 Raian min 변수
        private float _minAzimuth;

        //방위각 Degree max 변수
        public float maxAzimuth = 360f;
        //방위각 Radian max 변수
        private float _maxAzimuth;

        //=====================================
        //앙각 제한 변수

        //앙각 Degree min 변수
        public float minElevation = 0f;
        //앙각 Radian min 변수
        private float _minElevation;

        //앙각 Degree max 변수
        public float maxElevation = 90f;
        //앙각 Radian max 변수
        private float _maxElevation;

        //=====================================

        //기본 생성자
        public SphericalCoordinates() { }
        public SphericalCoordinates(Vector3 cartesianCoordinate)
        {
            //방위각을 Degree에서 Radian으로 바꿈
            _minAzimuth = Mathf.Deg2Rad * minAzimuth;
            _maxAzimuth = Mathf.Deg2Rad * maxAzimuth;


            //앙각을 Degree에서 Radian으로 바꿈
            _minElevation = Mathf.Deg2Rad * minElevation;
            _maxElevation = Mathf.Deg2Rad * maxElevation;

            //원점으로부터의 거리 -> 반지름
            radius = cartesianCoordinate.magnitude;

            //방위각 구하기
            azimuth = Mathf.Atan2(cartesianCoordinate.z, cartesianCoordinate.x);
            //앙각 구하기
            elevation = Mathf.Asin(cartesianCoordinate.y / radius);
        }
        //구면좌표에서 직교좌표로 변환
        public Vector3 toCartesian
        {
            get
            {
                float t = radius * Mathf.Cos(elevation);

                return new Vector3(t * Mathf.Cos(azimuth), radius * Mathf.Sin(elevation), t * Mathf.Sin(azimuth));
            }
        }
        //새로운 더해지는 방위각, 앙각을 통해 업데이트 시켜준다.
        public SphericalCoordinates Rotate(float newAzimuth, float newElevation)
        {
            azimuth += newAzimuth;
            elevation += newElevation;
            return this;
        }
        //움직임이면서 새로 더해지는 반지름(원점으로부터 거리) 값을 업데이트 
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
        //pivot 값으로부터 떨어진 거리를 직교 좌표계로 계산하여 설정 
        transform.position = sphericalCoordinates.toCartesian + pivot.position;
    }

    private void Update()
    {
        float kh, kv, mh, mv, h, v;

        //방향키 입력에 따른 움직임 값을 kh, kv에 담아줌
        kh = Input.GetAxis("Horizontal");
        kv = Input.GetAxis("Vertical");

        //마우스 상호작용을 할 시, 그 움직임 값을 mh와 mv에 담아줌
        bool anyMouseButton = Input.GetMouseButton(0) | Input.GetMouseButton(1) | Input.GetMouseButton(2);
        mh = anyMouseButton ? Input.GetAxis("Mouse X") : 0f;
        mv = anyMouseButton ? Input.GetAxis("Mouse Y") : 0f;

        //방위각 변화량: 둘 중 큰 수로 Rotate를 한다. (그래서 그런지 동시에 하면 더 움직임이 큰 값으로 돌아가더라) 
        h = kh * kh > mh * mh ? kh : mh;
        //앙각 변화량: 둘 중 큰 수로 Rotate를 한다. (그래서 그런지 동시에 하면 더 움직임이 큰 값으로 돌아가더라) 
        v = kv * kv > mv * mv ? kv : mv;

        //아까부터 제곱하는 이유는 수치의 절댓값이 0이 아님을 확인하기 위해서란다.
        if (h * h > Mathf.Epsilon || v * v > Mathf.Epsilon)
        {
            //방위각과 앙각의 변화량을 넣어 업데이트 한 뒤 직교 좌표계로 변환하여 pivot의 위치를 기점으로 position 변경 
            transform.position = sphericalCoordinates.Rotate(h * rotateSpeed * Time.deltaTime, v * rotateSpeed * Time.deltaTime).toCartesian + pivot.position;
        }
        //줌 인 / 줌 아웃
        float sw = -Input.GetAxis("Mouse ScrollWheel");
        if(sw * sw > Mathf.Epsilon)
        {
            //반지름의 변화량을 넣어 업데이트 한 뒤 직교 좌표계로 변환하여 pivot의 위치를 기점으로 position 변경
            transform.position = sphericalCoordinates.TranslateRadius(sw * scrollSpeed * Time.deltaTime).toCartesian + pivot.position;
        }
        //항상 카메라는 pivot을 바라보도록 한다.
        transform.LookAt(pivot.position);
    }
}
