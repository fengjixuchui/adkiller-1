using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VisualStateManager;
using Action = VisualStateManager.Action;
namespace AdKiller
{
    public partial class AdKillerMainForm
    {
        private Action playAction;
        private Action stopAction;
        private Action selectRadioAction;
        private Condition isPlayingCondition;
        private Condition isFileSelectedCondition;
        private Condition isRadioSelectedCondition;

        public void InitializeActions()
        {
            isPlayingCondition = new Condition();
            isRadioSelectedCondition = new Condition();
            isFileSelectedCondition = new Condition();
            playAction = new Action(DoPlay, new CompositeCondition(CompositeCondition.CompositionKind.And, new NegateCondition(isPlayingCondition), isRadioSelectedCondition), PlayButton);
            stopAction = new Action(DoStop, isPlayingCondition, StopButton);
            selectRadioAction = new Action(SelectRadio, new NegateCondition(isPlayingCondition), listBoxRadio);
           
        }
    }
}
