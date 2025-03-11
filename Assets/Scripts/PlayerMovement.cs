using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// adding namespaces
using Unity.Netcode;
using Unity.Collections;
using UnityEngine.InputSystem;
// because we are using the NetworkBehaviour class
// NewtorkBehaviour class is a part of the Unity.Netcode namespace
// extension of MonoBehaviour that has functions related to multiplayer
public class PlayerMovement : NetworkBehaviour
{
    public float speed;
    private Vector2 move;


    // create a list of colors
    public List<Color> colors = new List<Color>();

    // assigned role for play (CPU, IO, or Memory)
    // public NetworkVariable<FixedString64Bytes> role = new NetworkVariable<FixedString64Bytes>("None");
    public string role = "None";

    // Start is called before the first frame update
    void Start()
    {

    }
    // Update is called once per frame
    void Update()
    {
        // check if the player is the owner of the object
        // makes sure the script is only executed on the owners 
        // not on the other prefabs 
        if (!IsOwner) return;

        movePlayer();

    }



    // movement function
    public void onMove(InputAction.CallbackContext context) {
        move = context.ReadValue<Vector2>();
    }

    public void movePlayer() {
        Vector3 movement = new Vector3(move.x, 0f, move.y);

        if(movement != Vector3.zero) {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(movement), 0.15f);
        }
        

        transform.Translate(movement * speed * Time.deltaTime, Space.World);
    }


    // this method is called when the object is spawned
    // we will change the color of the objects
    public override void OnNetworkSpawn()
    {
        GetComponent<MeshRenderer>().material.color = colors[(int)OwnerClientId];

        // check if the player is the owner of the object
        if (!IsOwner) return;
        // if the player is the owner of the object

        // set role and corresponding spawn location
        SetRoleServerRpc();
    }


    // set the roles of this client (run on the server)
    [ServerRpc]
    private void SetRoleServerRpc() {
        setSpawnLocationClientRpc();
        AssignCameraClientRpc();
    }

    // set spawn position of this client based on selected role
    [ClientRpc]
    private void setSpawnLocationClientRpc() {
        role = NetworkManagerUI.selectedRole;
        Debug.Log("Setting role to: " + role);

        Transform spawnPoint = GameObject.Find(role + " Spawn").transform;
        transform.position = spawnPoint.position;        
    }


    // switch camera positions of this client based on selected role
    [ClientRpc]
    private void AssignCameraClientRpc() {
        Camera assignedCamera = CameraManager.instance.GetCamera(role);

        if (assignedCamera != null) {
            // Camera.main.gameObject.SetActive(false);
            // assignedCamera.gameObject.SetActive(true);
            assignedCamera.enabled = true;
        }
        else {
            Debug.LogError("No camera for role: " + role);
        }
    }
}