using System;
using System.Linq;

namespace SandBox
{
    public class Test_MV_SimModuleDevConcept
    {
        #region 맴버변수
        private const long Count_Boot = 10;
        private const long Count_Dispose = 1;

        private long Count = 0;

        // StateInfo
        // 0 , None         : 상태 천이 불가
        // 1 , Force        : 상태 천이 즉시 변경 가능
        // 2 , Condition    : 조건 만족해야만 상태 천이 가능
        private int[,] StateChangeInfos = new int[5, 5]
        {
            // none, boot, idle, run, dispose
            {0,1,0,0,0 },
            {0,0,2,0,1 },
            {0,0,0,1,1 },
            {0,0,1,0,1 },
            {2,0,0,0,0 }

        };

        private long[,] StateConditionInfos = new long[5, 5]
        {
            {0,0,0,0,0 },
            {0,0,0,0,0 },
            {0,0,0,0,0 },
            {0,0,0,0,0 },
            {0,0,0,0,0 }
        };

        private FSMState State = FSMState.None;
        private enum FSMState
        {
            None,
            Boot,
            Idle,
            Run,
            Dispose
        }

        #endregion

        #region 생성자
        public Test_MV_SimModuleDevConcept()
        {
            StateConditionInfos[(int)FSMState.Boot, (int)FSMState.Idle] = Count_Boot;
            StateConditionInfos[(int)FSMState.Dispose, (int)FSMState.None] = Count_Dispose;
        }

        #endregion

        #region 매소드
        public void PowerOn()
        {
            Transaction(FSMState.Boot);
        }

        public void PowerOff()
        {
            Transaction(FSMState.Dispose);
        }

        public void Run()
        {
            Transaction(FSMState.Run);
        }

        public void Idle()
        {
            Transaction(FSMState.Idle);
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
                        if (Count < Count_Boot)
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
                        Count++;
                        LogDisplay(string.Format("Do Something {0}", Count));
                        //Transaction(currentState, FSMState.Idle);
                    }
                    break;
                case FSMState.Dispose:
                    {
                        if (Count < Count_Dispose)
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

        private FSMState GetState()
        {
            return this.State;
        }

        private bool Transaction(FSMState targetState)
        {
            FSMState currentState = GetState();

            return Transaction(currentState, targetState);
        }

        private bool Transaction(FSMState currentState, FSMState targetState)
        {
            bool result = false;

            int stateItem = StateChangeInfos[(int)currentState, (int)targetState];

            if (stateItem > 0)
            {
                bool IsChange = true;

                if (stateItem == 2)
                {
                    if (Count < StateConditionInfos[(int)currentState, (int)targetState])
                    {
                        IsChange = false;
                    }
                }

                if (IsChange)
                {
                    State = targetState;

                    Count = 0;

                    LogDisplay(string.Format("Transaction {0} -> {1}, {2} ", currentState.ToString(), targetState.ToString(), State.ToString()));

                    result = true;
                }
            }

            return result;
        }

        #endregion

        #region 테스트 한정
        private void LogDisplay(string msg)
        {
            Console.WriteLine(msg);
        }

        #endregion

    }


}
