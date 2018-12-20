using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using CC_Module.Socket;
using UnityEngine;
namespace CC_Game.tools {
    public class C_LogCat {
        static C_LogCat instance;
        public static C_LogCat GetInstance {
            get {
                if (instance == null) {
                    instance = new C_LogCat();
                    GameObject gg = new GameObject();
                    instance.Init();
                }
                return instance;
            }
        }
        public string currentTime;
        List<byte[]> logList = new List<byte[]>();
        void Init() {
            Application.logMessageReceivedThreaded += delegate (string condition, string stackTrace, LogType type) {
                //  logIndex++;
                //if ((byte)type == 2) {
                //    return;
                //}
                byte[] bytes = C_TcpConnect2T4L.S_MessageSerialize(5, (byte)type, condition + "+?=k=&+" + stackTrace + "+?=k=&+" + currentTime);
                logList.Add(bytes);
            };
        }

        C_TcpConnect2T4L o_TcpConnect2T4L;
        string ip;
        public void S_StartUp(string ip) {
            this.ip = ip;
            C_TcpConnect2T4L.C_Parameter parameter = new C_TcpConnect2T4L.C_Parameter(ip, 45584) {
                d_ConnectResultCallBack = S_ConnectResultCallBack,
                d_TcpMessageHandleEvent = S_TcpMessageHandleEvent,
            };
            C_TcpConnect2T4L.S_BeginConnect(parameter);
        }

        void S_ConnectResultCallBack(C_TcpConnect2T4L tcp) {
            if (tcp != null) {
                o_TcpConnect2T4L = tcp;
                o_TcpConnect2T4L.S_SendMessage(2, 2, SystemInfo.deviceModel + "__" + SystemInfo.deviceName);
                Thread thread = new Thread(S_TcpThreadEvent);
                thread.IsBackground = true;
                thread.Start();
            } else {
                S_StartUp(ip);
            }
        }
        void S_TcpMessageHandleEvent(byte[] bytes) {
            string ss = Encoding.UTF8.GetString(bytes, 6, bytes.Length - 6);
            //Debug.Log("eeeeeeeeeeeeeeee" + ss);
        }
        void S_TcpThreadEvent() {
            Debug.LogFormat("logCat开启发送消息________");
            while (o_TcpConnect2T4L.Tcp != null && o_TcpConnect2T4L.Tcp.Connected) {
                for (int i = 0; i < logList.Count; i++) {
                    o_TcpConnect2T4L.S_SendMessage(logList[i]);
                }
                logList.Clear();
                Thread.Sleep(20);
            }
        }
    }

    public class C_LogCatMono : MonoBehaviour {
        public string ip = "10.235.8.45";
        void Awake() {
            if (Application.internetReachability == NetworkReachability.ReachableViaLocalAreaNetwork) {
                //Debug.LogError("玩家网络--WiFi");
                GameObject.DontDestroyOnLoad(gameObject);
                C_LogCat.GetInstance.S_StartUp(ip);
            } else if (Application.internetReachability == NetworkReachability.ReachableViaCarrierDataNetwork) {
                //Debug.LogError("玩家网络--移动网络");
            } else {
                //Debug.LogError("玩家网络--null");
            }

        }
        private void Start() {

        }
        private void Update() {
            C_LogCat.GetInstance.currentTime = Time.realtimeSinceStartup.ToString("0.000");
        }
    }
}
