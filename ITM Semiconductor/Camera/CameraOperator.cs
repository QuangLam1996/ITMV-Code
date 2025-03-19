/***************************************************************************************************
***************************************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MvCamCtrl.NET;
using System.Runtime.InteropServices;


namespace DeviceSource
{
    using ImageCallBack = MyCamera.cbOutputdelegate;
    using ExceptionCallBack = MyCamera.cbExceptiondelegate;
    class CameraOperator
    {
        public const int CO_FAIL = -1;
        public const int CO_OK = 0;
        private MyCamera m_pCSI;
        //private delegate void ImageCallBack(IntPtr pData, ref MyCamera.MV_FRAME_OUT_INFO pFrameInfo, IntPtr pUser);

        public CameraOperator()
        {
            // m_pDeviceList = new MyCamera.MV_CC_DEVICE_INFO_LIST();
            m_pCSI = new MyCamera();
        }

        /****************************************************************************
         * @fn           EnumDevices
         * @brief        Enumerate devices
         * @param        nLayerType       IN         Transport layer protocol: 1-GigE; 4-USB; Can be stacked
         * @param        stDeviceList     OUT        Device List
         * @return       Success:0; Fail:-1
         ****************************************************************************/
        public static int EnumDevices(uint nLayerType, ref MyCamera.MV_CC_DEVICE_INFO_LIST stDeviceList)
        {
            return MyCamera.MV_CC_EnumDevices_NET(nLayerType, ref stDeviceList);
        }
        /****************************************************************************
         * @fn           Open
         * @brief        Open Device
         * @param        stDeviceInfo       IN       Device Information Structure
         * @return       Success:0; Fail:-1
         ****************************************************************************/
        public int Open(ref MyCamera.MV_CC_DEVICE_INFO stDeviceInfo)
        {
            if (null == m_pCSI)
            {
                m_pCSI = new MyCamera();
                if (null == m_pCSI)
                {
                    return CO_FAIL;
                }
            }

            int nRet;
            nRet = m_pCSI.MV_CC_CreateDevice_NET(ref stDeviceInfo);
            if (MyCamera.MV_OK != nRet)
            {
                return CO_FAIL;
            }
            nRet = m_pCSI.MV_CC_OpenDevice_NET();
            if (MyCamera.MV_OK != nRet)
            {
                return CO_FAIL;
            }
            return CO_OK;
        }


        /****************************************************************************
         * @fn           Close
         * @brief        Close Device
         * @param        none
         * @return       Success:0; Fail:-1
         ****************************************************************************/
        public int Close()
        {
            int nRet;
            nRet = m_pCSI.MV_CC_CloseDevice_NET();
            if (MyCamera.MV_OK != nRet)
            {
                return CO_FAIL;
            }

           
            return CO_OK;
        }


        /****************************************************************************
         * @fn           StartGrabbing
         * @brief        Start Grabbing
         * @param        none
         * @return       Success:0; Fail:-1
         ****************************************************************************/
        public int StartGrabbing()
        {
            int nRet;
            //Start Grabbing
            nRet = m_pCSI.MV_CC_StartGrabbing_NET();
            if (MyCamera.MV_OK != nRet)
            {
                return CO_FAIL;
            }
            return CO_OK;
        }



        /****************************************************************************
         * @fn           StopGrabbing
         * @brief        Stop Grabbing
         * @param        none
         * @return       Success:0; Fail:-1
         ****************************************************************************/
        public int StopGrabbing()
        {
            int nRet;
            nRet = m_pCSI.MV_CC_StopGrabbing_NET();
            if (MyCamera.MV_OK != nRet)
            {
                return CO_FAIL;
            }
            return CO_OK;
        }


        /****************************************************************************
         * @fn           RegisterImageCallBack
         * @brief        Register Image CallBack Function
         * @param        CallBackFunc          IN        CallBack Function
         * @param        pUser                 IN        User Parameters
         * @return       Success:0; Fail:-1
         ****************************************************************************/
        public int RegisterImageCallBack(ImageCallBack CallBackFunc, IntPtr pUser)
        {
            int nRet;
            nRet = m_pCSI.MV_CC_RegisterImageCallBack_NET(CallBackFunc, pUser);
            if (MyCamera.MV_OK != nRet)
            {
                return CO_FAIL;
            }
            return CO_OK;
        }


        /****************************************************************************
         * @fn           RegisterExceptionCallBack
         * @brief        Register Exception CallBack Function
         * @param        CallBackFunc          IN        CallBack Function
         * @param        pUser                 IN        User Parameters
         * @return       Success:0; Fail:-1
         ****************************************************************************/
        public int RegisterExceptionCallBack(ExceptionCallBack CallBackFunc, IntPtr pUser)
        {
            int nRet;
            nRet = m_pCSI.MV_CC_RegisterExceptionCallBack_NET(CallBackFunc, pUser);
            if (MyCamera.MV_OK != nRet)
            {
                return CO_FAIL;
            }
            return CO_OK;
        }


        /****************************************************************************
         * @fn           GetOneFrame
         * @brief        Get one frame image data222222222222222222222222222222223111
         * @param        pData                 IN-OUT            Data Array Pointer
         * @param        pnDataLen             IN                Date Size
         * @param        nDataSize             IN                Array Buffer Size
         * @param        pFrameInfo            OUT               Data Information
         * @return       Success:0; Fail:-1
         ****************************************************************************/
        public int GetOneFrame(IntPtr pData, ref UInt32 pnDataLen, UInt32 nDataSize, ref MyCamera.MV_FRAME_OUT_INFO pFrameInfo)
        {
            pnDataLen = 0;
            int nRet = m_pCSI.MV_CC_GetOneFrame_NET(pData, nDataSize, ref pFrameInfo);
            pnDataLen = pFrameInfo.nFrameLen;
            if (MyCamera.MV_OK != nRet)
            {
                return nRet;
            }

            return nRet;
        }

        public int GetOneFrameTimeout(IntPtr pData, ref UInt32 pnDataLen, UInt32 nDataSize, ref MyCamera.MV_FRAME_OUT_INFO_EX pFrameInfo, Int32 nMsec)
        {
            pnDataLen = 0;
            int nRet = m_pCSI.MV_CC_GetOneFrameTimeout_NET(pData, nDataSize, ref pFrameInfo, nMsec);
            pnDataLen = pFrameInfo.nFrameLen;
            if (MyCamera.MV_OK != nRet)
            {
                return nRet;
            }

            return nRet;
        }


        /****************************************************************************
         * @fn           Display
         * @brief        Display Image
         * @param        hWnd                  IN        Windows Handle
         * @return       Success:0; Fail:-1
         ****************************************************************************/
        public int Display(IntPtr hWnd)
        {

            int nRet;
            nRet = m_pCSI.MV_CC_Display_NET(hWnd);
            if (MyCamera.MV_OK != nRet)
            {
                return CO_FAIL;
            }
            return CO_OK;
        }


        /****************************************************************************
         * @fn           GetIntValue
         * @brief        Get Int Type Paremeters Value
         * @param        strKey                IN        Parameters key value, for detail value name please refer to HikCameraNode.xls
         * @param        pnValue               OUT       Return Value
         * @return       Success:0; Fail:-1
         ****************************************************************************/
        public int GetIntValue(string strKey, ref UInt32 pnValue)
        {

            MyCamera.MVCC_INTVALUE stParam = new MyCamera.MVCC_INTVALUE();
            int nRet = m_pCSI.MV_CC_GetIntValue_NET(strKey, ref stParam);
            if (MyCamera.MV_OK != nRet)
            {
                return CO_FAIL;
            }

            pnValue = stParam.nCurValue;

            return CO_OK;
        }


        /****************************************************************************
         * @fn           SetIntValue
         * @brief        Set Int Type Paremeters Value
         * @param        strKey                IN        Parameters key value, for detail value name please refer to HikCameraNode.xls
         * @param        nValue                IN        Set parameters value, for specific value range please refer to HikCameraNode.xls
         * @return       Success:0; Fail:-1
         ****************************************************************************/
        public int SetIntValue(string strKey, UInt32 nValue)
        {

            int nRet = m_pCSI.MV_CC_SetIntValue_NET(strKey, nValue);
            if (MyCamera.MV_OK != nRet)
            {
                return CO_FAIL;
            }
            return CO_OK;
        }



        /****************************************************************************
         * @fn           GetFloatValue
         * @brief        Get Floot Type Paremeters Value
         * @param        strKey                IN        Parameters key value, for detail value name please refer to HikCameraNode.xls
         * @param        pValue                OUT       Return Value
         * @return       Success:0; Fail:-1
         ****************************************************************************/
        public int GetFloatValue(string strKey, ref float pfValue)
        {
            MyCamera.MVCC_FLOATVALUE stParam = new MyCamera.MVCC_FLOATVALUE();
            int nRet = m_pCSI.MV_CC_GetFloatValue_NET(strKey, ref stParam);
            if (MyCamera.MV_OK != nRet)
            {
                return CO_FAIL;
            }

            pfValue = stParam.fCurValue;

            return CO_OK;
        }


        /****************************************************************************
         * @fn           SetFloatValue
         * @brief        Set Floot Type Paremeters Value
         * @param        strKey                IN        Parameters key value, for detail value name please refer to HikCameraNode.xls
         * @param        fValue                IN        Set parameters value, for specific value range please refer to HikCameraNode.xls
         * @return       Success:0; Fail:-1
         ****************************************************************************/
        public int SetFloatValue(string strKey, float fValue)
        {
            int nRet = m_pCSI.MV_CC_SetFloatValue_NET(strKey, fValue);
            if (MyCamera.MV_OK != nRet)
            {
                return CO_FAIL;
            }
            return CO_OK;
        }


        /****************************************************************************
         * @fn           GetEnumValue
         * @brief        Get Enum Type Paremeters Value
         * @param        strKey                IN        Parameters key value, for detail value name please refer to HikCameraNode.xls
         * @param        pnValue               OUT       Return Value
         * @return       Success:0; Fail:-1
         ****************************************************************************/
        public int GetEnumValue(string strKey, ref UInt32 pnValue)
        {
            MyCamera.MVCC_ENUMVALUE stParam = new MyCamera.MVCC_ENUMVALUE();
            int nRet = m_pCSI.MV_CC_GetEnumValue_NET(strKey, ref stParam);
            if (MyCamera.MV_OK != nRet)
            {
                return CO_FAIL;
            }

            pnValue = stParam.nCurValue;

            return CO_OK;
        }



        /****************************************************************************
         * @fn           SetEnumValue
         * @brief        Set Enum Type Paremeters Value
         * @param        strKey                IN        Parameters key value, for detail value name please refer to HikCameraNode.xls
         * @param        nValue                IN        Set parameters value, for specific value range please refer to HikCameraNode.xls
         * @return       Success:0; Fail:-1
         ****************************************************************************/
        public int SetEnumValue(string strKey, UInt32 nValue)
        {
            int nRet = m_pCSI.MV_CC_SetEnumValue_NET(strKey, nValue);
            if (MyCamera.MV_OK != nRet)
            {
                return CO_FAIL;
            }
            return CO_OK;
        }



        /****************************************************************************
         * @fn           GetBoolValue
         * @brief        Get Bool Type Paremeters Value
         * @param        strKey                IN        Parameters key value, for detail value name please refer to HikCameraNode.xls
         * @param        pbValue               OUT       Return Value
         * @return       Success:0; Fail:-1
         ****************************************************************************/
        public int GetBoolValue(string strKey, ref bool pbValue)
        {
            int nRet = m_pCSI.MV_CC_GetBoolValue_NET(strKey, ref pbValue);
            if (MyCamera.MV_OK != nRet)
            {
                return CO_FAIL;
            }

            return CO_OK;
        }


        /****************************************************************************
         * @fn           SetBoolValue
         * @brief        Set Bool Type Paremeters Value
         * @param        strKey                IN        Parameters key value, for detail value name please refer to HikCameraNode.xls
         * @param        bValue                IN        Set parameters value, for specific value range please refer to HikCameraNode.xls
         * @return       Success:0; Fail:-1
         ****************************************************************************/
        public int SetBoolValue(string strKey, bool bValue)
        {
            int nRet = m_pCSI.MV_CC_SetBoolValue_NET(strKey, bValue);
            if (MyCamera.MV_OK != nRet)
            {
                return CO_FAIL;
            }
            return CO_OK;
        }


        /****************************************************************************
         * @fn           GetStringValue
         * @brief        Get String Type Paremeters Value
         * @param        strKey                IN        Parameters key value, for detail value name please refer to HikCameraNode.xls
         * @param        strValue              OUT       Return Value
         * @return       Success:0; Fail:-1
         ****************************************************************************/
        public int GetStringValue(string strKey, ref string strValue)
        {
            MyCamera.MVCC_STRINGVALUE stParam = new MyCamera.MVCC_STRINGVALUE();
            int nRet = m_pCSI.MV_CC_GetStringValue_NET(strKey, ref stParam);
            if (MyCamera.MV_OK != nRet)
            {
                return CO_FAIL;
            }

            strValue = stParam.chCurValue;
            return CO_OK;
        }


        /****************************************************************************
         * @fn           SetStringValue
         * @brief        Set String Type Paremeters Value
         * @param        strKey                IN        Parameters key value, for detail value name please refer to HikCameraNode.xls
         * @param        strValue              IN        Set parameters value, for specific value range please refer to HikCameraNode.xls
         * @return       Success:0; Fail:-1
         ****************************************************************************/
        public int SetStringValue(string strKey, string strValue)
        {
            int nRet = m_pCSI.MV_CC_SetStringValue_NET(strKey, strValue);
            if (MyCamera.MV_OK != nRet)
            {
                return CO_FAIL;
            }
            return CO_OK;
        }


        /****************************************************************************
         * @fn           CommandExecute
         * @brief        Command
         * @param        strKey                IN        Parameters key value, for detail value name please refer to HikCameraNode.xls
         * @return       Success:0; Fail:-1
         ****************************************************************************/
        public int CommandExecute(string strKey)
        {
            int nRet = m_pCSI.MV_CC_SetCommandValue_NET(strKey);
            if (MyCamera.MV_OK != nRet)
            {
                return CO_FAIL;
            }
            return CO_OK;
        }


        /****************************************************************************
         * @fn           SaveImage
         * @brief        Save Image
         * @param        pSaveParam            IN        Save image configure parameters structure 
         * @return       Success:0; Fail:-1
         ****************************************************************************/
        public int SaveImage(ref MyCamera.MV_SAVE_IMAGE_PARAM_EX pSaveParam)
        {
            int nRet;
            nRet = m_pCSI.MV_CC_SaveImageEx_NET(ref pSaveParam);
            return nRet;
        }
        public int DisPose()
        {
            int nRet;
            nRet = m_pCSI.MV_CC_DestroyDevice_NET();
            return nRet;
        }
    }
}
