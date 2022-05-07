using System.Collections;
using UnityEngine;

public class ScaleAnimation : MonoBehaviour
{
    [SerializeField] private StartAnimation _startAnimation;
    [SerializeField] private float _waitTime;
    private Vector3 _startSize = new Vector3(2.5f, 2.5f, 0);
    private Vector3 _endSize = new Vector3(1f, 1f, 0);
    private bool _zoomActivate = false;
    private void OnEnable()
    {
        _startAnimation.StartZoom += ZoomOut;
    }
    private void OnDisable()
    {
        _startAnimation.StartZoom -= ZoomOut;
    }
    private void Start()
    {

        gameObject.transform.localScale = _startSize;
    }
    private void ZoomOut()
    {
        StartCoroutine(WaitTime());
    }
    private void FixedUpdate()
    {
        if (_zoomActivate)
        {
            gameObject.transform.localScale = Vector3.Lerp(gameObject.transform.localScale, _endSize, Time.deltaTime * 2 );
        }
    }

    public IEnumerator WaitTime()
    {
        yield return new WaitForSeconds(_waitTime);
        {
            _zoomActivate = true;
        }
    }

}
