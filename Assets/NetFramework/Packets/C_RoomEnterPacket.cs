﻿using Karin.Network;
using System;


namespace Packets
{
    public class C_RoomEnterPacket : Packet
    {
        public override ushort ID => (ushort)PacketID.C_RoomEnterPacket;


        public override void Deserialize(ArraySegment<byte> buffer)
        {
            ushort process = 0;
            process += sizeof(ushort);
            process += sizeof(ushort);
        }

        public override ArraySegment<byte> Serialize()
        {
            ArraySegment<byte> buffer = UniqueBuffer.Open(1024);
            ushort process = 0;

            process += sizeof(ushort);
            process += PacketUtility.AppendUShortData(this.ID, buffer, process);
            PacketUtility.AppendUShortData(process, buffer, 0);

            return  UniqueBuffer.Close(process);

        }
    }
}
