using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace RPG.Saving {
    public interface ISaveable {

        void RestoreState(object state);
        object CaptureState();

    }
}
