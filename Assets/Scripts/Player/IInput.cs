public interface IInput
{
    public bool HitButtonDown { get; }

    public void Lock();
    public void Unlock();
}