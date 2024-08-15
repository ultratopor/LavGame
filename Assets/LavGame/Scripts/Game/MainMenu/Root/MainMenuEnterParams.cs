using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainMenu.Root
{
    public class MainMenuEnterParams
    {
        public string Result {get;}
        // передаём какие-то входные пораметры для входа в главное меню
        public MainMenuEnterParams(string result)
        {
            this.Result = result;
        }
    }
}
