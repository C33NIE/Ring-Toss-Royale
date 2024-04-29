using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DBManager : MonoBehaviour
{
    public static string username;
    public static int wins,losses;
    public static string most_used_ring;

    public static bool LoggedIn { get { return username != null; } }

    public static void Logout()
    {
        username = null;
    }
}
