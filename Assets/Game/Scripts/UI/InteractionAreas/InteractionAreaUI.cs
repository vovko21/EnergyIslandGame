using System.Collections;
using UnityEngine;

public class InteractionAreaUI : MonoBehaviour
{
    [Header("Interaction settings")]
    [SerializeField] protected InteractableArea _interactionArea;
    [SerializeField] private Vector3 _animationScale;
    [SerializeField] protected float _animationSpeed;

    private Vector3 _defaultScale;

    private void Start()
    {
        Initialize(); 
    }

    protected virtual void Initialize()
    {
        _defaultScale = transform.localScale;
    }

    private void OnEnable()
    {
        _interactionArea.OnCharacterTrigger += OnCharacterTrigger;
    }

    private void OnDisable()
    {
        _interactionArea.OnCharacterTrigger -= OnCharacterTrigger;
    }

    protected virtual void OnCharacterTrigger(bool inside)
    {
        StopAllCoroutines();

        if (inside)
        {
            StartCoroutine(InCoroutineAnimation());
        }
        else
        {
            StartCoroutine(OutCoroutineAnimation());
        }
    }

    private IEnumerator InCoroutineAnimation()
    {
        bool finished = false;
        while(!finished)
        {
            transform.localScale = Vector3.Lerp(transform.localScale, _animationScale, _animationSpeed);

            if(transform.localScale == _animationScale)
            {
                finished = true;
            }

            yield return null;
        }
    }

    private IEnumerator OutCoroutineAnimation()
    {
        bool finished = false;
        while (!finished)
        {
            if(_defaultScale == transform.localScale)
            {
                finished = true;
            }

            transform.localScale = Vector3.Lerp(transform.localScale, _defaultScale, _animationSpeed);

            yield return null;
        }
    }
}
