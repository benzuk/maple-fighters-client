﻿using System.IO;
using CommonCommunicationInterfaces;

namespace Shared.Game.Common
{
    public struct Character : IParameters
    {
        public CharacterClasses CharacterType;
        public string Name;
        public CharacterIndex Index;

        public Character(CharacterClasses characterType, string name, CharacterIndex characterIndex)
        {
            CharacterType = characterType;
            Name = name;
            Index = characterIndex;
        }

        public void Serialize(BinaryWriter writer)
        {
            writer.Write((byte)CharacterType);
            writer.Write(Name);
            writer.Write((byte)Index);
        }

        public void Deserialize(BinaryReader reader)
        {
            CharacterType = (CharacterClasses)reader.ReadByte();
            Name = reader.ReadString();
            Index = (CharacterIndex)reader.ReadByte();
        }
    }
}