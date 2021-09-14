using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SandBox
{
    public class Test_MV_SimModuleDevConcept
    {
        private const int BootCount = 3;
        private const int DisposeCount = 3;

        private int Count = 0;


        private int[,] StateInfo = new int[5, 5]
        {
            // none, boot, idle, run, dispose
            {1,2,0,0,0 },
            {1,1,1,0,0 },
            {1,0,1,1,1 },
            {1,0,1,1,1 },
            {1,0,0,0,0 }
        };

        private FSMState State = FSMState.None;

        public void PowerOn()
        {
            FSMState currentState = GetState();

            if (currentState.Equals(FSMState.None))
            {
                if(Transaction(currentState, FSMState.Boot))
                {
                    Count = 0;
                }
            }
        }

        public void PowerOff()
        {
            FSMState currentState = GetState();

            if (Transaction(currentState, FSMState.Dispose))
            {
                Count = 0;
            }
        }

        public void Run()
        {
            FSMState currentState = GetState();

            if (Transaction(currentState, FSMState.Run))
            {
                Count = 0;
            }
        }

        public void Idle()
        {
            
        }
        

        public void Step()
        {
            FSMState currentState = GetState();

            switch (currentState)
            {
                case FSMState.None:
                    break;
                case FSMState.Boot:
                    {
                        if (Count < BootCount)
                        {
                            Count++;
                            LogDisplay(string.Format("Step count {0}", Count));
                        }
                        else
                        {
                            Transaction(currentState, FSMState.Idle);
                        }
                    }
                    break;
                case FSMState.Idle:
                    break;
                case FSMState.Run:
                    {
                        LogDisplay(string.Format("Do Something"));
                        Transaction(currentState, FSMState.Idle);
                    }
                    break;
                case FSMState.Dispose:
                    {
                        if (Count < DisposeCount)
                        {
                            Count++;
                            LogDisplay(string.Format("Step count {0}", Count));
                        }
                        else
                        {
                            Transaction(currentState, FSMState.None);
                        }
                    }
                    break;
                default:
                    break;
            }
        }

        public FSMState GetState()
        {
            return this.State;
        }

        private bool Transaction(FSMState currentState, FSMState targetState)
        {
            bool result = false;

            if (StateInfo[(int)currentState, (int)targetState] > 0)
            {
                LogDisplay(string.Format("Transaction {0} -> {1}, {2} ", currentState.ToString(), targetState.ToString(), State.ToString()));
                State = targetState;
                result = true;
            }

            return result;
        }

        private void LogDisplay(string msg)
        {
            Console.WriteLine(msg);
        }
    }

    public enum FSMState
    {
        None,
        Boot,
        Idle,
        Run,
        Dispose
    }


}
