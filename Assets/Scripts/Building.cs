using System;
using System.Linq;
using ReferenceSharing;
using UnityEngine;

public class Building : MonoBehaviour
{
    public event Action<int, bool> OnUpdate;

    [SerializeField] private Reference<int> deathCountRef, fleeCountRef;
    [field: SerializeField] public string Name { get; private set; }

    [SerializeField] private Vector3 spawnPoint;
    [SerializeField] private GameObject deathParticles, fleeParticles;

    [SerializeField] private int windowIndex;
    private MeshRenderer _meshRenderer;
    private Material _windowMat;

    [field: SerializeField] public int Population { get; private set; }
    [field: SerializeField] public double PowerConsumption { get; private set; }

    private double _fleeAmount, _fleeTimer;
    [SerializeField] private double fleeTime, fleeRate;
    [SerializeField] private int fleeNumber;

    private double _deathTimer;
    [SerializeField] private double deathRate;
    [SerializeField] private int deathNumber;

    public bool Powered { get; private set; }
    public double CurrentConsumption => Powered ? PowerConsumption : 0;

    public enum Reason
    {
        Event,
        Flee,
        Death
    }

    public bool TogglePower()
    {
        SetPower(!Powered);
        return Powered;
    }

    public void SetPower(bool value)
    {
        Powered = value;
        if (Powered)
            _windowMat.EnableKeyword("_EMISSION");
        else
            _windowMat.DisableKeyword("_EMISSION");
        OnUpdate?.Invoke(Population, Powered);
    }

    public void AddPopulation(int amount)
    {
        Population += amount;
        OnUpdate?.Invoke(Population, Powered);
    }

    public void RemovePopulation(int amount, Reason reason = Reason.Event)
    {
        var toRemove = Population > amount ? amount : Population;
        Population -= toRemove;
        switch (reason)
        {
            case Reason.Flee:
                fleeCountRef.Value += toRemove;
                break;
            case Reason.Death:
                deathCountRef.Value += toRemove;
                break;
            case Reason.Event:
            default:
                break;
        }

        OnUpdate?.Invoke(Population, Powered);
    }

    private void Awake()
    {
        InitializeWindowsMaterial();
    }

    private void Start()
    {
        _fleeAmount = 0;
        _fleeTimer = 0;
        _deathTimer = 0;
        fleeCountRef.Value = 0;
        deathCountRef.Value = 0;
    }

    private void InitializeWindowsMaterial()
    {
        _meshRenderer = GetComponent<MeshRenderer>();
        _windowMat = Instantiate(Resources.Load<Material>("window"));
        var materials = _meshRenderer.sharedMaterials.ToList();
        materials[windowIndex] = _windowMat;
        _meshRenderer.materials = materials.ToArray();
    }

    public void HandlePopulationBehaviours()
    {
        if (Population == 0 || Powered)
        {
            _fleeAmount = 0;
            _deathTimer = 0;
            _fleeTimer = 0;
            return;
        }

        _fleeAmount += Time.deltaTime;

        if (_fleeAmount >= fleeTime)
        {
            _fleeTimer += Time.deltaTime * fleeRate;
            if (_fleeTimer >= 1)
            {
                _fleeTimer = 0;
                RemovePopulation(fleeNumber, Reason.Flee);
                Instantiate(fleeParticles, spawnPoint + transform.position, Quaternion.identity);
            }
        }
        else
        {
            _fleeTimer = 0;
        }

        _deathTimer += Time.deltaTime * deathRate;
        if (!(_deathTimer >= 1)) return;
        _deathTimer = 0;
        RemovePopulation(deathNumber, Reason.Death);
        Instantiate(deathParticles, spawnPoint + transform.position, Quaternion.identity);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(spawnPoint + transform.position, .1f);
    }
}