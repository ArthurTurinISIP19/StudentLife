using UnityEngine.Events;

namespace Assets.Scripts
{
    interface IProgress
    {
        public abstract event UnityAction OnProgressChange;
        void ProgressUp();
    }
}
