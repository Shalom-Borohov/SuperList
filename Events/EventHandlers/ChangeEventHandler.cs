using System;
using SuperList.Events.EventArgs;

namespace SuperList.Events.EventHandlers
{
    public class ChangeEventHandler<T>
    {
        public event EventHandler<ChangeEventArgs<T>> RaiseChangeEvent;

        public void RaiseAddEvent(object publisher, T value) =>
            RaiseChangeEvent(publisher, new ChangeEventArgs<T>("Added value", value));

        public void RaiseRemoveEvent(object publisher, T value) =>
            RaiseChangeEvent(publisher, new ChangeEventArgs<T>("Removed value", value));
    }
}
