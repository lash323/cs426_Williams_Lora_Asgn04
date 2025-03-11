using UnityEngine;
using Unity.Collections;

public class CameraManager : MonoBehaviour
{
    public static CameraManager instance;

    [SerializeField] private Camera IOCamera;
    [SerializeField] private Camera CPUCamera;
    [SerializeField] private Camera MemCamera;

    private void Awake() {
        if (instance == null) {
            instance = this;
        } else {
            Destroy(gameObject);
        }
    }

    public Camera GetCamera(FixedString64Bytes role) {
        if (role == "IO") {
            return IOCamera;
        }
        else if (role == "CPU") {
            return CPUCamera;
        }
        else if (role == "Memory") {
            return MemCamera;
        }
        else {
            Debug.LogError("Invalid role. Cannot assign camera.");
            return null;
        }
    }

}
