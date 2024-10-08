namespace ServerManagement.StateStore
{
    public class MontrealOnlineServersStore : Observer
    {

        private int _numServersOnline;

        public int GetNumServersOnline() {
            return _numServersOnline;
        }

        public void SetNumServersOnline(int numServersOnline)
        {
            _numServersOnline = numServersOnline;
            BroadcastStateChange();
        }
    }
}
