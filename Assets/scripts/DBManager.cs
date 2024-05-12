using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DBManager : MonoBehaviour
{
    public static int P_ID;
    public static string username;
    public static int wins,losses;
    public static int ringID;

    public static bool LoggedIn { get { return username != null; } }

    public static void Logout()
    {
        username = null;
    }
}
