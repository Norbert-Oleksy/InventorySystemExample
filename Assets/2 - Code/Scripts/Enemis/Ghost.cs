using System.Collections;
using UnityEngine;

public class Ghost : MonoBehaviour
{
    [SerializeField] private int _health = 1;
    [SerializeField] private int _damage = 10;
    [SerializeField] private float _damageDelay = 1f;
    [SerializeField] private float _speed = 3f;

    private Coroutine _ghostAttack;
    private Player _player;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && _ghostAttack == null) _ghostAttack = StartCoroutine(DamageOverTime());
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") && _ghostAttack != null)
        {
            StopCoroutine(_ghostAttack);
            _ghostAttack = null;
        }
    }

    private IEnumerator DamageOverTime()
    {
        while (true)
        {
            _player.TakeDamage(_damage);
            yield return new WaitForSeconds(_damageDelay);
        }
    }

    private void MoveTowardsPlayer()
    {
        Vector3 direction = (_player.transform.position - transform.position).normalized;
        transform.position += direction * _speed * Time.deltaTime;
    }

    private void RotateTowardsPlayer()
    {
        Vector3 direction = (_player.transform.position - transform.position).normalized;
        Quaternion targetRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 5f);
    }

    public void TakeDamage(int amount)
    {
        _health -= amount;
        if(_health >0) return;

        StopAllCoroutines();
        Destroy(this.gameObject);
    }

    #region Unity-API
    private void Start()
    {
        _player = FindFirstObjectByType<Player>();
    }
    private void Update()
    {
        if (TestManager.Instance != null && TestManager.Instance.Stage != TestManager.TestStage.Running) return;

        if (_ghostAttack != null) return;

        RotateTowardsPlayer();
        MoveTowardsPlayer();
    }
    #endregion
}
