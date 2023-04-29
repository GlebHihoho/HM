using System;
using UnityEngine;

namespace DefaultNamespace
{
    public class Weapon : MonoBehaviour
    {
        [SerializeField] private float force = 4;
        [SerializeField] private float damage = 1;
        [SerializeField] private GameObject impactPrefab;
        [SerializeField] private Transform shootPoint;

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (Physics.Raycast(shootPoint.position, shootPoint.forward, out var hit))
                {
                    // Выводим название объекта куда попали
                    // print(hit.transform.gameObject.name);

                    Instantiate(impactPrefab, hit.point, Quaternion.LookRotation(hit.normal, Vector3.up));

                    var destructible = hit.transform.GetComponent<DestructibleObject>();

                    if (destructible != null)
                    {
                        destructible.ReceiveDamage(damage);
                    }

                    var rigidbody = hit.transform.GetComponent<Rigidbody>();

                    if (rigidbody != null)
                    {
                        // Добавить отбрасывание
                        // вызываем AddForce, в который нужно передать
                        // 1) направление силы: shootPoint.forward (куда смотрит наше оружие)
                        // умноженное на force (силу)
                        // 2) ForceMode.Impulse - говорит о том, что мы учитываем вес объекта, к
                        // которому добавляем силу
                        rigidbody.AddForce(shootPoint.forward * force, ForceMode.Impulse);
                    }
                }
            }
        }

        // Юнити метод, который рисует графику для редактора
        // в нём можно обращаться к классу Gizmos
        // Так же вызвается на каждом кадре, даже когда игра не запущена
        private void OnDrawGizmos()
        {
            // Выставляем красный цвет
            Gizmos.color = Color.red;
            
            // Рисуем луч, идущий из позиции нашего объекта shootPoint, направленный в shootPoint.forward
            // длина луча 9999 метров
            Gizmos.DrawRay(shootPoint.position, shootPoint.forward * 9999);
        }
    }
}
