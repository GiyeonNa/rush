using ArcadeVP;
using UnityEngine;

public class SpeedBoost : MonoBehaviour
{
    [SerializeField]
    private float boostAmount = 5f; // 속도 증가량
    [SerializeField]
    private float boostDuration = 3f; // 속도 증가 지속 시간

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
