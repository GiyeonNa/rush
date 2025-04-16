using ArcadeVP;
using UnityEngine;

public class SpeedBoost : MonoBehaviour
{
    [SerializeField]
    private float boostAmount = 5f; // �ӵ� ������
    [SerializeField]
    private float boostDuration = 3f; // �ӵ� ���� ���� �ð�

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player entered speed boost area");

            ArcadeVehicleController vehicleController = other.GetComponent<ArcadeVehicleController>();
            if (vehicleController != null)
            {
                vehicleController.ApplySpeedBoost(boostAmount, boostDuration);
            }
        }
    }
}
