using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Animatronic : MonoBehaviour
{
    [Header("Screamer")]
    [SerializeField] private GameObject _screamer;

    [Header("Doors")]
    [SerializeField] private DoorButton _leftDoor;
    [SerializeField] private DoorButton _rightDoor;

    [Header("UI")]
    [SerializeField] private List<Sprite> _sprites;

    [Header("Configuration")]
    [SerializeField] private List<WalkPoint> _walkPoints;
    public WalkPoint CurrentPoint;

    public Action<WalkPoint> PointChanged;
    private TimeController _timeController => TimeController.Instance;
    private LevelController _levelController => LevelController.Instance;
    private WalkPoint _starterPoint => _walkPoints[0];

    private int _hard => _levelController.Level;

    private void Start()
    {
        _timeController.TimeChanged += timeChanged;
    }

    private void timeChanged(int time)
    {
        if (time == 1) Attack();
        if (time < 360) return;
        StopAttack();
    }

    public Sprite GetRandomSprite() => _sprites[Random.Range(0, _sprites.Count)];

    public void Attack()
    {
        if (!CurrentPoint.isNearSecurity) attackAroundPizza();
        else attackNearSecurity();
    }

    private void attackNearSecurity() => StartCoroutine(attackSecurity());

    private void attackAroundPizza()
    {
        List<WalkPoint> nextPaths = CurrentPoint.NextPaths;
        int chance = Random.Range(0, 101);
        if (((60 - _hard * 10 > chance) && nextPaths.Count > 0) || CurrentPoint.LastPoint == null)
        {
            WalkPoint nextPath = nextPaths[Random.Range(0, nextPaths.Count)];
            StartCoroutine(attackAfterTime(nextPath));
        }
        else
        {
            StartCoroutine(attackAfterTime(CurrentPoint.LastPoint));
        }
    }

    public void StopAttack() => StopAllCoroutines();

    private IEnumerator attackSecurity()
    {
        yield return new WaitForSeconds(5);
        if (CurrentPoint.Point == 5 || CurrentPoint.Point == 8)
        {
            if ((!_leftDoor.IsOpen && CurrentPoint.Point == 5) ||
                (!_rightDoor.IsOpen && CurrentPoint.Point == 8))
            {
                CurrentPoint = CurrentPoint.LastPoint;
                Attack();
            }
            else
            {
                DoScream();
            }
        }
    }

    public void DoScream()
    {
        _screamer.SetActive(true);
    }

    private IEnumerator attackAfterTime(WalkPoint walkPoint)
    {
        yield return new WaitForSeconds(Random.Range(4 / _hard, 8 / _hard));
        CurrentPoint = walkPoint;
        PointChanged?.Invoke(CurrentPoint);
        Attack();
    }
}
