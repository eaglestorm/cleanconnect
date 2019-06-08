using System;
using CleanConnect.Common.Model.Identity;

namespace CleanConnect.Core.Model
{
    public class Device: Base<GuidIdentity>
    {
        public string Name { get; }

        public Device(string name)
        {
            Name = name;
            Id = new GuidIdentity();
        }
    }
}