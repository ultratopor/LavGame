using GameRoot;

namespace Root
{
    public class MainMEnuExitParams
    {
        public SceneEnterParams TargetSceneEnterParams { get; }

        public MainMEnuExitParams(SceneEnterParams targetSceneEnterParams)
        {  
            TargetSceneEnterParams = targetSceneEnterParams;
        }
    }
}
