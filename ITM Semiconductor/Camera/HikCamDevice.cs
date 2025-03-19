using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MvCamCtrl.NET;
using DeviceSource;
using System.Runtime.InteropServices;

namespace VisionInspection
{
    public class HikCamDevice
    {
        MyCamera myCam;
        CameraOperator m_pOperator;
        public MyCamera.MV_CC_DEVICE_INFO_LIST m_pDeviceList;
        bool m_bGrabbing;
        UInt32 m_nBufSizeForDriver = 3072 * 2048 * 3;
        byte[] m_pBufForDriver = new byte[3072 * 2048 * 3];            // Buffer for getting image from driver
        UInt32 m_nBufSizeForSaveImage = 3072 * 2048 * 3 * 3 + 2048;
        byte[] m_pBufForSaveImage = new byte[3072 * 2048 * 3 * 3 + 2048];         // Buffer for saving image

        public HikCamDevice()
        {
            InitializeCamera();
        }

        bool InitializeCamera()
        {
            myCam = new MyCamera();
            m_pOperator = new CameraOperator();
            m_bGrabbing = false;
            //DeviceListAcq();
            return true;
        }

        public void DeviceListAcq()
        {
            int nRet;
            /*Create Device List*/
            System.GC.Collect();
            nRet = CameraOperator.EnumDevices(MyCamera.MV_GIGE_DEVICE | MyCamera.MV_USB_DEVICE, ref m_pDeviceList);
            if (0 != nRet)
            {
                return;
            }
        }
        public string GetserialNumber(MyCamera.MV_CC_DEVICE_INFO deviceT)
        {
            string SerialNumber = "";
            if (deviceT.nTLayerType == MyCamera.MV_GIGE_DEVICE)
            {
                IntPtr buffer = Marshal.UnsafeAddrOfPinnedArrayElement(deviceT.SpecialInfo.stGigEInfo, 0);
                MyCamera.MV_GIGE_DEVICE_INFO gigeInfo = (MyCamera.MV_GIGE_DEVICE_INFO)Marshal.PtrToStructure(buffer, typeof(MyCamera.MV_GIGE_DEVICE_INFO));

                SerialNumber = gigeInfo.chSerialNumber;

            }
            else if (deviceT.nTLayerType == MyCamera.MV_USB_DEVICE)
            {
                IntPtr buffer = Marshal.UnsafeAddrOfPinnedArrayElement(deviceT.SpecialInfo.stUsb3VInfo, 0);
                MyCamera.MV_USB3_DEVICE_INFO usbInfo = (MyCamera.MV_USB3_DEVICE_INFO)Marshal.PtrToStructure(buffer, typeof(MyCamera.MV_USB3_DEVICE_INFO));
                SerialNumber = usbInfo.chSerialNumber;

            }
            return SerialNumber;

        }

    }
}
