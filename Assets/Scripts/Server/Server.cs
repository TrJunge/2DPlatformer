using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net.Sockets;
using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;

public class Server : MonoBehaviour
{
    static int port = 8005; // порт сервера 
    static string address = "25.82.240.212"; // адрес сервера 
    public static IPEndPoint ipPoint = new IPEndPoint(IPAddress.Parse(address), port);//подключение 
    public static Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);//Сокет 

    public class StateObject
    {
        public Socket workSocket = null;
        public const int BufferSize = 1024;
        public byte[] buffer = new byte[BufferSize];
        public StringBuilder sb = new StringBuilder();
    }
    static void Send(string k)
    {
        try
        {
            // Конвертируем для передачи сообщения 
            byte[] byteData = Encoding.UTF8.GetBytes(k);
            // Начинаем отправку сообщений 
            socket.BeginSend(byteData, 0, byteData.Length, 0, SendCallback, socket);
        }
        catch (Exception ex)
        {

        }
    }
    static void AsyncCompleted(IAsyncResult resObj)
    { //Статистика о подключении 
        string mes = (string)resObj.AsyncState;
    }
    static void SendCallback(IAsyncResult ar)
    {
        try
        {
            // извлекаем сокет 
            Socket client = (Socket)ar.AsyncState;
            // Завершаем отправку 
            int bytesSent = client.EndSend(ar);
            Receive(client);
        }
        catch (Exception e)
        {

        }
    }
    static void Receive(Socket client)
    {
        StateObject state = new StateObject();
        state.workSocket = client;
        client.BeginReceive(state.buffer, 0, StateObject.BufferSize, 0, new AsyncCallback(ReceiveCallback), state);
    }
    private static void ReceiveCallback(IAsyncResult ar)
    {
        try
        {
            StateObject state = (StateObject)ar.AsyncState;
            Socket client = state.workSocket;
            int bytesRead = client.EndReceive(ar);
            string Content = state.sb.Append(Encoding.UTF8.GetString(state.buffer, 0, state.buffer.Length)).ToString().TrimEnd();
            string[] Data = new string[9];
            Data = Content.Split(',');
            Debug.Log(Data[1]);
           

        }
        catch (Exception e)
        {

        }
    }
}
