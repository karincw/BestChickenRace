using Karin.Network;
using System;

namespace Packets
{
    public class C_ItemInstalledPacket : Packet
    {
        public override ushort ID => (ushort)PacketID.C_ItemInstalledPacket;

        public PlayerPacket playerData;
        public ObjectPacket objectData;

        public override void Deserialize(ArraySegment<byte> buffer)
        {
            ushort process = 0;

            process += sizeof(ushort);
            process += sizeof(ushort);

            process += PacketUtility.ReadDataPacket<PlayerPacket>(buffer, process, out playerData);
            process += PacketUtility.ReadDataPacket<ObjectPacket>(buffer, process, out objectData);
        }

        public override ArraySegment<byte> Serialize()
        {
            ArraySegment<byte> buffer = UniqueBuffer.Open(1024);
            ushort process = 0;

            process += sizeof(ushort);
            process += PacketUtility.AppendUShortData(this.ID, buffer, process);

            process += PacketUtility.AppendDataPacket<PlayerPacket>(this.playerData, buffer, process);
            process += PacketUtility.AppendDataPacket<ObjectPacket>(this.objectData, buffer, process);
            
            PacketUtility.AppendUShortData(process, buffer, 0);

            return UniqueBuffer.Close(process);
        }
    }
}
