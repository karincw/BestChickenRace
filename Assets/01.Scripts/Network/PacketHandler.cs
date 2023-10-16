using Karin.Network;
using Packets;
using System;
using UnityEngine;

public class PacketHandler
{
    public static void S_RoomEnterPacket(Session session, Packet packet)
    {

        S_RoomEnterPacket roomEnterPacket = packet as S_RoomEnterPacket;
        GameManager.Instance.playerID = roomEnterPacket.playerid;
        SceneLoader.Instance.LoadSceneAsync("InGameScene", () =>
        {
            roomEnterPacket.playerList.ForEach(GameManager.Instance.AddPlayer);
        });
    }

    public static void S_MovePacket(Session session, Packet packet)
    {
        S_MovePacket movePacket = packet as S_MovePacket;
        PlayerPacket playerdata = movePacket.playerData;

        OtherPlayer player = GameManager.Instance.GetPlayer(playerdata.PlayerID);
        player?.SetPosition(playerdata);
    }

    public static void S_PlayerJoinPacket(Session session, Packet packet)
    {
        S_PlayerJoinPacket joinPacket = packet as S_PlayerJoinPacket;
        GameManager.Instance.AddPlayer(joinPacket.playerdata);
    }

    public static void S_SelectEndPacket(Session session, Packet packet)
    {
        InGameUIManager.Instance.ChangeGame();
    }

    public static void S_ItemInstalledPacket(Session session, Packet packet)
    {
        S_ItemInstalledPacket InstalledPacket = packet as S_ItemInstalledPacket;
    }

    public static void S_InstallEndPacket(Session session, Packet packet)
    {

    }

    public static void S_MoveEndedPacket(Session session, Packet packet)
    {

    }
}
