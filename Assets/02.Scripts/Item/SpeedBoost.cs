using ArcadeVP;
using UnityEngine;

public class SpeedBoost : MonoBehaviour
{
    public float boostAmount = 20f; // 속도 증가량
    public float boostDuration = 2f; // 속도 증가 지속 시간

    private void OnTriggerEnter(Collider other)
    {
        ArcadeVehicleController vehicleController = other.GetComponent<ArcadeVehicleController>();
        if (vehicleController != null)
        {
            vehicleController.ApplySpeedBoost(boostAmount, boostDuration);
            
        }
    }
}
