using System.Collections.Generic;

using UnityEngine;

public class ParticleManager : MonoBehaviour
{
    public static ParticleManager Instance;
    [SerializeField] private ParticleSystem _sparkSystem;
    [SerializeField]private float _emissionRate;

    private ParticleSystem.EmitParams _emitParams;


    public void EmitSparkAtLocations(List<Vector3> positions)
    {

        int countToEmit = Mathf.FloorToInt(_emissionRate);

        if(countToEmit == 0) return;

        foreach(var pos in positions)
        {
            _sparkSystem.transform.position = pos;

            _sparkSystem.Emit(countToEmit);
        }
    }
    private void Awake()
    {
        if(Instance != null)
        {
            Destroy(this);
            return;
        }
        Instance = this;
    }
    //private void OnEnable()
    //{
    //    var list = new List<Vector3>() { Vector3.zero };
    //    SparkAtLocations(list);
    //}

}
