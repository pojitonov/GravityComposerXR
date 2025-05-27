namespace Atomic.Networking
{
    public partial interface INetworkObject
    {
        public interface IListener
        {
        }

        public interface IInit : IListener
        {
            void OnInit();
        }

        public interface IFixedUpdate
        {
            void OnFixedUpdate(float deltaTime);
        }

        public interface IRender
        {
            void OnRender();
        }

        public interface IDispose : IListener
        {
            void OnDispose();
        }

        bool AddListener(IListener listener);

        bool RemoveListener(IListener listener);

        void EnableFixedUpdate(bool enable);
    }
}