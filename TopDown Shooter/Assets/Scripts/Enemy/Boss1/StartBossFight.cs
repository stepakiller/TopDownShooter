using Unity.Cinemachine;
using UnityEngine;

public class StartBossFight : MonoBehaviour
{
    [SerializeField] LayerMask InteractLayer;
    [SerializeField] float camDistance;
    [SerializeField] BoxCollider door;
    [SerializeField] GameObject[] offObj;
    [SerializeField] GameObject[] onObj;

    CinemachinePositionComposer cinemachinePositionComposer;
    float saveCamDist;
    Boss1 boss1;
    CinemachineCamera _camera;
    void Start()
    {
        _camera = FindFirstObjectByType<CinemachineCamera>();
        boss1 = FindFirstObjectByType<Boss1>();
        cinemachinePositionComposer = _camera.GetComponent<CinemachinePositionComposer>();
    }

    void OnTriggerEnter (Collider other)
    {
        if(((1 << other.gameObject.layer) & InteractLayer.value) != 0)
        {
            PlayerPrefs.SetInt("Chekpoint", 1);
            saveCamDist = cinemachinePositionComposer.CameraDistance;
            cinemachinePositionComposer.CameraDistance = camDistance;
            for (int i = 0; i < offObj.Length; i++) offObj[i].SetActive(false);
            for (int i = 0; i < onObj.Length; i++) onObj[i].SetActive(true);
            door.enabled = false;
            boss1.SetStage(1);
            Destroy(gameObject);
        }
    }

    public void EndFight()
    {
        cinemachinePositionComposer.CameraDistance =  saveCamDist;
        door.enabled = true;
        for (int i = 0; i < onObj.Length; i++) onObj[i].SetActive(false);
        PlayerPrefs.DeleteKey("Chekpoint");
        PlayerPrefs.DeleteKey("MedKit");
    }
}
