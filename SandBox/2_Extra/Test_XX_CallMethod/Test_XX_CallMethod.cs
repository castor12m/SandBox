using System.Collections.Generic;

namespace SandBox
{
    public class Test_XX_CallMethod
    {
        private string _name = "";
        public string Name
        {
            get { return _name; }
            set
            {
                if (_name != value)
                {
                    _name = value;

                }
            }
        }

        public int value { get; set; } = 0;

        public string testValue1 { get; set; } = "0.0";

        public double testValue2 { get; set; } = 0.0;



        public void callMe(Queue<Test_XX_CallMethod> que, string name)
        {
            if (Name.Equals(name))
            {
                que.Enqueue(this);

                value = 1;
            }
        }


    }
}
