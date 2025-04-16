using ArcadeVP;
using UnityEngine;

public class SpeedBoost : MonoBehaviour
{
    public float boostAmount = 20f; // �ӵ� ������
    public float boostDuration = 2f; // �ӵ� ���� ���� �ð�

    private void OnTriggerEnter(Collider other)
    {
        ArcadeVehicleController vehicleController = other.GetComponent<ArcadeVehicleController>();
        if (vehicleController != null)
        {
            vehicleController.ApplySpeedBoost(boostAmount, boostDuration);
            
        }
    }
}
