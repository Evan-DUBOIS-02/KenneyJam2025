using System;
using UnityEngine;
using UnityEngine.UI;

public class TowerMinion: MonoBehaviour
{
        public float _moveSpeed;
        private TowerManager _tower;

        private void Update()
        {
                transform.Translate(Vector3.forward*Time.deltaTime*_moveSpeed);
        }

        private void OnTriggerEnter(Collider other)
        {
                if (other.gameObject.CompareTag("Border"))
                {
                        Debug.Log("here");
                        _tower.IncreaseTerrain(1);
                        Destroy(gameObject);
                }
        }

        public void RegisterTower(TowerManager tower)
        {
                _tower = tower;
        }
}
