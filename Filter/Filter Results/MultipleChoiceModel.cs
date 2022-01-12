using System;

namespace Filter.Filters
{
    public class MultipleChoiceModel
    {
        public MultipleChoiceModel(string item, object model)
        {
            Item = item;
            Model = model;
        }
        public MultipleChoiceModel(string item)
        {
            Item = item;
        }

        public object Model { get; private set; }

        public string Item { get; private set; }

       
    }

}