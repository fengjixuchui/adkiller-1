using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VisualStateManager;
using Action = VisualStateManager.Action;
namespace AdKiller
{
   public partial class RadioStationForm
    {
       private Action deleteAction;
       private Action addAction;
       private Action editAction;
       private Action selectRadioAction;
       private Condition isRadioSelectedCondition;

       public void InitializeActions()
       {
           isRadioSelectedCondition = new Condition();
           deleteAction = new Action(DoDeleteRadio, isRadioSelectedCondition, DeleteRadioStationButton);
           editAction = new Action(DoEditRadio, isRadioSelectedCondition, EditRadioStationButton);
           selectRadioAction = new Action(SelectRadio,radioStationListBox);
           addAction = new Action(DoAddRadio,AddRadioStationButton);
       }
    }
}
