public interface IPoolable 
{
    event UnityEngine.Events.UnityAction<IPoolable> DeactivationRequested;
    public void ResetObject() { }
}
   

