using System;

namespace Filter.Filter_Settings
{
    public class FilterAction
    {

        public FilterAction(string name, Action action)
        {
            Action = action;
            ActionName = name;
        }

        public Action Action { get; set; }

        public string ActionName { get; set; }
    }
}