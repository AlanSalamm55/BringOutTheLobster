using System;

public interface IHasProgress 
{
    public event EventHandler<OnprogressChangeEventArgs> OnProgressChange;
    public class OnprogressChangeEventArgs : EventArgs
    {
        public float progressNormalized;
    }
}
