namespace bx_core
{
    public delegate void OnRecvEvent(object data);

    public class AbsSpider
    {
        public event OnRecvEvent OnRecvUser;
        public event OnRecvEvent OnFinish;
        public event OnRecvEvent OnThreadFinish;

        protected virtual void OnOnThreadFinish(object data)
        {
            OnThreadFinish?.Invoke(data);
        }

        protected virtual void OnOnFinish(object data)
        {
            OnFinish?.Invoke(data);
        }

        protected virtual void OnOnRecvUser(object data)
        {
            OnRecvUser?.Invoke(data);
        }
    }
}