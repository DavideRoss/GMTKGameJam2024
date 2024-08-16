using UnityEngine;
using UnityEngine.AI;

public enum Status
{
    Idle,
    Walking,
    Working
}

public class Villager : MonoBehaviour
{
    public Status Status = Status.Idle;

    NavMeshAgent _agent;
    BaseOrder _currentOrder;

    private void Start()
    {
        VillagerManager.Instance.Register(this);
        _agent = GetComponent<NavMeshAgent>();
    }
    
    private void Update()
    {
        if (Status == Status.Idle && _currentOrder != null)
        {
            _currentOrder.Execute(this);
        }
    }

    public void MoveTo(Vector3 position)
    {
        _agent.destination = position;
        Status = Status.Walking;

        // TODO: handle stop moving + event
    }

    public void SetOrder(BaseOrder order)
    {
        // TODO: stop agent if it's walking somewhere
        // TODO: free the workspot if it's going away from it

        _currentOrder = order;
    }
}