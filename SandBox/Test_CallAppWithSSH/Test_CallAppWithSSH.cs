﻿using Renci.SshNet;
using System;

namespace SandBox
{
    public class Test_CallAppWithSSH
    {
        static public void DoSomething()
        {
            var ci = new ConnectionInfo("127.0.0.1",
                    "user",
                    new PasswordAuthenticationMethod("user", "user"));

            //using var cli = new SshClient(ci);

            using var cli = new SshClient("127.0.0.1", 22, "user", "user");

            cli.Connect();

            // (1) 간단한 표현
            //var output = cli.CreateCommand("ls -al").Execute();
            //Console.WriteLine(output);

            //// (2) SshCommand 객체 변수 사용
            ////     Dispose 할 수 있고, ExitStatus 같은 속성 체크 가능
            //using (SshCommand cmd = cli.CreateCommand("cat .profile"))
            //{
            //    output = cmd.Execute();
            //    Console.WriteLine($"ExitStatus: {cmd.ExitStatus}");
            //    Console.WriteLine(output);
            //}

            //// (3) RunCommand() 사용. SshCommand 생성하고 실행.
            //SshCommand cmd2 = cli.RunCommand("cat .profile");
            //if (cmd2.ExitStatus == 0)
            //{
            //    Console.WriteLine(cmd2.Result); //결과
            //}
            //cmd2.Dispose();



            // 쉘스크립트 동작 테스트
            //SshCommand cmd2 = cli.RunCommand("/home/user/workspace/testRun.sh");
            //if (cmd2.ExitStatus == 0)
            //{
            //    Console.WriteLine(cmd2.Result); //결과
            //}



            SshCommand cmd2 = cli.RunCommand("/home/user/simspace/42/42_RemoteExecutor.sh");
            if (cmd2.ExitStatus == 0)
            {
                Console.WriteLine(cmd2.Result); //결과
            }

            cli.Disconnect();
        }
    }
}
